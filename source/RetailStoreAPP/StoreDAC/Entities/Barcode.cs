using System;
using System.Collections.Generic;

#nullable disable

namespace StoreDAC.Entities
{
    public partial class Barcode
    {
        public int Id { get; set; }
        public string BarcodeId { get; set; }
        public long SerialNumber { get; set; }

        public virtual Product SerialNumberNavigation { get; set; }
    }
}
