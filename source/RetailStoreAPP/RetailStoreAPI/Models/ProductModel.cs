using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RetailStoreAPI.Models
{
    public class ProductModel
    {
        public long SerialNumber { get; set; }
        [Required]
        [MaxLength(30)]
        public string ProductName { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public bool IsWeightedItem { get; set; }
        [Required]
        [MaxLength(10)]
        public string? UnitWeight { get; set; }
        [Required]
        public double UnitsInStock { get; set; }

    }
}
