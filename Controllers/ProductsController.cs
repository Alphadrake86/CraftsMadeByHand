using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using CraftsMadeByHand.Data;
using CraftsMadeByHand.Models;
using System.IO;
using Microsoft.AspNetCore.Identity;

namespace CraftsMadeByHand.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ProductsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles = IdentityHelper.AdminRole+","+IdentityHelper.SellerRole)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("ProductId,Title,Description,Price")] Product product, List<IFormFile> UserImages)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                product.SellerId = user.Id;
                    
                _context.Add(product);
                await _context.SaveChangesAsync();

                if (UserImages.Count > 0)
                {
                        foreach (IFormFile file in UserImages)
                        {
                        var memoryStream = new MemoryStream();
                        await file.CopyToAsync(memoryStream);

                            // Upload the file if less than 2 MB
                            if (memoryStream.Length < 2097152)
                            {
                                var image = new Image()
                                {
                                    Content = memoryStream.ToArray(),
                                    ProductId = product.ProductId,
                                    FileName = file.FileName
                                };

                                _context.Images.Add(image);
                                await _context.SaveChangesAsync();
                            }
                            else
                            {
                                
                                ModelState.AddModelError("File", "The file is too large.");
                            }
                        }
                }
                

                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = IdentityHelper.AdminRole + "," + IdentityHelper.SellerRole)]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Title,Description,Price,SellerId")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = IdentityHelper.AdminRole + "," + IdentityHelper.SellerRole)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await ImageHelper.DeleteAllImagesByProductId(_context, id);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }

        public IActionResult GetTopProductImageFor(int id)
        {
            Image img = ImageHelper.GetTopImageByProductId(_context, id);
            return File(img.Content, "image/png");
        }

        public IActionResult GetImage(int imgId)
        {
            Image img = ImageHelper.GetImageById(_context, imgId);
            return File(img.Content, "image/png");
        }
    }

    
}
