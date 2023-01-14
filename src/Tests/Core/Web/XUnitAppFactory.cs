using Xunit;

namespace SynergyISP.Tests.Core.Web;

/// <summary>
/// A web app factory for use with xUnit.
/// </summary>
/// <typeparam name="TStartup"></typeparam>
public class XUnitAppFactory<TStartup> : CustomAppFactory<TStartup>, IAsyncLifetime
    where TStartup : class
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomAppFactory"/> class.
    /// </summary>
    public XUnitAppFactory()
    {
        BuildContainers();
    }

    /// <inheritdoc/>
    public async Task InitializeAsync()
    {
        Task tasks = Task.WhenAll(
            DbContainer!.StartAsync(),
            CacheContainer!.StartAsync(),
            QueueContainer!.StartAsync(),
            EventDbContainer!.StartAsync());
        await tasks;
    }

    /// <inheritdoc/>
    async Task IAsyncLifetime.DisposeAsync()
    {
        await DbContainer!.DisposeAsync();
        await CacheContainer!.StartAsync();
        await QueueContainer!.DisposeAsync();
        await EventDbContainer!.DisposeAsync();
    }
}
