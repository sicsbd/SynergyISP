using SynergyISP.Application.Common.Dtos;

namespace SynergyISP.Presentation.APIs.GraphQL.Types.UserManagement;
public class CreateCustomerType
    : InputObjectType<CreateCustomerDto>
{
    /// <inheritdoc/>
    protected override void Configure(IInputObjectTypeDescriptor<CreateCustomerDto> descriptor)
    {
        descriptor.Field(u => u.Id).Type<IdType>();
        descriptor
            .Field(u => u.UserName)
            .Type<NonNullType<StringType>>();
        descriptor
            .Field(u => u.FirstName)
            .Type<NonNullType<StringType>>();
        descriptor
            .Field(u => u.LastName)
            .Type<StringType>();
        descriptor
            .Field(u => u.DisplayName)
            .Type<StringType>();
        descriptor
            .Field(u => u.NickName)
            .Type<StringType>();
        base.Configure(descriptor);
    }
}
