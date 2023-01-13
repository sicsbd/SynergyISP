namespace SynergyISP.Infrastructure;
using Newtonsoft.Json;

/// <summary>
/// The database settings.
/// </summary>
public class DatabaseSettings
{
    public int? CommandTimeout { get; set; }
    public int MaxRetryCount { get; set; }
    public int RetryDelay { get; set; }
    public bool? UseRedShift { get; set; }

    [JsonIgnore]
    public TimeSpan MaxRetryDelay => TimeSpan.FromSeconds(RetryDelay);
}
