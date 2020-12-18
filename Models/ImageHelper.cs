using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using CraftsMadeByHand.Data;

namespace CraftsMadeByHand.Models
{
    /// <summary>
    /// A class created to help with getting images
    /// </summary>
    public static class ImageHelper
    {
        public static void AddImage(ApplicationDbContext _context, Image img)
        {

        }

        public static Image GetTopImageByProductId(ApplicationDbContext _context, int id)
        {
           Image img = _context.Images.FirstOrDefault(img => img.ProductId == id);
           return img ?? GetDefaultImage(_context);
        } 

        public static Image GetDefaultImage(ApplicationDbContext _context)
        {
            return _context.Images.FirstOrDefault(img => img.ID == 1);
        }

        public static Image GetImageById(ApplicationDbContext _context, int id)
        {
            Image img = _context.Images.FirstOrDefault(img => img.ID == id);
            return img ?? GetDefaultImage(_context);
        }

        public static List<Image> GetAllProductImages(ApplicationDbContext _context, int id)
        {
            return _context.Images.Where(img => img.ProductId == id).ToList();
        }

        public static async Task DeleteAllImagesByProductId(ApplicationDbContext _context, int id)
        {
            _context.Images.RemoveRange(GetAllProductImages(_context, id));
            await _context.SaveChangesAsync();
        }
    }
}
