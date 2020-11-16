using StoreDAC.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailStoreAPI.Models
{
    public partial class ProductCategoryModel
    {
        public ProductCategoryModel()
        { }

        public int GroupId { get; set; }
        [Required]
        [MaxLength(30)]
        public string CategoryName { get; set; }
        [MaxLength(50)]
        public string Description { get; set; }
        public byte[] Picture { get; set; }
        public bool? Active { get; set; }
    }

}
