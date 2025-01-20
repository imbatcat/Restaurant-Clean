using FluentValidation;
using Restaurants.Applications.Restaurants.Dtos;

namespace Restaurants.Applications.Restaurants.Commands.CreateRestaurants
{
    public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
    {
        private readonly List<string> validCategories = ["Italian", "Mexican", "Japanese", "American", "Indian"];
        public CreateRestaurantCommandValidator()
        {
            RuleFor(x => x.Name)
                .Length(3, 100);

            RuleFor(x => x.Category)
                //simple method
                .Must(category => validCategories.Contains(category))
                .WithMessage("Invalid category. Please choose from the valid categories");

            //More complicated
            //.Custom((value, context) =>
            //{
            //    var isValidCategory = validCategories.Contains(value);
            //    if (!isValidCategory)
            //    {
            //        context.AddFailure("Category", "Invalid category. Please choose a valid one.");
            //    }
            //});

            //NotEmpty is not needed if theres nullable expression 
            //RuleFor(x => x.Description)
            //    .NotEmpty()
            //    .WithMessage("Description is required.");

            //RuleFor(x => x.Category)
            //    .NotEmpty()
            //    .WithMessage("Insert a valid category.");

            RuleFor(x => x.Contactnumber)
                .Matches(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$")
                .WithMessage("Please provide a valid phone number");
            RuleFor(x => x.ContactEmail)
                .EmailAddress()
                .WithMessage("Category is required.");

            RuleFor(x => x.PostalCode)
                .Matches(@"^\d{2}-\d{3}$")
                .WithMessage("Please provide a valid postal code (XX-XXX)");
        }
    }
}
