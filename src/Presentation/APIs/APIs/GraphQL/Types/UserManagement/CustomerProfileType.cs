using SynergyISP.Domain.Entities;

namespace SynergyISP.Presentation.APIs.GraphQL.Types.UserManagement;
/// <summary>
/// The user profile type.
/// </summary>
public class CustomerProfileType : ObjectType<CustomerProfile>, IInputType, IOutputType
{
    /// <inheritdoc/>
    protected override void Configure(IObjectTypeDescriptor<CustomerProfile> descriptor)
    {
        base.Configure(descriptor);
        descriptor.BindFieldsExplicitly();
        descriptor.Field(f => f.Id).Type<IdType>().ID();
        descriptor.Field(f => f.UserId).Type<IdType>().ID();
        descriptor.Field(f => f.Field).Type<StringType>();
        descriptor.Field(f => f.DataType).Type<StringType>();
        descriptor
            .Field(f => f.Value)
            .Type<AnyType>()
            .Resolve(ctx =>
            {
                CustomerProfile profile = ctx.Parent<CustomerProfile>();
                return profile.DataType switch
                {
                    nameof(DateTime) => DateTime.Parse(profile.Value.ToString()!).ToLocalTime().ToString(),
                    nameof(DateTimeOffset) => DateTimeOffset.Parse(profile.Value.ToString()!).ToLocalTime().ToString(),
                    _ => profile.Value,
                };
            });
    }
}
