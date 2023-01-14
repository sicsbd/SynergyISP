﻿using SynergyISP.Domain.Abstractions;
using SynergyISP.Domain.Entities;

namespace SynergyISP.Application.Common.Dtos;

public sealed record class CreateOrganizationalUserDto
    : CreateUserDto, IMapTo<OrganizationUser>
{

}
