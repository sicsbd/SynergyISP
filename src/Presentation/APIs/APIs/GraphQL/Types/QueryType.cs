namespace SynergyISP.Presentation.APIs.GraphQL.Types;
using UserManagement;

public class QueryType : ObjectType<Query>
{
    /// <inheritdoc/>
    protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
    {
        base.Configure(descriptor);
        descriptor
            .Field(q => q.GetUsers(default!, default!, default!))
            .Type<ListType<UserEntityType>>()
            .UsePaging()
            //.UseProjection<UserEntityType>()
            .UseFiltering<UserFilterInputType>()
            .UseSorting<UserSortInputType>()
            ;
    }
}
