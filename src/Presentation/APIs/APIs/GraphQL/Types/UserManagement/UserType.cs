namespace SynergyISP.Presentation.APIs.GraphQL.Types.UserManagement;
using Application.Common.Dtos;
using FluentValidation.Validators;

public class UserType : ObjectType<UserDto>
{
    protected override void Configure(IObjectTypeDescriptor<UserDto> descriptor)
    {
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
        base.Configure(descriptor);
    }
}
