namespace SynergyISP.Infrastructure;
using Marten;
using SynergyISP.Domain.Entities;

internal sealed class SynergyRegistry : MartenRegistry
{
    public SynergyRegistry()
    {
        For<CustomerProfile>()
            .Identity(u => u.Id)
            .UniqueIndex(u => new { u.Id, u.ProfileKey, u.DataType, u.Value });
    }
}