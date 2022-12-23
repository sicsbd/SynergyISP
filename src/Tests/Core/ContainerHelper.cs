using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;

namespace SynergyISP.Tests.Core;
public static class ContainerHelper
{
    /// <summary>
    /// Creates the database container.
    /// </summary>
    /// <param name="databaseName">The database name.</param>
    /// <param name="userName">The database user name.</param>
    /// <param name="password">The database user password.</param>
    /// <returns>A PostgreSqlTestcontainer.</returns>
    public static PostgreSqlTestcontainer CreateDatabaseContainer(
        string databaseName,
        string userName,
        string password,
        int port)
    {
        return new TestcontainersBuilder<PostgreSqlTestcontainer>()
                .WithDatabase(new PostgreSqlTestcontainerConfiguration()
                {
                    Database = databaseName,
                    Username = userName,
                    Password = password,
                })
                .WithEnvironment("POSTGRES_USER", "DbTester")
                .WithEnvironment("POSTGRES_PASSWORD", "P@ssw0rd")
                .WithEnvironment("POSTGRES_DB", databaseName)
                .WithPortBinding(port, 5432)
                .WithCleanUp(true)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5432))
                .Build();
    }

    /// <summary>
    /// Creates the cache container.
    /// </summary>
    /// <param name="databaseName">The database name.</param>
    /// <param name="userName">The database user name.</param>
    /// <param name="password">The database user password.</param>
    /// <returns>A PostgreSqlTestcontainer.</returns>
    public static RedisTestcontainer CreateCacheContainer(
        string databaseName,
        string userName,
        string password,
        int port)
    { 
        return new TestcontainersBuilder<RedisTestcontainer>()
                .ConfigureContainer(c =>
                {
                    c.Database = databaseName;
                    c.Username = userName;
                    c.Password = password;
                })
                .WithCleanUp(true)
                .WithPortBinding(port, 6379)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5432))
                .Build();
    }

    /// <summary>
    /// Creates the RabbitMQ container.
    /// </summary>
    /// <param name="databaseName">The database name.</param>
    /// <param name="userName">The database user name.</param>
    /// <param name="password">The database user password.</param>
    /// <returns>A PostgreSqlTestcontainer.</returns>
    public static RabbitMqTestcontainer CreateQueueMessageBrokerContainer(
        string userName,
        string password,
        int port)
    {
        return new TestcontainersBuilder<RabbitMqTestcontainer>()
                .WithMessageBroker(new RabbitMqTestcontainerConfiguration()
                {
                    Username = userName,
                    Password = password,
                    Port = port,
                })
                .WithCleanUp(true)
                .WithPortBinding(port, 6379)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5432))
                .Build();
    }

    /// <summary>
    /// Creates the Kafka container.
    /// </summary>
    /// <param name="databaseName">The database name.</param>
    /// <param name="userName">The database user name.</param>
    /// <param name="password">The database user password.</param>
    /// <returns>A PostgreSqlTestcontainer.</returns>
    public static KafkaTestcontainer CreateMessageBrokerContainer(
        string databaseName,
        string userName,
        string password,
        int port)
    {
        return new TestcontainersBuilder<KafkaTestcontainer>()
                .WithMessageBroker(new KafkaTestcontainerConfiguration()
                {
                    Username = userName,
                    Password = password,
                    Port = port,
                })
                .WithCleanUp(true)
                .WithPortBinding(port, 6379)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5432))
                .Build();
    }
}
