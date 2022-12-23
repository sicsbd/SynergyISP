namespace SynergyISP.Application;

using Domain;

/// <summary>
/// The implementation.
/// </summary>
internal class Implementation : IInterface
{
    /// <inheritdoc/>
    public string Do(string s)
    {
        return s;
    }
}
