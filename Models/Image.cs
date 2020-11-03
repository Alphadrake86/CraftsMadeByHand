using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CraftsMadeByHand.Models
{
    /// <summary>
    /// Represents an image that is stored on the disk. The path is 
    /// stored on the database for ease of retrieval.
    /// </summary>
    public class Image
    {
        /// <summary>
        /// The image path on the disk
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// The product that this image is for
        /// </summary>
        public int ProductId { get; set; }
    }
}
