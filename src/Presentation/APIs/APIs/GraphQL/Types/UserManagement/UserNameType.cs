namespace SynergyISP.Presentation.APIs.GraphQL.Types.UserManagement;
using Domain.ValueObjects;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Sorting;
using HotChocolate.Language;

/// <summary>
/// The name type.
/// </summary>
public sealed class UserNameType : ScalarType<UserName, StringValueNode>, IInputType, IOutputType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserNameType"/> class.
    /// </summary>
    /// <param name="validator">The validator.</param>
    public UserNameType()
        : base("UserName", BindingBehavior.Implicit)
    {
        Description = "Represents a user name";
    }

    /// <inheritdoc/>
    public override IValueNode ParseResult(object? resultValue)
        => ParseValue(resultValue);

    // is another StringValueNode an instance of this scalar
    protected override bool IsInstanceOfType(StringValueNode valueSyntax)
        => IsInstanceOfType(valueSyntax.Value);

    // is another string .NET type an instance of this scalar
    protected override bool IsInstanceOfType(UserName runtimeValue)
        => runtimeValue.GetType().Equals(typeof(Name));

    // define how a value node is parsed to the string .NET type
    /// <inheritdoc/>
    protected override UserName ParseLiteral(StringValueNode valueSyntax)
        => new (valueSyntax.Value);

    // define how the string .NET type is parsed to a value node
    /// <inheritdoc/>
    protected override StringValueNode ParseValue(UserName runtimeValue)
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

        if (runtimeValue is UserName name)
        {
            resultValue = name.Value;
            return true;
        }

        return false;
    }
}
