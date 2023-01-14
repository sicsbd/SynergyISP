namespace SynergyISP.Infrastructure;

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;
using Marten;
using Marten.Schema;

/// <summary>
/// Initial data for 
/// </summary>
public class SynergyInitialData : IInitialData
{
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="SynergyInitialData"/> class.
    /// </summary>
    /// <param name="mapper">The mapper.</param>
    public SynergyInitialData(
        IMapper mapper)
    {
        _mapper = mapper;
    }

    public static IEnumerable<OrganizationUser> InitialOrgUsers { get; set; }

    public static IEnumerable<TenantUser> InitialTenantUsers { get; set; }

    public static IEnumerable<Customer> InitialCustomers { get; set; }

    /// <summary>
    /// Gets the users.
    /// </summary>
    /// <param name="initialUsers">Initial users.</param>
    /// <returns>A list of User.</returns>
    public static IEnumerable<OrganizationUser> PopulateOrganizationUsers()
    {
        return PopulateUsers<OrganizationUser, OrganizationUserId>("rafsan", "Rafsanul", "Hasan");
    }

    /// <summary>
    /// Gets the users.
    /// </summary>
    /// <param name="initialUsers">Initial users.</param>
    /// <returns>A list of User.</returns>
    public static IEnumerable<TenantUser> PopulateTenantUsers()
    {
        return PopulateUsers<TenantUser, TenantUserId>("tuser", "Tenant", "User");
    }

    /// <summary>
    /// Gets the users.
    /// </summary>
    /// <param name="initialUsers">Initial users.</param>
    /// <returns>A list of User.</returns>
    public static IEnumerable<Customer> PopulateCustomers()
    {
        return PopulateUsers<Customer, CustomerId>("customer", "Customer", "User");
    }

    /// <summary>
    /// Gets the users profiles.
    /// </summary>
    /// <param name="initialUsers">Initial users.</param>
    /// <returns>A list of User.</returns>
    public static IEnumerable<CustomerProfile> PopulateUserProfiles()
    {
        List<CustomerProfile> profiles = new();
        for (int i = 0; i < InitialCustomers.Count(); i++)
        {
            OrganizationUser organizationUser = InitialOrgUsers.ElementAt(i);
            profiles.Add(PopulateProfile(organizationUser.Id, i));
            TenantUser tenantUser = InitialTenantUsers.ElementAt(i);
            profiles.Add(PopulateProfile(tenantUser.Id, i));
            Customer customer = InitialCustomers.ElementAt(i);
            profiles.Add(PopulateProfile(customer.Id, i));
        }

        return profiles;
    }

    /// <inheritdoc/>
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        try
        {
            using IDocumentSession session = store.LightweightSession();
            IEnumerable<CustomerProfile> userProfiles = PopulateUserProfiles();
            session.Store(userProfiles);
            await session.SaveChangesAsync(cancellation);
        }
        catch
        {

        }
    }

    /// <summary>
    /// Populates the users.
    /// </summary>
    /// <param name="userName">The user name.</param>
    /// <param name="firstName">The first name.</param>
    /// <param name="lastName">The last name.</param>
    /// <returns>A list of TUsers.</returns>
    private static IEnumerable<TUser> PopulateUsers<TUser, TKey>(
        string userName,
        string firstName,
        string lastName)
        where TUser : User<TKey>, new()
        where TKey : UserId, new()
    {
        List<TUser> initialUsers = new(5);
        for (int i = 0; i < 5; i++)
        {
            string num = i == 0 ? string.Empty : i.ToString();
            TUser user = new();

            user = (TUser)user
                .ChangeAccount(
                    new TKey(),
                    $"{userName}{num}",
                    $"{firstName}{num}",
                    $"{lastName}{num}",
                    string.Empty,
                    string.Empty,
                    "Rh123@",
                    null)
                .ToEntity();
            initialUsers.Add(user);
        }

        return initialUsers;
    }

    private static CustomerProfile PopulateProfile(Guid id, int counter)
    {
        return new()
        {
            Id = id,
            ProfileKey = "DateOfBirth",
            DataType = typeof(DateTime).Name,
            Value = DateTimeOffset.UtcNow.AddDays(counter).ToString(),
        };
    }
}