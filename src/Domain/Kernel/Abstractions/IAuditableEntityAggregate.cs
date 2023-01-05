namespace SynergyISP.Domain.Abstractions;

/// <summary>
/// The aggregate for auditable entity.
/// </summary>
/// <typeparam name="TKey">The type of the key.</typeparam>
public interface IAuditableEntityAggregate<TKey>
    : IAggregate
    where TKey : Id
{
    /// <summary>
    /// Populates the creation time.
    /// </summary>
    /// <returns>Entity.</returns>
    IAuditableEntityAggregate<TKey> PopulateCreateTime();

    /// <summary>
    /// Populates the current user from HttpContext upon creation.
    /// </summary>
    /// <returns>Entity.</returns>
    IAuditableEntityAggregate<TKey> PopulateCreateBy();

    /// <summary>
    /// Populates the moditied time.
    /// </summary>
    /// <returns>Entity.</returns>
    IAuditableEntityAggregate<TKey> PopulateModifiedTime();

    /// <summary>
    /// Populates the current user from HttpContext upon modification.
    /// </summary>
    /// <returns>Entity.</returns>
    IAuditableEntityAggregate<TKey> PopulateModifiedBy();

    /// <summary>
    /// Populates the creation time.
    /// </summary>
    /// <returns>Entity.</returns>
    IAuditableEntityAggregate<TKey> PopulateDeletionTime();

    /// <summary>
    /// Populates the current user from HttpContext upon deletion.
    /// </summary>
    /// <returns>Entity.</returns>
    IAuditableEntityAggregate<TKey> PopulateDeletedBy();

    /// <summary>
    /// Toggles the IsDeted flag.
    /// </summary>
    /// <returns>Entity.</returns>
    IAuditableEntityAggregate<TKey> ToggleIsDeletedFlag();
}
