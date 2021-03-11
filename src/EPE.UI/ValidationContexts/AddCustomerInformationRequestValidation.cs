using EPE.Application.Cart;
using FluentValidation;

namespace EPE.UI.ValidationContexts
{
    public class AddCustomerInformationRequestValidation : AbstractValidator<AddCustomerInformation.Request>
    {
        public AddCustomerInformationRequestValidation()
        {
            RuleFor(x => x.FirstName)
                .NotNull()
                .MinimumLength(2)
                .MaximumLength(100);
            RuleFor(x => x.LastName)
                .NotNull()
                .MinimumLength(2)
                .MaximumLength(100);
            RuleFor(x => x.Email)
                .NotNull()
                .EmailAddress();
            RuleFor(x => x.PhoneNumber)
                .NotNull()
                .MinimumLength(7);
            RuleFor(x => x.City)
                .NotNull()
                .MinimumLength(2)
                .MaximumLength(100);
            RuleFor(x => x.Address1)
                .NotNull()
                .MinimumLength(2)
                .MaximumLength(100);
            RuleFor(x => x.Address2)
                .MinimumLength(2)
                .MaximumLength(100);
            RuleFor(x => x.PostCode)
                .NotNull();
        }
    }
}