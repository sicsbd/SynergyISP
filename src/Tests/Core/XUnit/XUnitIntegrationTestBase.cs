using SynergyISP.Tests.Core.Web;
using Xunit;

namespace SynergyISP.Tests.Core.XUnit;

/// <summary>
/// The xunit test base.
/// </summary>
public abstract class XUnitIntegrationTestBase<TStartup>
    : IntegrationTestBase<TStartup>, IClassFixture<TStartup>
    where TStartup : class
{
    /// <inheritdoc/>
    public async Task DisposeAsync()
    {
        await _appFactory!.DisposeAsync();
    }

    /// <inheritdoc/>
    public Task InitializeAsync()
    {
        _appFactory = new XUnitAppFactory<TStartup>();
        return Task.CompletedTask;
    }
}
