namespace SynergyISP.Tests.Core.Web;

/// <summary>
/// A web app factory for use with NUnit.
/// </summary>
/// <typeparam name="TStartup"></typeparam>
public class NUnitAppFactory<TStartup> : CustomAppFactory<TStartup>
    where TStartup : class
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomAppFactory"/> class.
    /// </summary>
    public NUnitAppFactory()
    {
        BuildContainers();
    }

    /// <summary>
    /// Starts all the containers.
    /// </summary>
    /// <returns></returns>
    public async Task InitializeAsync()
    {
        Task tasks = Task.WhenAll(
            DbContainer!.StartAsync(),
            CacheContainer!.StartAsync(),
            QueueContainer!.StartAsync(),
            EventDbContainer!.StartAsync());
        await tasks;
    }

    /// <summary>
    /// Stops and removes all containers.
    /// </summary>
    /// <returns></returns>
    public override async ValueTask DisposeAsync()
    {
        await DbContainer!.DisposeAsync();
        await CacheContainer!.StartAsync();
        await QueueContainer!.DisposeAsync();
        await EventDbContainer!.DisposeAsync();
        await base.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}