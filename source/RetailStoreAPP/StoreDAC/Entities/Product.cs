using System;
using System.Collections.Generic;

#nullable disable

namespace StoreDAC.Entities
{
    public partial class Product
    {
        public Product()
        {
            Barcodes = new HashSet<Barcode>();
            BillItems = new HashSet<BillItem>();
        }

        public long SerialNumber { get; set; }
        public string ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public int CategoryId { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsWeightedItem { get; set; }
        public string? UnitWeight { get; set; }
        public double UnitsInStock { get; set; }
        public double Discount { get; set; }
        public byte[]? Picture { get; set; }
        public string? Color { get; set; }
        public bool? Active { get; set; }

        public virtual ProductCategory Category { get; set; }
        public virtual ICollection<Barcode> Barcodes { get; set; }
        public virtual ICollection<BillItem> BillItems { get; set; }
    }
}
