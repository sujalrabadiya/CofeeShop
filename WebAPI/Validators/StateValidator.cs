using FluentValidation;
using WebAPI.Models;

namespace WebAPI.Validators
{
    public class StateValidator : AbstractValidator<StateModel>
    {
        public StateValidator()
        {
            RuleFor(x => x.StateName)
                .NotEmpty()
                .NotNull()
                .WithMessage("State Name is Required!");

            RuleFor(x => x.CountryID)
                .NotEmpty()
                .NotNull()
                .WithMessage("Country Id is Required!");

            RuleFor(x => x.StateCode)
                .NotEmpty()
                .NotNull()
                .WithMessage("State Code is Required!");
        }
    }
}
