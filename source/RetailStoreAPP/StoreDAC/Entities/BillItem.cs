using System;
using System.Collections.Generic;

#nullable disable

namespace StoreDAC.Entities
{
    public partial class BillItem
    {
        public long Sno { get; set; }
        public long BillId { get; set; }
        public long ProductId { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal NetPrice { get; set; }
        public decimal Quantity { get; set; }
        public string Status { get; set; }

        public virtual Bill Bill { get; set; }
        public virtual Product Product { get; set; }
    }
}
