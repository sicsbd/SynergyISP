namespace SynergyISP.Application.Specifications;
using System.Linq.Expressions;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Enumns;
using Domain.ValueObjects;
public record class GetUserWithProfileByIdSpec
    : ISpecification<User<UserId>, UserId>
{
    public GetUserWithProfileByIdSpec(UserId id)
    {
        FilterClauses = new List<Expression<Func<User<UserId>, bool>>>()
        {
            u => u.Id == id,
        };
        OrderByClauses = new Dictionary<Expression<Func<User<UserId>, object>>, SortOrder>()
        {
            { u => u.FirstName, SortOrder.Ascending },
        };
    }

    public IReadOnlyList<Expression<Func<User<UserId>, bool>>> FilterClauses { get; private set; }

    public IReadOnlyDictionary<Expression<Func<User<UserId>, object>>, SortOrder> OrderByClauses { get; private set; }

    public IReadOnlyList<Expression<Func<User<UserId>, object>>> IncludeClauses { get; private set; }
}
