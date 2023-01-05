using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Mvc.Testing;

namespace SynergyISP.Tests.Core.Web;
public abstract class CustomAppFactory<TStartup> : WebApplicationFactory<TStartup>
    where TStartup : class
{
    /// <summary>
    /// The username.
    /// </summary>
    protected const string _userName = "testUser";

    /// <summary>
    /// The username.
    /// </summary>
    protected const string _password = "P@ssw0rd";

    protected int _databasePort = Random.Shared.Next(10000, 60000);
    protected int _cachePort = Random.Shared.Next(10000, 60000);
    protected int _kafkaPort = Random.Shared.Next(10000, 60000);
    protected int _rabbitMQPort = Random.Shared.Next(10000, 60000);

    /// <summary>
    /// Builds the containers.
    /// </summary>
    protected void BuildContainers()
    {
        DbContainer = ContainerHelper
            .CreateDatabaseContainer(
                "SynergyISP",
                _userName,
                _password,
                _databasePort);
        CacheContainer = ContainerHelper
            .CreateCacheContainer(
                "A",
                _userName,
                _password,
                _cachePort);
        QueueContainer = ContainerHelper
            .CreateQueueMessageBrokerContainer(
                _userName,
                _password,
                _rabbitMQPort);
        EventDbContainer = ContainerHelper
            .CreateDatabaseContainer(
                "SynergyISPEvents",
                _userName,
                _password,
                _databasePort);
    }

    /// <summary>
    /// Gets the db container.
    /// </summary>
    public PostgreSqlTestcontainer? DbContainer { get; private set; }

    /// <summary>
    /// Gets the event source db container.
    /// </summary>
    public PostgreSqlTestcontainer? EventDbContainer { get; private set; }

    /// <summary>
    /// Gets the cache container.
    /// </summary>
    public RedisTestcontainer? CacheContainer { get; private set; }

    /// <summary>
    /// Gets the queue container.
    /// </summary>
    public RabbitMqTestcontainer? QueueContainer { get; private set; }
}
