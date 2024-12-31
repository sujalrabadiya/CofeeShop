using FluentValidation;
using WebAPI.Models;

namespace WebAPI.Validators
{
    public class CountryValidator : AbstractValidator<CountryModel>
    {
        public CountryValidator()
        {
            RuleFor(x => x.CountryName)
                .NotEmpty()
                .NotNull()
                .WithMessage("Country Name is Required!");

            RuleFor(x => x.CountryCode)
                .NotEmpty()
                .NotNull()
                .WithMessage("Country Code is Required!");
        }
    }
}
