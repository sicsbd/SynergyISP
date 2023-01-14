namespace SynergyISP.Infrastructure.DataAccess.EntityTypeConfigurations.Comparers;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.ChangeTracking;

internal class UserNameComparer : ValueComparer<UserName>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserNameComparer"/> class.
    /// </summary>
    public UserNameComparer()
        : base(
            (v1, v2) => v1!.Equals(v2),
            v => v.GetHashCode())
    {
    }
}
