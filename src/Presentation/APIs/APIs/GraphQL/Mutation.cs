namespace SynergyISP.Presentation.APIs.GraphQL;

using Application.Common.Dtos;
using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

public class Mutation
{
    public async Task<User<UserId>> CreateUser(
        CreateUserDto user,
        [Service] IMapper mapper,
        [Service] IWriteDataContext dataCtx,
        [Service] ITopicEventSender sender,
        CancellationToken cancellationToken)
    {
        IExecutionStrategy executionStrategy = dataCtx.Database.CreateExecutionStrategy();
        user = user with
        {
            Id = new UserId(),
        };
        User<UserId> entity = mapper.Map<User<UserId>>(user);
        await dataCtx.Set<User<UserId>>().AddAsync(entity, cancellationToken);
        await executionStrategy.ExecuteAsync(
            dataCtx,
            operation: async (_, ctx, ct2) => (await ctx.SaveChangesAsync(false, ct2)) >= 1,
            verifySucceeded: async (_, ctx, ct3) => new ExecutionResult<bool>(await ctx.Set<User<UserId>>().AnyAsync(u => u.UserName.Equals(user.UserName), ct3), true),
            cancellationToken);
        await sender.SendAsync("userAdded", entity, cancellationToken).ConfigureAwait(false);
        return entity;
    }
}
