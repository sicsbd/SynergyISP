namespace SynergyISP.Presentation.APIs.GraphQL.Types.UserManagement;
using Domain.ValueObjects;
using HotChocolate.Language;

/// <summary>
/// The name type.
/// </summary>
public sealed class NameType : ScalarType<Name, StringValueNode>, IInputType, IOutputType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NameType"/> class.
    /// </summary>
    /// <param name="validator">The validator.</param>
    public NameType()
        : base("Name", BindingBehavior.Implicit)
    {
        Description = "Represents a name";
    }

    /// <inheritdoc/>
    public override IValueNode ParseResult(object? resultValue)
        => ParseValue(resultValue);

    // is another StringValueNode an instance of this scalar
    protected override bool IsInstanceOfType(StringValueNode valueSyntax)
        => IsInstanceOfType(valueSyntax.Value);

    // is another string .NET type an instance of this scalar
    protected override bool IsInstanceOfType(Name runtimeValue)
        => runtimeValue.GetType().Equals(typeof(Name));

    // define how a value node is parsed to the string .NET type
    /// <inheritdoc/>
    protected override Name ParseLiteral(StringValueNode valueSyntax)
        => new (valueSyntax.Value);

    // define how the string .NET type is parsed to a value node
    /// <inheritdoc/>
    protected override StringValueNode ParseValue(Name runtimeValue)
        => new (runtimeValue.Value);

    /// <inheritdoc/>
    public override bool TryDeserialize(
        object? resultValue,
        out object? runtimeValue)
    {
        runtimeValue = null;

        if (resultValue is string s)
        {
            runtimeValue = s;
            return true;
        }

        return false;
    }

    /// <inheritdoc/>
    public override bool TrySerialize(
        object? runtimeValue,
        out object? resultValue)
    {
        resultValue = null;

        if (runtimeValue is Name name)
        {
            resultValue = name.Value;
            return true;
        }

        return false;
    }
}
