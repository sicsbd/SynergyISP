namespace SynergyISP.Infrastructure;
using Marten;
using SynergyISP.Application.Common.Dtos;
using SynergyISP.Domain.Entities;

internal sealed class SynergyRegistry : MartenRegistry
{
    public SynergyRegistry()
    {
        For<UserProfile>()
            .Identity(u => u.Id)
            .UniqueIndex(u => new { u.Id, u.ProfileKey, u.DataType, u.Value });
    }
}