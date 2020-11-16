using System;
using System.Collections.Generic;

#nullable disable

namespace StoreDAC.Entities
{
    public partial class Bill
    {
        public Bill()
        {
            BillItems = new HashSet<BillItem>();
        }

        public long BillId { get; set; }
        public int OperatorId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime BillDate { get; set; }
        public string TimeStamp { get; set; }
        public string BillStatus { get; set; }
        public decimal? SalesTax { get; set; }

        public virtual Employee Operator { get; set; }
        public virtual ICollection<BillItem> BillItems { get; set; }
    }
}
