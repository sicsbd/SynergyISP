namespace SynergyISP.Application;

public interface ICacheService
{
    /// <summary>
    /// Gets the async.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="ct">The ct.</param>
    /// <returns>A T.</returns>
    public Task<T?> GetAsync<T>(string key, CancellationToken ct = default);

    /// <summary>
    /// Gets the or add async.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="data">The data.</param>
    /// <param name="ct">The ct.</param>
    /// <returns>A T.</returns>
    public Task<T> GetOrAddAsync<T>(string key, T data, CancellationToken ct = default);

    /// <summary>
    /// Sets the async.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="data">The data.</param>
    /// <param name="ct">The ct.</param>
    /// <returns>A T.</returns>
    public Task<T> SetAsync<T>(string key, T data, CancellationToken ct = default);
}
