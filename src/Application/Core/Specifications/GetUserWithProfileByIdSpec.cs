using System.Linq.Expressions;
using SynergyISP.Domain.Abstractions;
using SynergyISP.Domain.Entities;
using SynergyISP.Domain.Enumns;
using SynergyISP.Domain.ValueObjects;

namespace SynergyISP.Application.Specifications;
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
