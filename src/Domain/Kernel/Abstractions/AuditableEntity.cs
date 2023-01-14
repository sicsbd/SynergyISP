namespace SynergyISP.Domain.Abstractions;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using ValueObjects;

public abstract record class AuditableEntity<TKey>
    : IEntity<TKey>, IAuditableEntityAggregate<TKey>
    where TKey : Id
{
    private IHttpContextAccessor? _httpContextAccessor;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="AuditableEntity{TKey}"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    protected AuditableEntity(TKey id)
    {
        Id = id;
    }

    /// <inheritdoc/>
    public virtual TKey Id { get; protected init; }

    /// <summary>
    /// Gets or inits the create date.
    /// </summary>
    public DateTimeOffset CreateDate { get; protected init; }

    /// <summary>
    /// Gets or inits the CreatedBy.
    /// </summary>
    public UserName? CreatedBy { get; protected init; }

    /// <summary>
    /// Gets or inits the modified date.
    /// </summary>
    public DateTimeOffset? LastModifiedAt { get; protected init; }

    /// <summary>
    /// Gets or inits the LastModifiedBy.
    /// </summary>
    public UserName? LastModifiedBy { get; protected init; }

    /// <summary>
    /// Gets or inits a value indicating whether is deleted.
    /// </summary>
    public bool? IsDeleted { get; protected init; }

    /// <summary>
    /// Gets or inits the deleted at.
    /// </summary>
    public DateTimeOffset? DeletedAt { get; protected init; }

    /// <summary>
    /// Gets or inits the deleted by.
    /// </summary>
    public UserName? DeletedBy { get; protected init; }

    /// <inheritdoc/>
    public IAuditableEntityAggregate<TKey> PopulateCreateBy()
        => this with { CreatedBy = GetCurrentUserName() ?? string.Empty };

    /// <inheritdoc/>
    public IAuditableEntityAggregate<TKey> PopulateCreateTime()
        => this with { CreateDate = DateTimeOffset.UtcNow };

    /// <inheritdoc/>
    public IAuditableEntityAggregate<TKey> PopulateDeletedBy()
        => this with { DeletedBy = GetCurrentUserName() ?? string.Empty };

    /// <inheritdoc/>
    public IAuditableEntityAggregate<TKey> PopulateDeletionTime()
        => this with { DeletedAt = DateTimeOffset.UtcNow };

    /// <inheritdoc/>
    public IAuditableEntityAggregate<TKey> PopulateModifiedBy()
        => this with { LastModifiedBy = GetCurrentUserName() ?? string.Empty };

    /// <inheritdoc/>
    public IAuditableEntityAggregate<TKey> PopulateModifiedTime()
        => this with { LastModifiedAt = DateTimeOffset.UtcNow };

    /// <inheritdoc/>
    public IAuditableEntityAggregate<TKey> ToggleIsDeletedFlag()
        => this with { IsDeleted = true };

    /// <inheritdoc />
    public virtual void ResolveDependencies(IServiceProvider serviceProvider)
    {
        _httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
    }

    /// <inheritdoc />
    public virtual Task ResolveDependenciesAsync(IServiceProvider serviceProvider)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Gets the current user name.
    /// </summary>
    /// <returns>A string.</returns>
    private string? GetCurrentUserName()
    {
        return _httpContextAccessor?.HttpContext.User.Identity?.Name;
    }

    /// <inheritdoc />
    public virtual AuditableEntity<TKey> ToEntity()
    {
        return this;
    }
}
