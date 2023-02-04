using HotChocolate.Data.Sorting;
using SynergyISP.Application.Common.Dtos;

namespace SynergyISP.Presentation.APIs.GraphQL.Types.UserManagement.InputTypes;
public class CustomerSortInputType : SortInputType<CustomerDto>
{
    protected override void Configure(ISortInputTypeDescriptor<CustomerDto> descriptor)
    {
        base.Configure(descriptor);
        descriptor.BindFieldsExplicitly();
        descriptor.Field(d => d.FirstName);
    }
}
