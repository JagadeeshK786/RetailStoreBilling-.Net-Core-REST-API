using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailStoreAPI.Models
{
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }
        public int RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Dob { get; set; }
        public string Mobile { get; set; }
        public string EmailId { get; set; }
        public string Address1 { get; set; }        
        public bool? Active { get; set; }
        public string Role { get; set; }
    }
}