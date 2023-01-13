namespace SynergyISP.Presentation.APIs.GraphQL.Types.UserManagement;

using Domain.Entities;
using Domain.ValueObjects;
using HotChocolate.Data.Filters;
using SynergyISP.Application.Common.Dtos;

/// <inheritdoc />
public class UserFilterInputType : FilterInputType<UserDto>
{
    /// <inheritdoc />
    protected override void Configure(IFilterInputTypeDescriptor<UserDto> descriptor)
    {
        base.Configure(descriptor);
        descriptor.BindFieldsImplicitly();
    }
}
