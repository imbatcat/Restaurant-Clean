using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Applications.Restaurants.Commands.PatchRestaurants
{
    public class PatchRestaurantCommandValidator : AbstractValidator<PatchRestaurantCommand>
    {
        public PatchRestaurantCommandValidator()
        {
            RuleFor(x => x.Name)
                .Length(3, 100);
        }
    }
}
