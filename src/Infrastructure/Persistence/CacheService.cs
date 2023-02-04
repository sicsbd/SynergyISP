
using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using SynergyISP.Application;

namespace SynergyISP.Infrastructure.Persistence;
/// <summary>
/// The cache service.
/// </summary>
internal class CacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;

    /// <summary>
    /// Initializes a new instance of the <see cref="CacheService"/> class.
    /// </summary>
    /// <param name="distributedCache">The distributed cache.</param>
    public CacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    /// <inheritdoc/>
    public async Task<T?> GetAsync<T>(string key, CancellationToken ct = default)
    {
        byte[]? bytes = await _distributedCache.GetAsync(key, ct);
        if (bytes is null)
        {
            return default;
        }

        if (bytes.Length == 0)
        {
            return default;
        }

        string json = Encoding.UTF8.GetString(bytes);
        T? data = JsonConvert.DeserializeObject<T>(json);

        return data;
    }

    /// <inheritdoc/>
    public async Task<T> GetOrAddAsync<T>(string key, T data, CancellationToken ct = default)
    {
        T? existingData = await GetAsync<T>(key, ct);

        existingData ??= await SetAsync(key, data, ct);

        return existingData;
    }

    /// <inheritdoc/>
    public async Task<T> SetAsync<T>(string key, T data, CancellationToken ct = default)
    {
        string json = JsonConvert.SerializeObject(data);
        byte[] bytes = Encoding.UTF8.GetBytes(json);
        await _distributedCache.SetAsync(key, bytes, ct);
        return data;
    }
}
