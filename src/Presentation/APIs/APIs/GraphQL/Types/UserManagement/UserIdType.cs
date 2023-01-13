namespace SynergyISP.Presentation.APIs.GraphQL.Types.UserManagement;
using Domain.ValueObjects;
using HotChocolate.Language;

/// <summary>
/// The name type.
/// </summary>
public sealed class UserIdType : ScalarType<UserId, StringValueNode>, IInputType, IOutputType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserIdType"/> class.
    /// </summary>
    /// <param name="validator">The validator.</param>
    public UserIdType()
        : base("UserId", BindingBehavior.Implicit)
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
    protected override bool IsInstanceOfType(UserId runtimeValue)
        => runtimeValue.GetType().Equals(typeof(UserId));

    // define how a value node is parsed to the string .NET type
    /// <inheritdoc/>
    protected override UserId ParseLiteral(StringValueNode valueSyntax)
        => new (Guid.Parse(valueSyntax.Value));

    // define how the string .NET type is parsed to a value node
    /// <inheritdoc/>
    protected override StringValueNode ParseValue(UserId runtimeValue)
        => new(runtimeValue.Value.ToString());

    /// <inheritdoc/>
    public override bool TryDeserialize(
        object? resultValue,
        out object? runtimeValue)
    {
        runtimeValue = null;

        if (resultValue is string s && Guid.TryParse(s, out var id))
        {
            runtimeValue = id;
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

        if (runtimeValue is UserId id)
        {
            resultValue = id.Value;
            return true;
        }

        return false;
    }
}
