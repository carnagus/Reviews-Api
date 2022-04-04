using FluentValidation;

namespace Application.Reviews.Commands.CreateReview
{
    public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewCommandValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(120)
                .NotEmpty();
            RuleFor(v => v.Id).NotEmpty();
            RuleFor(v => v.StartDate).NotEmpty().WithMessage("Start date can not be empty");
            RuleFor(v => v.EndDate).NotEmpty().WithMessage("End date can not be empty"); ;
            RuleFor(v => v.CreateDate).NotEmpty().WithMessage("Create date can not be empty"); ;
        }
    }
}
