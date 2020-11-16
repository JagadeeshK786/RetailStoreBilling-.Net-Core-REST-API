using System;
using System.Collections.Generic;
using System.Text;

namespace StoreServices.ServiceModels
{
    public class BillItemUpdateSM
    {
        public long ItemId { get; set; }
        public long BillId { get; set; }
        public long ProductId { get; set; }
        public decimal Quantity { get; set; }
    }
}
