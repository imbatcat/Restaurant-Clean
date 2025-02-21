using Restaurants.Domain.Entities;
using System.Linq.Expressions;

namespace Restaurants.Domain.Specifications
{
    public interface ISpecification<T> where T : class 
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> IncludeExpressions { get; }
        Expression<Func<T, object>> OrderByExpression { get; }
        Expression<Func<T, object>> OrderByDescendingExpression { get; }
    }
}