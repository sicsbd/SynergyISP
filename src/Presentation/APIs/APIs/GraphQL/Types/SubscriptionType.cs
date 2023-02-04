using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using SynergyISP.Domain.Entities;
using SynergyISP.Presentation.APIs.GraphQL.Types.UserManagement;

namespace SynergyISP.Presentation.APIs.GraphQL.Types;
public class SubscriptionType : ObjectType
{
    /// <inheritdoc/>
    protected override void Configure(IObjectTypeDescriptor descriptor)
    {
        base.Configure(descriptor);
        descriptor
            .Field("userAdded")
            .Type<CustomerType>()
            .Resolve(ctx => ctx.GetEventMessage<Customer>())
            .Subscribe(async context =>
            {
                var receiver = context.Service<ITopicEventReceiver>();

                ISourceStream stream =
                    await receiver.SubscribeAsync<string, Customer>("userAdded");

                return stream;
            });
    }
}
