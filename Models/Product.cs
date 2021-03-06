﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
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
        /// The price of the product, in USD.
        /// </summary>
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        /// <summary>
        /// The Id of the Seller of the product.
        /// </summary>
        public string SellerId { get; set; }
    }

    /// <summary>
    /// used to bundle the cart items together. May become obselete.
    /// </summary>
    public class ProductComparer : IEqualityComparer<Product>
    {
        public bool Equals([AllowNull] Product x, [AllowNull] Product y)
        {
            return x.ProductId == y.ProductId;
        }

        public int GetHashCode([DisallowNull] Product obj)
        {
            return obj.ProductId;
        }
    }

}
