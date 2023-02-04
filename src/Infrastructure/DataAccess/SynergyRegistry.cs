
using Marten;
using SynergyISP.Domain.Entities;

namespace SynergyISP.Infrastructure;
/// <summary>
/// The synergy registry.
/// </summary>
internal sealed class SynergyRegistry : MartenRegistry
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SynergyRegistry"/> class.
    /// </summary>
    public SynergyRegistry()
    {
        For<CustomerProfile>()
            .Identity(u => u.Id)
            .UniqueIndex(u => new { u.UserId, u.Field, u.Value })
            .SoftDeletedWithIndex()
            .UseOptimisticConcurrency(true);
    }
}