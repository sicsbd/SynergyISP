namespace SynergyISP.Infrastructure.DataAccess.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SynergyISP.Domain.Abstractions;

internal class ReadRepository<TEntity, TKey, TAggregateRoot>
    : IReadRepository<TEntity, TKey, TAggregateRoot>
    where TEntity : class, IEntity<TKey>, IAggregateRoot<TEntity, TKey>
    where TKey : Id
    where TAggregateRoot : class, IAggregateRoot<TEntity, TKey>
{
    private readonly IConfigurationProvider _configurationProvider;
    private readonly IMapper _mapper;

    protected IReadDataContext DataContext { get; private set; }
    protected DbSet<TEntity> Set { get; private set; }

    public ReadRepository(
        IReadDataContext dataContext,
        IConfigurationProvider configurationProvider,
        IMapper mapper)
    {
        DataContext = dataContext;
        Set = DataContext.Set<TEntity>();
        _configurationProvider = configurationProvider;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public IEnumerable<TAggregateRoot> GetAll()
    {
        return Set.Select(e => e as TAggregateRoot).AsEnumerable();
    }

    public IAsyncEnumerable<TAggregateRoot> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Set.Select(e => e as TAggregateRoot).AsAsyncEnumerable();
    }

    /// <inheritdoc/>
    public Task<List<TAggregateRoot>> GetAllListAsync(CancellationToken cancellationToken = default)
    {
        return Set.Select(e => e as TAggregateRoot).ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public IEnumerable<TAggregateRoot> GetAll(Expression<Func<TEntity, bool>> predicate)
    {
        return Set.Where(predicate).Select(e => e as TAggregateRoot).AsEnumerable();
    }

    /// <inheritdoc/>
    public IAsyncEnumerable<TAggregateRoot> GetAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return Set.Where(predicate).Select(e => e as TAggregateRoot).AsAsyncEnumerable(cancellationToken);
    }

    /// <inheritdoc/>
    public Task<IList<TAggregateRoot>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return Set.Where(predicate).Select(e => e as TAggregateRoot).ToListAsync(cancellationToken);
    }

    public IEnumerable<TAggregateRoot> GetAll<TReturnType>()
    {
        return Set.ProjectTo<TReturnType>(_configurationProvider, parameters: null).AsEnumerable();
    }

    /// <inheritdoc/>
    public IAsyncEnumerable<TReturnType> GetAllAsync<TReturnType>(CancellationToken cancellationToken = default)
    {
        return Set.ProjectTo<TReturnType>(_configurationProvider, parameters: null).AsAsyncEnumerable();
    }

    /// <inheritdoc/>
    public Task<IList<TReturnType>> GetAllListAsync<TReturnType>(CancellationToken cancellationToken = default)
    {
        return Set.ProjectTo<TReturnType>(_configurationProvider, parameters: null).ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public IEnumerable<TReturnType> GetAll<TReturnType>(Expression<Func<TEntity, bool>> predicate)
    {
        return Set.Where(predicate).ProjectTo<TReturnType>(_configurationProvider, parameters: null).AsAsyncEnumerable();
    }

    /// <inheritdoc/>
    public IAsyncEnumerable<TReturnType> GetAllAsync<TReturnType>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return Set.Where(predicate).ProjectTo<TReturnType>(_configurationProvider, parameters: null).ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public Task<IList<TReturnType>> GetAllListAsync<TReturnType>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return Set.Where(predicate).ProjectTo<TReturnType>(_configurationProvider, parameters: null).ToListAsync(cancellationToken);
    }

    #region Single
    public TAggregateRoot Get(TKey key)
    {
        return Set.Select(e => e as TAggregateRoot).First(e => e.Id == key);
    }

    public TAggregateRoot GetSingle(TKey key)
    {
        return Set.Select(e => e as TAggregateRoot).Single(e => e.Id == key);
    }

    public TAggregateRoot Get(Expression<Func<TEntity, bool>> predicate)
    {
        return Set.First(predicate);
    }

    public TAggregateRoot GetSingle(Expression<Func<TEntity, bool>> predicate)
    {
        return Set.Select(e => e as TAggregateRoot).Single(predicate);
    }

    public Task<TAggregateRoot> GetAsync(TKey key, CancellationToken cancellationToken = default)
    {
        return Set.Select(e => e as TAggregateRoot).FirstAsync(e => e.Id == key, cancellationToken);
    }

    public Task<TAggregateRoot> GetSingleAsync(TKey key, CancellationToken cancellationToken = default)
    {
        return Set.Select(e => e as TAggregateRoot).SingleAsync(e => e.Id == key, cancellationToken);
    }

    public Task<TAggregateRoot> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return Set.Where(predicate).Select(e => e as TAggregateRoot).FirstAsync(cancellationToken);
    }

    public Task<TAggregateRoot> GetSingleAsync(ISpecification<TEntity, TKey> specification)
    {
        return ApplySpecification(specification).Select(e => e as TAggregateRoot).SingleAsync(cancellationToken);
    }
    #endregion

    private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity, TKey> specification)
    {
        IQueryable<TEntity> query = this.Set.AsQueryable();

        foreach (var includeClause in specification.IncludeClauses)
        {
            query = query.Include(includeClause);
        }

        foreach (var filterClause in specification.FilterClauses)
        {
            query = query.Where(filterClause);
        }

        if (specification is IPagedSpecification<TEntity, TKey> pageSpecification)
        {
            query = query
                        .Skip((pageSpecification.CurrentPage - 1) * pageSpecification.ItemsPerPage)
                        .Take(pageSpecification.ItemsPerPage);
        }

        foreach (var orderClause in specification.OrderByClauses)
        {
            query = orderClause.Value == SortOrder.Ascending
                  ? query.OrderBy(orderClause.Key)
                  : query.OrderByDescending(orderClause.Key);
        }

        return query;
    }
}
