using System;
using System.Collections.Generic;

#nullable disable

namespace StoreDAC.Entities
{
    public partial class Employee
    {
        public Employee()
        {
            Bills = new HashSet<Bill>();
        }

        public int EmployeeId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Dob { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Mobile { get; set; }
        public string EmailId { get; set; }
        public bool? Active { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
    }
}
