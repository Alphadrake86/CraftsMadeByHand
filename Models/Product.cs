﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CraftsMadeByHand.Models
{
    /// <summary>
    /// A product, put up by a Seller for a Buyer
    /// </summary>
    public class Product
    {
        /// <summary>
        /// The Product Id for the database
        /// </summary>
        [Key]
        public int ProductId { get; set; }

        /// <summary>
        /// The Title of the product. User Generated by the Seller.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The Description of the product. User Generated by the Seller.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// A list of tags associated with the product. Needs to be changed in the future.
        /// </summary>
        [NotMapped]
        public List<string> Tags { get; set; }

        /// <summary>
        /// The price of the product, in USD.
        /// </summary>
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        /// <summary>
        /// The Id of the Seller of the product.
        /// </summary>
        public int SellerId { get; set; }
    }
}
