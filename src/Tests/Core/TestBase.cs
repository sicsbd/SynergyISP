using AutoFixture;
using AutoFixture.Kernel;

namespace SynergyISP.Tests.Core;

/// <summary>
/// The test base.
/// </summary>
public abstract class TestBase
{
    protected readonly Fixture _fixture = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="TestBase"/> class.
    /// </summary>
    protected TestBase()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TestBase"/> class.
    /// </summary>
    /// <param name="includedBehaviors">The included behaviors.</param>
    /// <param name="excludedBehaviors">The excluded behaviors.</param>
    protected TestBase(
        IEnumerable<ISpecimenBuilderTransformation> includedBehaviors,
        params ISpecimenBuilderTransformation[] excludedBehaviors
    )
        : this()
    {
        foreach (var behavior in excludedBehaviors)
        {
            _fixture.Behaviors.Remove(behavior);
        }
        foreach (var behavior in includedBehaviors)
        {
            _fixture.Behaviors.Add(behavior);
        }
    }

    /// <summary>
    /// Omits the recursive behavior.
    /// </summary>
    protected void OmitRecursiveBehavior()
    {
        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }
}
