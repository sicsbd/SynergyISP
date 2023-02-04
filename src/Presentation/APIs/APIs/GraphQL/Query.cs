
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreSecondLevelCacheInterceptor;
using Microsoft.EntityFrameworkCore;
using SynergyISP.Application.Common.Dtos;
using SynergyISP.Domain.Abstractions;
using SynergyISP.Domain.Aggregates;
using SynergyISP.Domain.Entities;
using SynergyISP.Domain.ValueObjects;

namespace SynergyISP.Presentation.APIs.GraphQL;
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
        [ScopedService] IReadDataContext dataContext,
        [Service] IMapper mapper)
    {
        return dataContext.Set<Customer>().Cacheable();
    }

    public IQueryable<CustomerDto> GetCustomers(
        string? name,
        [Service] IReadRepository<Customer, CustomerId, IUserAggregateRoot<Customer, CustomerId>> repo,
        [Service] IReadDataContext dataContext,
        [Service] IMapper mapper)
    {
        IQueryable<Customer> query = dataContext.Set<Customer>();

        return query.ProjectTo<CustomerDto>(mapper.ConfigurationProvider).Cacheable();
    }
}
