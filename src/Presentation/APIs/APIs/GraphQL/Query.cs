namespace SynergyISP.Presentation.APIs.GraphQL;

using AutoMapper;
using Domain.Abstractions;
using Domain.Aggregates;
using Domain.Entities;
using Domain.ValueObjects;

public class Query
{
    // [UseDbContext]
    // [UsePaging]
    // [UseProjection]
    // [UseFiltering]
    // [UseSorting]
    // [UseSelection]
    public IQueryable<Customer> GetCustomers(
        [Service] IReadRepository<Customer, CustomerId, IUserAggregateRoot<Customer, CustomerId>> repo,
        [Service] IReadDataContext dataContext,
        [Service] IMapper mapper)
    {
        return dataContext.Set<Customer>();
    }
}
