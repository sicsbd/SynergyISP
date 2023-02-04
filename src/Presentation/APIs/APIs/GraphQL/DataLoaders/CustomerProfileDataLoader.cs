using System.Collections.ObjectModel;
using EFCoreSecondLevelCacheInterceptor;
using Marten;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SynergyISP.Application;
using SynergyISP.Domain.Entities;

namespace SynergyISP.Presentation.APIs.GraphQL.DataLoaders;
public class CustomerProfileBatchLoader : BatchDataLoader<Guid, ReadOnlyCollection<CustomerProfile>>
{
    private bool _disposedValue;
    private readonly IQuerySession _querySession;
    private readonly ICacheService _cache;
    private readonly CustomerProfileCatchLoader _catchLoader;
    private readonly CustomerProfileCatchBatchLoader _catchBatchLoader;

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomerProfileBatchLoader"/> class.
    /// </summary>
    /// <param name="sessionFactory">The session factory.</param>
    /// <param name="batchScheduler">The batch scheduler.</param>
    /// <param name="cache">The cache.</param>
    /// <param name="catchLoader">The catch loader.</param>
    /// <param name="options">The options.</param>
    public CustomerProfileBatchLoader(
        ISessionFactory sessionFactory,
        IBatchScheduler batchScheduler,
        ICacheService cache,
        CustomerProfileCatchLoader catchLoader,
        CustomerProfileCatchBatchLoader catchBatchLoader,
        DataLoaderOptions? options = null)
        : base(batchScheduler, options)
    {
        _querySession = sessionFactory.QuerySession();
        _cache = cache;
        _catchLoader = catchLoader;
        _catchBatchLoader = catchBatchLoader;
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (!_disposedValue)
        {
            if (disposing)
            {
                _querySession?.Dispose();
            }

            _disposedValue = true;
        }
    }

    /// <inheritdoc/>
    protected override async Task<IReadOnlyDictionary<Guid, ReadOnlyCollection<CustomerProfile>>> LoadBatchAsync(
        IReadOnlyList<Guid> keys,
        CancellationToken cancellationToken)
    {
        Dictionary<Guid, ReadOnlyCollection<CustomerProfile>> resultantProfiles = new();

        IReadOnlyList<ReadOnlyCollection<CustomerProfile>> cachedProfiles = await _catchBatchLoader
                .LoadAsync(keys, cancellationToken)
                .ConfigureAwait(false);
        if (cachedProfiles is not null && cachedProfiles.Count > 0)
        {
            foreach (ReadOnlyCollection<CustomerProfile> customerProfiles in cachedProfiles)
            {
                if (customerProfiles != null && customerProfiles.Any(p => p is not null))
                {
                    resultantProfiles.Add(customerProfiles.First().UserId, customerProfiles);
                }
            }
        }

        //foreach (Guid key in keys)
        //{
        //    ReadOnlyCollection<CustomerProfile>? cachedProfiles = await _catchLoader
        //        .LoadAsync(key, cancellationToken)
        //        .ConfigureAwait(false);
        //    if (cachedProfiles is not null && cachedProfiles.Count > 0)
        //    {
        //        resultantProfiles.Add(key, cachedProfiles);
        //    }
        //}

        if (resultantProfiles.Count > 0)
        {
            return resultantProfiles;
        }

        IReadOnlyList<CustomerProfile> profiles = await _querySession
                                                    .Query<CustomerProfile>()
                                                    .Where(x => keys.ToList().Contains(x.UserId))
                                                    .ToListAsync(cancellationToken);
        IEnumerable<IGrouping<Guid, CustomerProfile>> profileGroup = profiles
            .GroupBy(x => x.UserId);
        foreach (var group in profileGroup)
        {
            var anonObj = new
            {
                group.Key,
                Profiles = group.ToList().AsReadOnly(),
            };
            await _cache.GetOrAddAsync($"Profile_{anonObj.Key}", anonObj.Profiles, cancellationToken);
            resultantProfiles.Add(group.Key, group.ToList().AsReadOnly());
        }

        string keysString = string.Join("+", keys);
        await _cache.GetOrAddAsync($"Profile_{keysString}", profiles, cancellationToken);

        return resultantProfiles;
    }
}
