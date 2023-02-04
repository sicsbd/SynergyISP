using System.Collections.ObjectModel;
using Newtonsoft.Json;
using SynergyISP.Application;
using SynergyISP.Domain.Entities;

namespace SynergyISP.Presentation.APIs.GraphQL.DataLoaders;

public class CustomerProfileCatchBatchLoader : BatchDataLoader<Guid, ReadOnlyCollection<CustomerProfile>>
{
    private bool _disposedValue;
    private readonly ICacheService _cacheService;
    private readonly ILogger<CustomerProfileCatchLoader> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomerProfileCatchBatchLoader"/> class.
    /// </summary>=
    /// <param name="cacheService">The cache service.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="options">The options.</param>
    public CustomerProfileCatchBatchLoader(
        ICacheService cacheService,
        ILogger<CustomerProfileCatchLoader> logger,
        IBatchScheduler batchScheduler,
        DataLoaderOptions? options = null)
        : base(batchScheduler, options)
    {
        _cacheService = cacheService;
        _logger = logger;
    }

    /// <inheritdoc/>>
    protected override async Task<IReadOnlyDictionary<Guid, ReadOnlyCollection<CustomerProfile>>> LoadBatchAsync(
        IReadOnlyList<Guid> keys,
        CancellationToken cancellationToken)
    {
        string keysString = string.Join("+", keys);
        string profileKey = $"Profile_{keysString}";

        _logger.LogInformation("Getting customer profile: '{profileKey}' from cache", profileKey);
        List<CustomerProfile>? profiles = await _cacheService
            .GetAsync<List<CustomerProfile>>(profileKey, cancellationToken);
        _logger.LogInformation("Fetched customer profile: '{profileKey}' from cache:{newLine}{data}", profileKey, Environment.NewLine, JsonConvert.SerializeObject(profiles));

        return profiles?.GroupBy(p => p.UserId).ToDictionary(s => s.Key, s => s.ToList().AsReadOnly())
            ?? new Dictionary<Guid, ReadOnlyCollection<CustomerProfile>>();
    }
}
