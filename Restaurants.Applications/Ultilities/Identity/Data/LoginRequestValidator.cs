using FluentValidation;

namespace Restaurants.Applications.Ultilities.Identity.Data
{
    internal class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            //RuleFor(rq => rq.Email)
            //    .EmailAddress()
            //    .WithMessage("Invalid Email");
            //RuleFor(rq => rq.Password)
            //    .Matches(@"(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{6,}")
            //    .WithMessage("Invalid Password must be at least 6 characters, at least 1 special character (@#$%^&*) and at least 1 number");
        }
    }
}