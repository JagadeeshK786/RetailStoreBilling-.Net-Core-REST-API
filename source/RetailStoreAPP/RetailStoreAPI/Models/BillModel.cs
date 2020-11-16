using FluentValidation;
using System;
using System.Collections.Generic;

namespace RetailStoreAPI.Models
{
    public class BillNewModel
    {
        public int OperatorId { get; set; }
        //public DateTime BillDate { get; set; }
        public virtual ICollection<BillItemNewModel> BillItems { get; set; }
    }

    public class BillDetailsModel
    {
        public long BillId { get; set; }
        public int OperatorId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime BillDate { get; set; }
        public string BillStatus { get; set; }
        public virtual ICollection<BillItemModel> BillItems { get; set; }
    }

    public class BillModel
    {
        public long BillId { get; set; }
        public int OperatorId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime BillDate { get; set; }
        public string BillStatus { get; set; }
    }
}
