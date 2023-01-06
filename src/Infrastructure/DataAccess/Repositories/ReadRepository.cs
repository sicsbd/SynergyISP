namespace SynergyISP.Infrastructure.DataAccess.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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

    #region GetAll

    /// <inheritdoc/>
    public IQueryable<TAggregateRoot?> GetAll()
    {
        return Set.Select(e => e as TAggregateRoot);
    }

    /// <inheritdoc/>
    public IQueryable<TAggregateRoot?> GetAll(ISpecification<TEntity, TKey> specification)
    {
        return ApplySpecification(specification).Select(e => e as TAggregateRoot);
    }

    public IQueryable<TReturnType?> GetAll<TReturnType>()
    {
        return Set.ProjectTo<TReturnType>(_configurationProvider, parameters: null);
    }

    /// <inheritdoc/>
    public IQueryable<TReturnType?> GetAll<TReturnType>(ISpecification<TEntity, TKey> specification)
    {
        return ApplySpecification(specification).ProjectTo<TReturnType>(_configurationProvider, parameters: null);
    }

    #endregion

    #region Single
    public TAggregateRoot? Get(TKey key)
    {
        return Set.Where(e => e.Id == key).Select(e => e as TAggregateRoot).First();
    }

    public TAggregateRoot? GetSingle(TKey key)
    {
        return Set.Where(e => e.Id == key).Select(e => e as TAggregateRoot).Single();
    }

    public TAggregateRoot? Get(ISpecification<TEntity, TKey> specification)
    {
        return ApplySpecification(specification).Select(e => e as TAggregateRoot).First();
    }

    public TAggregateRoot? GetSingle(ISpecification<TEntity, TKey> specification)
    {
        return ApplySpecification(specification).Select(e => e as TAggregateRoot).Single();
    }

    public Task<TAggregateRoot?> GetAsync(TKey key, CancellationToken cancellationToken = default)
    {
        return Set.Where(e => e.Id == key).Select(e => e as TAggregateRoot).FirstAsync(cancellationToken);
    }

    public Task<TAggregateRoot?> GetSingleAsync(TKey key, CancellationToken cancellationToken = default)
    {
        return Set.Where(e => e.Id == key).Select(e => e as TAggregateRoot).SingleAsync(cancellationToken);
    }

    public Task<TAggregateRoot?> GetAsync(ISpecification<TEntity, TKey> specification, CancellationToken cancellationToken = default)
    {
        return ApplySpecification(specification).Select(e => e as TAggregateRoot).FirstAsync(cancellationToken);
    }

    public Task<TAggregateRoot?> GetSingleAsync(ISpecification<TEntity, TKey> specification, CancellationToken cancellationToken = default)
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
            query = orderClause.Value.Equals(SortOrder.Ascending)
                  ? query.OrderBy(orderClause.Key)
                  : query.OrderByDescending(orderClause.Key);
        }

        return query;
    }
}
