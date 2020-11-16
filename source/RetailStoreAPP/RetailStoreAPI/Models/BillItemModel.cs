using FluentValidation;

namespace RetailStoreAPI.Models
{
    public class BillItemModel
    {
        public long ItemId { get; set; }
        public long BillId { get; set; }
        public long ProductId { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal NetPrice { get; set; }        
        public decimal Quantity { get; set; }
        public string Status { get; set; }
        public string ProductName { get; set; }
    }

    public class BillItemNewModel
    {
        public long ProductId { get; set; }
        public decimal Quantity { get; set; }
    }

    public class BillItemUpdateModel
    {
        public long ItemId { get; set; }
        public long BillId { get; set; }
        public long ProductId { get; set; }
        public decimal Quantity { get; set; }
    }
   
}
