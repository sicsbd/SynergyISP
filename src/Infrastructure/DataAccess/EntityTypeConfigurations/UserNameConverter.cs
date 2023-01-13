namespace SynergyISP.Infrastructure.DataAccess.EntityTypeConfigurations;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
internal class UserNameConverter : ValueConverter<UserName, string>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserNameConverter"/> class.
    /// </summary>
    public UserNameConverter()
        : base(
            v => v.Value,
            v => new UserName(v),
            new ConverterMappingHints(500, unicode: true))
    {
    }
}

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
