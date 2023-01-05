namespace SynergyISP.Domain.Abstractions;

/// <summary>
/// The aggregate.
/// </summary>
public interface IAggregate
{
    /// <summary>
    /// Resolve all dependencies.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    void ResolveDependencies(IServiceProvider serviceProvider);
}
