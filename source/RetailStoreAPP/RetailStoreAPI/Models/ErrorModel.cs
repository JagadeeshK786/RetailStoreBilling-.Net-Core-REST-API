using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailStoreAPI.Models
{
    public class ErrorModel
    {
        [Required]
        public int StatusCode { get; set; }
        [Required]
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}