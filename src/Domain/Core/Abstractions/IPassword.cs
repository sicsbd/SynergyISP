namespace SynergyISP.Domain.Abstractions;
/// <summary>
/// The password.
/// </summary>

public interface IPassword<TPassword> : IValueObject, IEquatable<string>
    where TPassword : IPassword<TPassword>
{
    public static abstract implicit operator TPassword(string password);

    public static abstract bool operator ==(
        TPassword password,
        string otherPassword);

    public static abstract bool operator !=(
        TPassword password,
        string otherPassword);
}
