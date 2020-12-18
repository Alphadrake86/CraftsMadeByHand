using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using CraftsMadeByHand.Data;
using System.IO;

namespace CraftsMadeByHand.Models
{
    /// <summary>
    /// A class created to help with getting images
    /// </summary>
    public static class ImageHelper
    {
        /// <summary>
        /// Initiaizes the DB with the default image, taken from the files
        /// </summary>
        public static async Task InitializeDb(ApplicationDbContext _context)
        {
            if (_context.Images.Count() == 0)
            {
                using (FileStream fs = new FileStream("./wwwroot/img/OIP.jpg", FileMode.Open, FileAccess.Read))
                {
                    using(var memoryStream = new MemoryStream())
                    {
                        await fs.CopyToAsync(memoryStream);


                        Image img = new Image()
                        {
                            ProductId = 0,
                            FileName = "NotFound.jpg",
                            Content = memoryStream.ToArray()
                        };

                        await _context.Images.AddAsync(img);

                        await _context.SaveChangesAsync();
                    }
                }
            }
        }

        /// <summary>
        /// Gets the display image for a product, based on its id
        /// </summary>
        public static Image GetTopImageByProductId(ApplicationDbContext _context, int id)
        {
           Image img = _context.Images.FirstOrDefault(img => img.ProductId == id);
           return img ?? GetDefaultImage(_context);
        } 

        /// <summary>
        /// returns the default "Image not available" image
        /// </summary>
        public static Image GetDefaultImage(ApplicationDbContext _context)
        {
            return _context.Images.FirstOrDefault(img => img.ID == 1);
        }

        /// <summary>
        /// gets an image by image id
        /// </summary>
        public static Image GetImageById(ApplicationDbContext _context, int id)
        {
            Image img = _context.Images.FirstOrDefault(img => img.ID == id);
            return img ?? GetDefaultImage(_context);
        }

        /// <summary>
        /// gets a list of all images for a single product
        /// </summary>
        public static List<Image> GetAllProductImages(ApplicationDbContext _context, int id)
        {
            return _context.Images.Where(img => img.ProductId == id).ToList();
        }

        /// <summary>
        /// when a product is removed, all of its images should leave with it
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task DeleteAllImagesByProductId(ApplicationDbContext _context, int id)
        {
            _context.Images.RemoveRange(GetAllProductImages(_context, id));
            await _context.SaveChangesAsync();
        }
    }
}
