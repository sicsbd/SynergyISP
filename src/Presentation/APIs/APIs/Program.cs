using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SynergyISP.Application;
using SynergyISP.Application.Common.Dtos;
using SynergyISP.Domain.Abstractions;
using SynergyISP.Domain.Entities;
using SynergyISP.Infrastructure;
using SynergyISP.Presentation.APIs;
using SynergyISP.Presentation.APIs.GraphQL.DataLoaders;
using SynergyISP.Presentation.APIs.GraphQL.Types;
using SynergyISP.Presentation.APIs.GraphQL.Types.UserManagement;
using SynergyISP.Presentation.APIs.GraphQL.Types.UserManagement.InputTypes;
using SynergyISP.Presentation.APIs.GraphQL.Types.UserManagement.ScalarTypes;

var builder = WebApplication.CreateBuilder(args);

IServiceCollection services = builder.Services;
IConfiguration configuration = builder.Configuration;
IHostEnvironment hostEnvironment = builder.Environment;
CancellationTokenSource cts = new();

services.AddAuthentication();
services.AddAuthorization();
services
    .AddGraphQLServer()
    .ModifyOptions(o => o.DefaultBindingBehavior = BindingBehavior.Explicit)
    .AddAuthorization()
    .AddFiltering(desc =>
    {
        desc
            .AddDefaultOperations()
            .BindDefaultTypes()
            //.AddSoundsLike()
            ;
        desc
            .UseQueryableProvider()
            //.UseSoundsLike()
            ;
        desc.AllowAnd().AllowOr();
        //desc.BindRuntimeType<UserId, FilterInputType<UserId>>();
        //desc.BindRuntimeType<Name, FilterInputType<UserName>>();
        //desc.BindRuntimeType<UserName, UserNameFilterInputType>();
        desc.BindRuntimeType<CustomerDto, CustomerFilterInputType>();
    })
    .AddProjections()
    .AddSorting(d =>
    {
        d.AddDefaultOperations();
        d.BindDefaultTypes();
        d.UseQueryableProvider();
        d.AddDefaults();
        d.BindRuntimeType<CustomerDto, CustomerSortInputType>();
    })
    .AddQueryableCursorPagingProvider()
    .AddDefaultTransactionScopeHandler()
    //.AddMutationConventions()
    .AddQueryFieldToMutationPayloads()
    .AddQueryType<QueryType>()
    .AddMutationType<MutationType>()
    .AddSubscriptionType<SubscriptionType>()
    //.AddType<OrgUserType>()
    //.AddType<TenantUserType>()
    .AddType<CustomerType>()
    .AddType<CustomerDtoType>()
    .AddType<CustomerProfileType>()
    //.AddType<UserEntityType>()
    .AddType<CreateCustomerType>()
    .AddType<UserIdType>()
    .AddType<SynergyISP.Presentation.APIs.GraphQL.Types.UserManagement.ScalarTypes.NameType>()
    .AddType<UserNameType>()
    .BindRuntimeType<object, AnyType>()
    //.BindRuntimeType<UserId, StringType>()
    //.AddTypeConverter<UserId, string>(x => x)
    //.BindRuntimeType<Name, StringType>()
    //.AddTypeConverter<Name, string>(x => x)
    //.BindRuntimeType<UserName, StringType>()
    //.AddTypeConverter<UserName, string>(x => x.Value)
    .AddDataLoader<CustomerProfileBatchLoader>()
    .AddDataLoader<CustomerProfileCatchLoader>()
    .AddDataLoader<CustomerProfileCatchBatchLoader>()
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
    IExecutionStrategy executionStrategy = ctx.Database.CreateExecutionStrategy();
    IEnumerable<OrganizationUser> initialOrgUsers = SynergyInitialData.PopulateOrganizationUsers();
    IEnumerable<TenantUser> initialTenantUsers = SynergyInitialData.PopulateTenantUsers();
    IEnumerable<Customer> initialCustomers = SynergyInitialData.PopulateCustomers();
    Task ouAddTask = ctx.Set<OrganizationUser>().AddRangeAsync(initialOrgUsers);
    Task tuAddTask = ctx.Set<TenantUser>().AddRangeAsync(initialTenantUsers);
    Task cAddTask = ctx.Set<Customer>().AddRangeAsync(initialCustomers);
    await Task.WhenAll(ouAddTask, tuAddTask, cAddTask);
    await executionStrategy
        .ExecuteAsync(
            ctx,
            operation: async (_, ctx, ct2) => (await ctx.SaveChangesAsync(false, ct2)) >= 1,
            verifySucceeded: async (_, ctx, ct3) => new ExecutionResult<bool>(await ctx.Set<Customer>().AnyAsync(u => initialCustomers.Any(c => u.UserName.Equals(c.UserName)), ct3), true),
            cts.Token);
    SynergyInitialData.InitialOrgUsers = initialOrgUsers.ToList();
    SynergyInitialData.InitialTenantUsers = initialTenantUsers.ToList();
    SynergyInitialData.InitialCustomers = initialCustomers.ToList();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseWebSockets();

app.MapGraphQL();
app.UseETag();

await app.RunAsync(cts.Token);
