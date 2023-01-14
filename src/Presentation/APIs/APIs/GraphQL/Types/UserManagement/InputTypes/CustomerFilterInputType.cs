namespace SynergyISP.Presentation.APIs.GraphQL.Types.UserManagement.InputTypes;
using HotChocolate.Data.Filters;
using SynergyISP.Application.Common.Dtos;

/// <inheritdoc />
public class CustomerFilterInputType : FilterInputType<CustomerDto>
{
    /// <inheritdoc />
    protected override void Configure(IFilterInputTypeDescriptor<CustomerDto> descriptor)
    {
        base.Configure(descriptor);
        descriptor.BindFieldsImplicitly();
    }
}
