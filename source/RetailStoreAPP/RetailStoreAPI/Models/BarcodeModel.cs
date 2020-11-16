using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailStoreAPI.Models
{
    public class BarcodeModel
    {
        [Required]
        [StringLength(13)]
        public string BarcodeId { get; set; }
        public long SerialNumber { get; set; }
    }
}