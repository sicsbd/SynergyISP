namespace SynergyISP.Presentation.APIs.GraphQL.Types.Scalars;

public class KeyValuePairType
    : ObjectType<KeyValuePair<string, object>>
{
    /// <inheritdoc/>
    protected override void Configure(IObjectTypeDescriptor<KeyValuePair<string, object>> descriptor)
    {
        base.Configure(descriptor);
        descriptor.BindFieldsImplicitly();
        descriptor.Field(d => d.Value).Type<AnyType>();
    }
}