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
        public int ID { get; set; }
        /// <summary>
        /// The product that this image is for.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// The actual content of the image
        /// </summary>
        public byte[] Content { get; set; }

        /// <summary>
        /// The name of the uploaded file. Only used by the uploading user.
        /// </summary>
        public string FileName { get; set; }
    }
}
