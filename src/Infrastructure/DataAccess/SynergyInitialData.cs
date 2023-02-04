using Marten;
using Marten.Schema;
using Microsoft.Extensions.Logging;
using SynergyISP.Domain.Aggregates;
using SynergyISP.Domain.Entities;
using SynergyISP.Domain.ValueObjects;

namespace SynergyISP.Infrastructure;
/// <summary>
/// Initial data for No SQL.
/// </summary>
public class SynergyInitialData : IInitialData
{
    private readonly ILogger<SynergyInitialData> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="SynergyInitialData"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public SynergyInitialData(ILogger<SynergyInitialData> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Gets or sets the initial org users.
    /// </summary>
    public static IEnumerable<OrganizationUser> InitialOrgUsers { get; set; }

    /// <summary>
    /// Gets or sets the initial tenant users.
    /// </summary>
    public static IEnumerable<TenantUser> InitialTenantUsers { get; set; }

    /// <summary>
    /// Gets or sets the initial customers.
    /// </summary>
    public static IEnumerable<Customer> InitialCustomers { get; set; }

    /// <summary>
    /// Gets the users.
    /// </summary>
    /// <param name="initialUsers">Initial users.</param>
    /// <returns>A list of User.</returns>
    public static IEnumerable<OrganizationUser> PopulateOrganizationUsers()
    {
        return PopulateUsers<OrganizationUser, OrganizationUserId>("rafsan");
    }

    /// <summary>
    /// Gets the users.
    /// </summary>
    /// <param name="initialUsers">Initial users.</param>
    /// <returns>A list of User.</returns>
    public static IEnumerable<TenantUser> PopulateTenantUsers()
    {
        return PopulateUsers<TenantUser, TenantUserId>("tuser");
    }

    /// <summary>
    /// Gets the users.
    /// </summary>
    /// <param name="initialUsers">Initial users.</param>
    /// <returns>A list of User.</returns>
    public static IEnumerable<Customer> PopulateCustomers()
    {
        return PopulateUsers<Customer, CustomerId>("customer");
    }

    /// <summary>
    /// Gets the users profiles.
    /// </summary>
    /// <param name="initialUsers">Initial users.</param>
    /// <returns>A list of User.</returns>
    public static IEnumerable<CustomerProfile> PopulateCustomerProfiles()
    {
        PopulateCustomers();
        List<CustomerProfile> profiles = new();
        for (int i = 0; i < InitialCustomers.Count(); i++)
        {
            ////OrganizationUser organizationUser = InitialOrgUsers.ElementAt(i);
            ////profiles.AddRange(PopulateProfile(organizationUser.Id, i));
            ////TenantUser tenantUser = InitialTenantUsers.ElementAt(i);
            ////profiles.AddRange(PopulateProfile(tenantUser.Id, i));
            Customer customer = InitialCustomers.ElementAt(i);
            profiles.AddRange(PopulateProfile<CustomerProfile, Customer, CustomerId>(customer.Id, i));
        }

        return profiles;
    }

    /// <summary>
    /// Gets the users profiles.
    /// </summary>
    /// <param name="initialUsers">Initial users.</param>
    /// <returns>A list of User.</returns>
    public static IEnumerable<CustomerProfile> PopulateOrgUserProfiles()
    {
        //List<OrganizationUserProfile> profiles = new();
        //for (int i = 0; i < InitialCustomers.Count(); i++)
        //{
        //    OrganizationUser organizationUser = InitialOrgUsers.ElementAt(i);
        //    profiles.AddRange(PopulateProfile < (organizationUser.Id, i));
        //    ////TenantUser tenantUser = InitialTenantUsers.ElementAt(i);
        //    ////profiles.AddRange(PopulateProfile(tenantUser.Id, i));
        //}

        //return profiles;
        return Enumerable.Empty<CustomerProfile>();
    }

    /// <inheritdoc/>
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        try
        {
            using IDocumentSession session = store.DirtyTrackedSession();
            session.EjectAllOfType(typeof(CustomerProfile));
            await session.SaveChangesAsync(cancellation);
            List<CustomerProfile> userProfiles = PopulateCustomerProfiles().ToList();
            userProfiles.ForEach(p =>
            {
                session.Store(p);
            });
            await session.SaveChangesAsync(cancellation);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }

    /// <summary>
    /// Populates the users.
    /// </summary>
    /// <param name="userName">The user name.</param>
    /// <returns>A list of TUsers.</returns>
    private static IEnumerable<TUser> PopulateUsers<TUser, TKey>(
        string userName)
        where TUser : User<TKey>, new()
        where TKey : UserId, new()
    {
        List<TUser> initialUsers = new(100);
        for (int i = 0; i < 100; i++)
        {
            string num = i == 0 ? string.Empty : i.ToString();
            TUser user = new();

            user = (TUser)user
                .ChangeAccount(
                    new TKey(),
                    $"{userName}{num}",
                    Faker.Name.First(),
                    Faker.Name.Last(),
                    Faker.Name.First(),
                    Faker.Name.First(),
                    "Rh123@",
                    null)
                .ToEntity();
            initialUsers.Add(user);
        }

        return initialUsers;
    }

    /// <summary>
    /// Populates the profile.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="counter">The counter.</param>
    /// <returns>A CustomerProfile.</returns>
    private static List<TProfile> PopulateProfile<TProfile, TUser, TKey>(Guid id, int counter)
        where TProfile : UserProfileAggregate<TUser, TKey>, new()
        where TUser : User<TKey>
        where TKey : UserId
    {
        TProfile profile1 = new()
        {
            UserId = id,
            Field = "DateOfBirth",
            DataType = typeof(DateTimeOffset).Name,
            Value = DateTimeOffset.UtcNow.AddDays(counter),
        };

        TProfile profile2 = new()
        {
            UserId = id,
            Field = "DateOfBirth2",
            DataType = typeof(DateTimeOffset).Name,
            Value = DateTimeOffset.UtcNow.AddDays(counter + 1),
        };

        return new() { profile1, profile2 };
    }
}
