using SynergyISP.Domain.Abstractions;
using SynergyISP.Domain.Entities;

namespace SynergyISP.Application.Common.Dtos;

public sealed record class CreateTenantUserDto
    : CreateUserDto, IMapTo<TenantUser>
{
    /// <summary>
    /// Gets or inits the tenant id.
    /// </summary>
    public string TenantId { get; init; } = null!;
}
