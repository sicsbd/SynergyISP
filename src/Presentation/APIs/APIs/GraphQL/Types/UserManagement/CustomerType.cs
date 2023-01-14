namespace SynergyISP.Presentation.APIs.GraphQL.Types.UserManagement;

using System.IO.Pipelines;
using Domain.Entities;
using HotChocolate.Resolvers;
using Marten;
using ScalarTypes;

/// <inheritdoc />
public class CustomerType : ObjectType<Customer>
{
    /// <inheritdoc />
    protected override void Configure(IObjectTypeDescriptor<Customer> descriptor)
    {
        base.Configure(descriptor);
        descriptor.Description("Represents a customer of the ISP.");
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
            .ResolveWith<CustomerProfileResolver>(r => r.GetProfile(null!, CancellationToken.None));
        descriptor
            .Field("fullName")
            .Resolve(ctx =>
            {
                Customer user = ctx.Parent<Customer>();
                return user.FirstName + user.LastName;
            });
    }

    public class CustomerProfileResolver : IDisposable
    {
        private readonly IDocumentStore _documentStore;

        public CustomerProfileResolver(
            IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public void Dispose()
        {
            _documentStore?.Dispose();
        }

        public async Task<CustomerProfile?> GetProfile(
            IResolverContext ctx,
            CancellationToken ct = default)
        {
            Customer user = ctx.Parent<Customer>();
            using IDocumentSession documentSession = _documentStore.LightweightSession();
            return await documentSession
                .Query<CustomerProfile>()
                .SingleOrDefaultAsync(x => x.Id.Equals((Guid)user.Id), token: ct!);
        }
    }
}
