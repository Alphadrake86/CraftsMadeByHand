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
        public static void AddImage(ApplicationDbContext _context, IFormFile file)
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
    }
}
