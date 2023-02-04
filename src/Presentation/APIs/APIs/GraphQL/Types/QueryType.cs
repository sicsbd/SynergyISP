using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net;
using SynergyISP.Domain.Abstractions;
using SynergyISP.Domain.Entities;
using SynergyISP.Presentation.APIs.GraphQL.Helpers;
using SynergyISP.Presentation.APIs.GraphQL.Types.UserManagement;
using SynergyISP.Presentation.APIs.GraphQL.Types.UserManagement.InputTypes;

namespace SynergyISP.Presentation.APIs.GraphQL.Types;
public class QueryType : ObjectType<Query>
{
    /// <inheritdoc/>
    protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
    {
        descriptor
            .Field(q => q.GetCustomers(default!, default!, default!))
            .Type<ListType<CustomerType>>()
            .UseReadDataContext<IReadDataContext>()
            .UsePaging()
            .UseProjection()
            .UseFiltering<CustomerFilterInputType>()
            .UseSorting<CustomerSortInputType>()
            ;

        //descriptor
        //    .Field(q => q.GetCustomers(null!, default!, default!, default!))
        //    .Name("GetCustomersByName")
        //    .Argument("name", d => d.Type<StringType>())
        //    .Type<ListType<CustomerType>>()
        //    .UsePaging()
        //    .UseProjection<CustomerType>()
        //    .UseFiltering<CustomerFilterInputType>()
        //    .UseSorting<CustomerSortInputType>()
        //    .ResolveWith<Query>(q => q.GetCustomers(null!, null!, null!, null!));
        base.Configure(descriptor);
    }
}
