namespace SynergyISP.Infrastructure.DataAccess.EntityTypeConfigurations.Comparers;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.ChangeTracking;

internal class NameComparer : ValueComparer<Name>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NameComparer"/> class.
    /// </summary>
    public NameComparer()
        : base(
            (v1, v2) => v1!.Equals(v2),
            v => v.GetHashCode())
    {
    }
}
