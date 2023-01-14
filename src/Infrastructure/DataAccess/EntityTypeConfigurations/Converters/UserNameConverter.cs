namespace SynergyISP.Infrastructure.DataAccess.EntityTypeConfigurations.Converters;
using Domain.ValueObjects;
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
