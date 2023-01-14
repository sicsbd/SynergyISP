namespace SynergyISP.Presentation.APIs.GraphQL.Types;
using UserManagement;

public class MutationType : ObjectType<Mutation>
{
    /// <inheritdoc/>
    protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
    {
        descriptor.Name(OperationTypeNames.Mutation);
        descriptor
            .Field(m => m.CreateUser(default!, default!, default!, default!, default!))
            .Type<CustomerType>()
            .ResolveWith<Mutation>(m => m.CreateUser(default!, default!, default!, default!, default!));
        base.Configure(descriptor);
    }
}
