using FluentValidation;
using Restaurants.Applications.Restaurants.Dtos;

namespace Restaurants.Applications.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
    {
        private int[] allowedPageSizes = [5, 10, 15, 30];
        private string[] allowedSortByColumnNames = [nameof(RestaurantDto.Name), nameof(RestaurantDto.Description), nameof(RestaurantDto.Category)];

        public GetAllRestaurantsQueryValidator()
        {
            RuleFor(r => r.PageNumber)
                .GreaterThanOrEqualTo(1);

            RuleFor(r => r.PageSize)
                .Must(value => allowedPageSizes.Contains(value))
                .WithMessage($"Page size must be in [{string.Join(",", allowedPageSizes)}]");

            RuleFor(r => r.SortBy)
                .Must(value => allowedSortByColumnNames.Contains(value))
                .When(q => q.SortBy != null)
                .WithMessage($"Sort by is option, or must be in [{string.Join(",", allowedSortByColumnNames)}]");
        }
    }
}