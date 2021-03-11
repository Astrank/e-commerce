using EPE.UI.ViewModels;
using FluentValidation;

namespace EPE.UI.ValidationContexts
{
    public class ProjectViewModelValidation : AbstractValidator<ProjectViewModel>
    {
        public ProjectViewModelValidation()
        {
            RuleFor(x => x.Title)
                .NotNull()
                .MinimumLength(2)
                .MaximumLength(100);
            RuleFor(x => x.Description)
                .NotNull()
                .MinimumLength(2)
                .MaximumLength(100);
            RuleFor(x => x.Tags)
                .NotNull();
            RuleFor(x => x.Image)
                .NotNull();
        }
    }
}
