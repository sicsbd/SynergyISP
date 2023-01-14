namespace SynergyISP.Presentation.APIs.GraphQL.Types.UserManagement;
using Application.Common.Dtos;
public class CreateCustomerType
    : InputObjectType<CreateCustomerDto>
{
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
