namespace SynergyISP.Presentation.APIs.GraphQL.Types.UserManagement;
using Application.Common.Dtos;

public class CustomerDtoType : ObjectType<CustomerDto>
{
    /// <inheritdoc/>
    protected override void Configure(IObjectTypeDescriptor<CustomerDto> descriptor)
    {
        base.Configure(descriptor);
        descriptor
            .Field(u => u.Id)
            .Ignore();
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
    }
}
