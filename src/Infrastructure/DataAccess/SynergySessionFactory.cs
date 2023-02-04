using Marten;

namespace SynergyISP.Infrastructure;
public class SynergySessionFactory : ISessionFactory
{
    private readonly IDocumentStore _documentStore;

    public SynergySessionFactory(IDocumentStore documentStore)
    {
        _documentStore = documentStore;
    }

    /// <inheritdoc/>
    public IDocumentSession OpenSession()
    {
        return _documentStore.OpenSession();
    }

    /// <inheritdoc/>
    public IQuerySession QuerySession()
    {
        return _documentStore.QuerySession();
    }
}