using NUnit.Framework;

namespace SynergyISP.Tests.Core.NUnit;

/// <summary>
/// The nunit test base.
/// </summary>
[TestFixture]
public abstract class NUnitTestBase : TestBase
{
}

/// <summary>
/// The n unit test base.
/// </summary>
public abstract class NUnitTestBase<TService> : NUnitTestBase
    where TService : class
{
    protected TService service;
}
