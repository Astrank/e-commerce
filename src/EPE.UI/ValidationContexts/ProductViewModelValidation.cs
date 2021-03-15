using EPE.UI.ViewModels;
using FluentValidation;

namespace EPE.UI.ValidationContexts
{
    public class ProductViewModelValidation : AbstractValidator<ProductViewModel>
    {
        public ProductViewModelValidation()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .MinimumLength(2)
                .MaximumLength(100);
            RuleFor(x => x.Description)
                .NotNull()
                .MinimumLength(2)
                .MaximumLength(100);
            RuleFor(x => x.Value)
                .NotNull();
            RuleFor(x => x.PrimaryImageFile)
                .NotNull();
        }
    }
}
