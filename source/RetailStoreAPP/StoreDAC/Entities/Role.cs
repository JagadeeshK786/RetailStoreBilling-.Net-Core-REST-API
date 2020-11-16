using System;
using System.Collections.Generic;

#nullable disable

namespace StoreDAC.Entities
{
    public partial class Role
    {
        public Role()
        {
            Employees = new HashSet<Employee>();
            InverseSupervisor = new HashSet<Role>();
        }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public int SupervisorId { get; set; }
        public bool? Active { get; set; }

        public virtual Role Supervisor { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Role> InverseSupervisor { get; set; }
    }
}
