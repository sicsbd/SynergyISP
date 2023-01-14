namespace SynergyISP.Presentation.APIs.GraphQL.Types;

using SynergyISP.Presentation.APIs.GraphQL.Types.UserManagement.InputTypes;
using UserManagement;

public class QueryType : ObjectType<Query>
{
    /// <inheritdoc/>
    protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
    {
        base.Configure(descriptor);
        descriptor
            .Field(q => q.GetCustomers(default!, default!, default!))
            .Type<ListType<CustomerType>>()
            .UsePaging()
            //.UseProjection()
            .UseFiltering<CustomerFilterInputType>()
            .UseSorting<CustomerSortInputType>()
            ;
    }
}
