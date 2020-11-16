using FluentValidation;
using RetailStoreAPI.Models;

namespace RetailStoreAPI.Validators
{

    public class BillItemNewValidator : AbstractValidator<BillItemNewModel>
    {
        public BillItemNewValidator()
        {
            RuleFor(x => x.ProductId).NotNull().GreaterThan(0).WithMessage("The ProductId is Invalid.");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("The Quantity is Invalid.");
        }
    }
    public class BillNewValidator : AbstractValidator<BillNewModel>
    {
        public BillNewValidator()
        {
            RuleFor(bill => bill.OperatorId).GreaterThan(0).WithMessage("The EmployeeId is Invalid.");
            RuleForEach(bill => bill.BillItems).SetValidator(new BillItemNewValidator());
        }
    }
}
