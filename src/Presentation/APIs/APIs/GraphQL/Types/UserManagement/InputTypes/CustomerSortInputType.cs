namespace SynergyISP.Presentation.APIs.GraphQL.Types.UserManagement.InputTypes;
using HotChocolate.Data.Sorting;
using SynergyISP.Application.Common.Dtos;

public class CustomerSortInputType : SortInputType<CustomerDto>
{
    protected override void Configure(ISortInputTypeDescriptor<CustomerDto> descriptor)
    {
        base.Configure(descriptor);
        descriptor.BindFieldsImplicitly();
    }
}
