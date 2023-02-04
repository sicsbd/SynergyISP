namespace SynergyISP.Presentation.APIs.GraphQL.Types.UserManagement;
using Domain.Entities;
using ScalarTypes;
using SynergyISP.Presentation.APIs.GraphQL.DataLoaders;

/// <inheritdoc />
public partial class CustomerType : ObjectType<Customer>
{
    /// <inheritdoc />
    protected override void Configure(IObjectTypeDescriptor<Customer> descriptor)
    {
        base.Configure(descriptor);
        descriptor.Description("Represents a customer of the ISP.");
        descriptor.BindFieldsExplicitly();
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
            .Type<ListType<CustomerProfileType>>()
            .UseDataloader<CustomerProfileBatchLoader>()
            .UseDataloader<CustomerProfileCatchLoader>()
            .UsePaging()
            //.ResolveWith<CustomerProfileResolver>(r => r.GetProfile(null!, null!, null!))
            .ResolveWith<CustomerProfileResolver>(r => r.GetProfiles(default!, default!, default!))
            //.IsProjected(false)
            ;
        descriptor
            .Field("fullName")
            .Resolve(ctx =>
            {
                Customer user = ctx.Parent<Customer>();
                return user.FirstName + user.LastName;
            });
        descriptor
            .Field(c => c.CreateDate)
            .IsProjected(false);
        descriptor
            .Field(c => c.CreatedBy)
            .IsProjected(false);
        descriptor
            .Field(c => c.LastModifiedAt)
            .IsProjected(false);
        descriptor
            .Field(c => c.LastModifiedBy)
            .IsProjected(false);
        descriptor
            .Field(c => c.DeletedAt)
            .IsProjected(false);
        descriptor
            .Field(c => c.DeletedBy)
            .IsProjected(false);
        descriptor
            .Field(c => c.IsDeleted)
            .IsProjected(false);
    }
}
