namespace SynergyISP.Presentation.APIs.GraphQL.Types.UserManagement;
using SynergyISP.Domain.Entities;

/// <summary>
/// The user profile type.
/// </summary>
public class CustomerProfileType : ObjectType<CustomerProfile>
{
    /// <inheritdoc/>
    protected override void Configure(IObjectTypeDescriptor<CustomerProfile> descriptor)
    {
        descriptor
            .Field(u => u.Id)
            .Ignore();
        descriptor
            .Field(u => u.ProfileKey)
            .Type<NonNullType<StringType>>();
        descriptor
            .Field(u => u.DataType)
            .Type<StringType>();
        descriptor
            .Field(u => u.Value)
            .Type<StringType>()
            .Resolve(ctx =>
            {
                CustomerProfile profile = ctx.Parent<CustomerProfile>();
                string dataType = profile.DataType;
                return dataType.Equals(typeof(DateTime).Name)
                    ? DateTimeOffset.Parse(profile.Value).ToLocalTime().ToString()
                    : profile.Value;
            });
        base.Configure(descriptor);
    }
}
