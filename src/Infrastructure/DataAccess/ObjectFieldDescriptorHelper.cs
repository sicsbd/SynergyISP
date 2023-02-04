namespace SynergyISP.Presentation.APIs.GraphQL.Helpers;
using Domain.Abstractions;
using HotChocolate.Types;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class ObjectFieldDescriptorHelper
{
    public static IObjectFieldDescriptor UseReadDataContext<TContext>(
        this IObjectFieldDescriptor field)
        where TContext : IReadDataContext
    {
        field.UseScopedService<IReadDataContext>(
            create: sp => sp.GetRequiredService<IDbContextFactory<DataContext>>().CreateDbContext(),
            disposeAsync: async (_, ctx) => await ctx.DisposeAsync());
        return field;
    }

    public static IObjectFieldDescriptor UseWriteDataContext<TContext>(
        this IObjectFieldDescriptor field)
        where TContext : IWriteDataContext
    {
        field.UseScopedService<IWriteDataContext>(
            create: sp => sp.GetRequiredService<IDbContextFactory<DataContext>>().CreateDbContext(),
            disposeAsync: async (_, ctx) => await ctx.DisposeAsync());
        return field;
    }
}
