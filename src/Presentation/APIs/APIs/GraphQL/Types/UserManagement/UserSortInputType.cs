namespace SynergyISP.Presentation.APIs.GraphQL.Types.UserManagement;
using HotChocolate.Data.Sorting;
using SynergyISP.Application.Common.Dtos;

public class UserSortInputType : SortInputType<UserDto>
{
    protected override void Configure(ISortInputTypeDescriptor<UserDto> descriptor)
    {
        base.Configure(descriptor);
        descriptor.BindFieldsImplicitly();
    }
}
