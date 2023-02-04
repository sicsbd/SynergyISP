
using System.Collections.ObjectModel;
using HotChocolate.Resolvers;
using Marten;
using Newtonsoft.Json;
using SynergyISP.Domain.Entities;
using SynergyISP.Presentation.APIs.GraphQL.DataLoaders;

namespace SynergyISP.Presentation.APIs.GraphQL.Types.UserManagement;
/// <summary>
/// The customer type.
/// </summary>W
public partial class CustomerType
{
    /// <summary>
    /// The customer profile resolver.
    /// </summary>
    public class CustomerProfileResolver : IDisposable
    {
        private bool _disposedValue;

        /// <summary>
        /// Gets the profile.
        /// </summary>
        /// <param name="ctx">The ctx.</param>
        /// <param name="sessionFactory">The document session factory.</param>
        /// <returns>An IQueryable.</returns>
        public async Task<IReadOnlyList<CustomerProfile>> GetProfile(
            IResolverContext ctx,
            [Service] ISessionFactory sessionFactory,
            [Service] ILogger<CustomerProfileResolver> logger)
        {
            Customer customer = ctx.Parent<Customer>();
            using IQuerySession session = sessionFactory.QuerySession();
            logger.LogInformation("Getting profile for user {@UserId}", customer.Id);
            IReadOnlyList<CustomerProfile> result = await session
                //.Query<CustomerProfile>()
                .QueryAsync<CustomerProfile>("CAST(data ->> 'userId' as uuid) = ?", (Guid)customer.Id)

                ;
            logger.LogInformation("Result: {@Data}", JsonConvert.SerializeObject(result));
            return result;
        }

        /// <summary>
        /// Gets the profiles.
        /// </summary>
        /// <param name="context">The resolverc ontext.</param>
        /// <param name="dataLoader">The data loader.</param>
        /// <param name="cancellation">The cancellation.</param>
        /// <returns>A Task.</returns>
        public async Task<List<CustomerProfile>> GetProfiles(
            IResolverContext context,
            CustomerProfileBatchLoader dataLoader,
            CancellationToken cancellation = default)
        {
            Guid customerId = context.Parent<Customer>().Id;
            IReadOnlyList<ReadOnlyCollection<CustomerProfile>> customerProfiles = await dataLoader
                .LoadAsync(new[] { customerId }, cancellation);
            return customerProfiles.SelectMany(x => x).ToList();
        }

        /// <summary>
        /// Frees up memory.
        /// </summary>
        /// <param name="disposing">If true, disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    //_dataLoader?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~CustomerProfileResolver()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        /// <inheritdoc/>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
