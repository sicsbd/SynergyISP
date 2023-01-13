namespace SynergyISP.Presentation.APIs.GraphQL.Types;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SynergyISP.Application.Common.Dtos;
using SynergyISP.Domain.Abstractions;
using SynergyISP.Domain.Entities;
using SynergyISP.Domain.ValueObjects;

using SynergyISP.Infrastructure.DataAccess;
using UserManagement;

public class MutationType : ObjectType<Mutation>
{
    /// <inheritdoc/>
    protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
    {
        descriptor.Name(OperationTypeNames.Mutation);
        descriptor
            .Field(m => m.CreateUser(default!, default!, default!, default!, default!))
            .Type<UserEntityType>()
            .ResolveWith<Mutation>(m => m.CreateUser(default!, default!, default!, default!, default!));
        base.Configure(descriptor);
    }
}
