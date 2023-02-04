
namespace SynergyISP.Presentation.APIs.GraphQL.Types.UserManagement;
using Domain.Entities;
using Marten;
using ScalarTypes;

/// <inheritdoc />
public class TenantUserType : ObjectType<TenantUser>
{
    /// <inheritdoc />
    protected override void Configure(IObjectTypeDescriptor<TenantUser> descriptor)
    {
        base.Configure(descriptor);
        descriptor
            .Field(u => u.Id)
            .Name("id")
            .Type<UserIdType>();
        descriptor
            .Field(u => u.UserName)
            .Name("userName")
            .Type<NonNullType<UserNameType>>();
        descriptor
            .Field(u => u.FirstName)
            .Name("firstName")
            .Type<NonNullType<NameType>>();
        descriptor
            .Field(u => u.LastName)
            .Name("lastName")
            .Type<NameType>();
        descriptor
            .Field(u => u.DisplayName)
            .Name("displayName")
            .Type<NameType>();
        descriptor
            .Field(u => u.NickName)
            .Name("nickName")
            .Type<NameType>();
        descriptor
            .Field(u => u.Profile)
            .Type<CustomerProfileType>()
            .Resolve(async (ctx, ct) =>
            {
                TenantUser user = ctx.Parent<TenantUser>();
                using IServiceScope scope = ctx.Services.CreateAsyncScope();
                using IDocumentStore documentStore = scope.ServiceProvider.GetRequiredService<IDocumentStore>();
                using IDocumentSession documentSession = documentStore.LightweightSession();

                return await documentSession.Query<CustomerProfile>().SingleOrDefaultAsync(x => x.Id.Equals((Guid)user.Id), ct);
            });
        descriptor
            .Field("fullName")
            .Resolve(ctx =>
            {
                TenantUser user = ctx.Parent<TenantUser>();
                return user.FirstName + user.LastName;
            });
    }
}
