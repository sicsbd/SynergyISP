using AutoFixture;
using NUnit.Framework;
using Shouldly;
using SynergyISP.Domain;
using SynergyISP.Tests.Core.NUnit;

namespace SynergyISP.Application.UnitTests;

[TestFixture]
public class ImplementationTest : NUnitTestBase<IInterface>
{
    [SetUp]
    public void Setup()
    {
        this.service = new Implementation();
    }

    [Test]
    public void DoTest()
    {
        // Arrange
        var s = this._fixture.Create<string>();

        // Act
        var result = this.service.Do(s);

        // Assert
        result.ShouldBe(s);
    }
}
