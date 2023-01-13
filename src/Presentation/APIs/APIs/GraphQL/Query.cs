namespace SynergyISP.Presentation.APIs.GraphQL;

using Application.Common.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Abstractions;
using Domain.Aggregates;
using Domain.Entities;
using Domain.ValueObjects;
using HotChocolate.Data.Projections.Expressions;
using SynergyISP.Presentation.APIs.GraphQL.Types.UserManagement;

public class Query
{
    // [UseDbContext]
    // [UsePaging]
    // [UseProjection]
    // [UseFiltering]
    // [UseSorting]
    // [UseSelection]
    public IQueryable<User<UserId>> GetUsers(
        [Service] IReadRepository<User<UserId>, UserId, IUserAggregateRoot> repo,
        [Service] IReadDataContext dataContext,
        [Service] IMapper mapper)
    {
        return dataContext.Set<User<UserId>>();
    }
}
