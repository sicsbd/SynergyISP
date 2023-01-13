using HotChocolate.Data.Filters;
using Microsoft.EntityFrameworkCore;
using SynergyISP.Application;
using SynergyISP.Application.Common.Dtos;
using SynergyISP.Domain.Abstractions;
using SynergyISP.Domain.Entities;
using SynergyISP.Domain.ValueObjects;
using SynergyISP.Infrastructure;
using SynergyISP.Presentation.APIs.GraphQL.Types;
using SynergyISP.Presentation.APIs.GraphQL.Types.UserManagement;

var builder = WebApplication.CreateBuilder(args);

IServiceCollection services = builder.Services;
IConfiguration configuration = builder.Configuration;
IHostEnvironment hostEnvironment = builder.Environment;

services.AddAuthentication();
services.AddAuthorization();
services
    .AddGraphQLServer()
    .ModifyOptions(o => o.DefaultBindingBehavior = BindingBehavior.Explicit)
    .AddAuthorization()
    .AddFiltering(d =>
    {
        d.UseQueryableProvider();
        d.AddDefaultOperations();
        d.AllowAnd().AllowOr();
        d.BindDefaultTypes();
        //d.BindRuntimeType<UserId, FilterInputType<UserId>>();
        //d.BindRuntimeType<Name, FilterInputType<UserName>>();
        //d.BindRuntimeType<UserName, UserNameFilterInputType>();
        d.BindRuntimeType<UserDto, UserFilterInputType>();
    })
    .AddProjections()
    .AddSorting(d =>
    {
        d.UseQueryableProvider();
        d.AddDefaultOperations();
        d.BindDefaultTypes();
        d.BindRuntimeType<UserDto, UserSortInputType>();
    })
    .AddQueryableCursorPagingProvider()
    .AddDefaultTransactionScopeHandler()
    //.AddMutationConventions()
    .AddQueryFieldToMutationPayloads()
    .AddQueryType<QueryType>()
    .AddMutationType<MutationType>()
    .AddSubscriptionType<SubscriptionType>()
    .AddType<UserEntityType>()
    .AddType<UserType>()
    .AddType<UserProfileType>()
    .AddType<UserEntityType>()
    .AddType<CreateUserType>()
    .AddType<UserIdType>()
    .AddType<SynergyISP.Presentation.APIs.GraphQL.Types.UserManagement.NameType>()
    .AddType<UserNameType>()
    //.BindRuntimeType<UserId, StringType>()
    //.AddTypeConverter<UserId, string>(x => x)
    //.BindRuntimeType<Name, StringType>()
    //.AddTypeConverter<Name, string>(x => x)
    //.BindRuntimeType<UserName, StringType>()
    //.AddTypeConverter<UserName, string>(x => x.Value)
    ;
services.AddInMemorySubscriptions();

services.AddApplication(new[] { typeof(DatabaseSettings).Assembly, typeof(Program).Assembly });
services.AddInfrastructure(hostEnvironment, configuration, "SynergyRelational", "SynergyNonRelational");

using var app = builder.Build();

bool isDevelopment = app.Environment.IsDevelopment();
if (isDevelopment)
{
    IWriteDataContext ctx = app.Services.GetRequiredService<IWriteDataContext>();
    await ctx.Database.EnsureDeletedAsync();
    await ctx.Database.MigrateAsync();
    IEnumerable<User<UserId>> initialUsers = SynergyInitialData.PopulateUsers();
    ctx.Set<User<UserId>>().AddRange(initialUsers);
    SynergyInitialData.InitialUsers = initialUsers;
    ctx.SaveChanges();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseWebSockets();

app.MapGraphQL();

await app.RunAsync();
