using NUnit.Framework;
using SynergyISP.Tests.Core.Web;

namespace SynergyISP.Tests.Core;

/// <summary>
/// The nunit test base.
/// </summary>
[TestFixture]
public abstract class IntegrationTestBase<TStartup> : TestBase
    where TStartup : class
{
    protected CustomAppFactory<TStartup>? _appFactory;
}
