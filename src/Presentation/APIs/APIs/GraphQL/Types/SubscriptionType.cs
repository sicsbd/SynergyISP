namespace SynergyISP.Presentation.APIs.GraphQL.Types;
using HotChocolate.Execution;

using HotChocolate.Subscriptions;
using SynergyISP.Domain.Entities;
using SynergyISP.Domain.ValueObjects;
using UserManagement;

public class SubscriptionType : ObjectType
{
    /// <inheritdoc/>
    protected override void Configure(IObjectTypeDescriptor descriptor)
    {
        base.Configure(descriptor);
        descriptor
            .Field("userAdded")
            .Type<UserEntityType>()
            .Resolve(ctx => ctx.GetEventMessage<User<UserId>>())
            .Subscribe(async context =>
            {
                var receiver = context.Service<ITopicEventReceiver>();

                ISourceStream stream =
                    await receiver.SubscribeAsync<string, User<UserId>>("userAdded");

                return stream;
            });
    }
}
