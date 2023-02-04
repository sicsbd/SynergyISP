﻿using System.Collections.ObjectModel;
using Newtonsoft.Json;
using SynergyISP.Application;
using SynergyISP.Domain.Entities;

namespace SynergyISP.Presentation.APIs.GraphQL.DataLoaders;
public class CustomerProfileCatchLoader : CacheDataLoader<Guid, ReadOnlyCollection<CustomerProfile>?>
{
    private bool _disposedValue;
    private readonly ICacheService _cacheService;
    private readonly ILogger<CustomerProfileCatchLoader> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomerProfileCatchLoader"/> class.
    /// </summary>=
    /// <param name="cacheService">The cache service.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="options">The options.</param>
    public CustomerProfileCatchLoader(
        ICacheService cacheService,
        ILogger<CustomerProfileCatchLoader> logger,
        DataLoaderOptions? options = null)
        : base(options)
    {
        _cacheService = cacheService;
        _logger = logger;
    }

    /// <inheritdoc/>
    protected override async Task<ReadOnlyCollection<CustomerProfile>?> LoadSingleAsync(
        Guid key,
        CancellationToken cancellationToken)
    {
        string profileKey = $"Profile_{key}";

        _logger.LogInformation("Getting customer profile: '{profileKey}' from cache", profileKey);
        List<CustomerProfile>? profiles = await _cacheService
            .GetAsync<List<CustomerProfile>>(profileKey, cancellationToken);
        _logger.LogInformation("Fetched customer profile: '{profileKey}' from cache:{newLine}{data}", profileKey, Environment.NewLine, JsonConvert.SerializeObject(profiles));

        return profiles?.AsReadOnly()
            ?? Enumerable.Empty<CustomerProfile>().ToList().AsReadOnly();
    }
}
