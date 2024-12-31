using FluentValidation;
using WebAPI.Models;

namespace WebAPI.Validators
{
    public class CityValidator : AbstractValidator<CityModel>
    {
        public CityValidator()
        {
            RuleFor(x => x.CityID);
            RuleFor(x => x.CityName)
                .NotEmpty()
                .NotNull()
                .WithMessage("City Name is Required!");

            RuleFor(x => x.CountryID)
                .NotEmpty()
                .NotNull()
                .WithMessage("Country Id is Required!");

            RuleFor(x => x.StateID)
                .NotEmpty()
                .NotNull()
                .WithMessage("State Id is Required!");

            RuleFor(x => x.CityCode)
                .NotEmpty()
                .NotNull()
                .WithMessage("City Code is Required!");

            RuleFor(x => x.CreatedDate);
            RuleFor(x => x.ModifiedDate);
        }
    }
}
