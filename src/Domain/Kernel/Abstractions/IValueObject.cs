namespace SynergyISP.Domain.Abstractions;

/// <summary>
/// The value object contract.
/// </summary>
public interface IValueObject
{
    /// <summary>
    /// Creates a new instance of Value Object.
    /// </summary>
    /// <returns>Value Object.</returns>
    public static abstract IValueObject New();
}