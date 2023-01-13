namespace SynergyISP.Presentation.APIs.GraphQL.Types.UserManagement;

using System.Reflection;
using Domain.Entities;
using Domain.ValueObjects;
using Marten;

/// <inheritdoc />
public class UserEntityType : ObjectType<User<UserId>>
{
    /// <inheritdoc />
    protected override void Configure(IObjectTypeDescriptor<User<UserId>> descriptor)
    {
        base.Configure(descriptor);
        descriptor.Name("User");
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
            .Type<NonNullType<NameType>>()
            .UseFiltering<string>(d =>
            {
                d.BindFields(BindingBehavior.Explicit);
            });
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
            .Type<UserProfileType>()
            .Resolve(async (ctx, ct) =>
            {
                User<UserId> user = ctx.Parent<User<UserId>>();
                using IServiceScope scope = ctx.Services.CreateAsyncScope();
                using IDocumentStore documentStore = scope.ServiceProvider.GetRequiredService<IDocumentStore>();
                using IDocumentSession documentSession = documentStore.LightweightSession();

                return await documentSession.Query<UserProfile>().SingleOrDefaultAsync(x => x.Id.Equals((Guid)user.Id), ct);
            });
        descriptor
            .Field("fullName")
            .Resolve(ctx =>
            {
                User<UserId> user = ctx.Parent<User<UserId>>();
                return user.FirstName + user.LastName;
            });
    }
}
