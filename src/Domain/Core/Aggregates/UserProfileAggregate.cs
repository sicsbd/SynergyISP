using SynergyISP.Domain.Entities;
using SynergyISP.Domain.ValueObjects;

namespace SynergyISP.Domain.Aggregates;
/// <summary>
/// The user profile aggregate.
/// </summary>
public abstract record class UserProfileAggregate<TUser, TKey>
    : IUserProfileAggregate<TUser, TKey>
    where TUser : User<TKey>
    where TKey : UserId
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserProfileAggregate"/> class.
    /// </summary>
    /// <param name="firstName">The first name.</param>
    protected UserProfileAggregate()
    {
    }

    /// <inheritdoc/>
    public virtual Guid Id { get; init; }

    /// <inheritdoc/>
    public virtual Guid UserId { get; init; }

    /// <inheritdoc/>
    public virtual string Field { get; init; } = null!;

    /// <inheritdoc/>
    public virtual string DataType { get; init; } = null!;

    /// <inheritdoc/>
    public virtual object Value { get; init; } = null!;

    /// <inheritdoc/>
    public IUserProfileAggregate<TUser, TKey> ChangeInfo<T>(
        string key,
        T value,
        TKey? id = null)
        where T : notnull
    {
        return id is not null
            ? this with
            {
                UserId = id.Value,
                Field = key,
                DataType = typeof(T).Name,
                Value = value,
            }
            : this with
            {
                UserId = Guid.NewGuid(),
                Field = key,
                DataType = typeof(T).Name,
                Value = value,
            };
    }

    /// <inheritdoc/>
    public void ResolveDependencies(IServiceProvider serviceProvider)
    {
    }
}
