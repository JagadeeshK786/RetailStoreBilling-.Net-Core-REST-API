using FluentValidation;
using RetailStoreAPI.Models;

namespace RetailStoreAPI.Validators
{
    public class BillItemUpdateValidator : AbstractValidator<BillItemUpdateModel>
    {
        public BillItemUpdateValidator()
        {
            RuleFor(item => item.ItemId).GreaterThan(0).WithMessage("The ItemId is Invalid.");
            RuleFor(item => item.BillId).GreaterThan(0).WithMessage("The BillId is Invalid.");
            RuleFor(item => item.ProductId).GreaterThan(0).WithMessage("The ProductId is Invalid.");
            RuleFor(item => item.Quantity).GreaterThan(0).WithMessage("The Quantity is Invalid.");
        }
    }
}
