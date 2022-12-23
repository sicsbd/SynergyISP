using Xunit;

namespace SynergyISP.Tests.Core.XUnit;

/// <summary>
/// The xunit test base.
/// </summary>
public abstract class XUnitTestBase : TestBase, IAsyncLifetime
{
    /// <inheritdoc/>
    public abstract Task DisposeAsync();

    /// <inheritdoc/>
    public abstract Task InitializeAsync();
}
