using NUnit.Framework;
using SynergyISP.Tests.Core.Web;

namespace SynergyISP.Tests.Core.NUnit;

/// <summary>
/// The nunit test base.
/// </summary>
[TestFixture]
public abstract class NUnitIntegrationTestBase<TStartup> : IntegrationTestBase<TStartup>
    where TStartup : class
{
    /// <summary>
    /// Initializes the test suit.
    /// </summary>
    [OneTimeSetUp]
    public void InitializeSuit()
    {
        _appFactory = new NUnitAppFactory<TStartup>();
    }

    /// <summary>
    /// Initializes the test suit.
    /// </summary>
    [OneTimeTearDown]
    public async Task DisposeSuit()
    {
        await _appFactory!.DisposeAsync();
    }
}
