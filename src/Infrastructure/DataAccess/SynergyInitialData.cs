namespace SynergyISP.Infrastructure;

using System.Threading;
using System.Threading.Tasks;
using Application.Common.Dtos;
using AutoMapper;
using Domain.Aggregates;
using Domain.Entities;
using Domain.ValueObjects;
using Marten;
using Marten.Schema;
using Marten.Services;
using SynergyISP.Domain.Abstractions;

/// <summary>
/// Initial data for 
/// </summary>
public class SynergyInitialData : IInitialData
{
    private readonly IMapper _mapper;
    private readonly IReadRepository<User<UserId>, UserId, IUserAggregateRoot> _repo;

    public SynergyInitialData(IMapper mapper, IReadRepository<User<UserId>, UserId, IUserAggregateRoot> repo)
    {
        _mapper = mapper;
        _repo = repo;
    }

    public static IEnumerable<User<UserId>> InitialUsers { get; set; }

    /// <summary>
    /// Gets the users.
    /// </summary>
    /// <param name="initialUsers">Initial users.</param>
    /// <returns>A list of User.</returns>
    public static IEnumerable<User<UserId>> PopulateUsers()
    {
        List<User<UserId>> initialUsers = new(5);
        for (int i = 0; i < 5; i++)
        {
            string num = (i + 1).ToString();
            User<UserId> user = new (new UserId());
            user = user
                .ChangeAccount($"Rafsan{num}", $"Rafsan{num}", $"Hasan{num}", string.Empty, string.Empty, "Rh123@", new UserId(), null)
                .ToEntity();
            initialUsers.Add(user);
        }
        return initialUsers;
    }

    /// <summary>
    /// Gets the users profiles.
    /// </summary>
    /// <param name="initialUsers">Initial users.</param>
    /// <returns>A list of User.</returns>
    public static IEnumerable<UserProfile> PopulateUserProfiles()
    {
        for (int i = 0; i < InitialUsers.Count(); i++)
        {
            User<UserId> user = InitialUsers.ElementAt(i);
            UserProfile profile = new()
            {
                Id = user.Id,
                ProfileKey = "DateOfBirth",
                DataType = typeof(DateTime).Name,
                Value = DateTimeOffset.UtcNow.AddDays(i).ToString(),
            };
            yield return profile;
        }
    }

    /// <inheritdoc/>
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        try
        {
            using IDocumentSession session = store.LightweightSession();
            IEnumerable<UserProfile> userProfiles = PopulateUserProfiles();
            session.Store(userProfiles);
            await session.SaveChangesAsync(cancellation);
        }
        catch
        {

        }
    }
}