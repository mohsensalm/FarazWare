
using FarazWare.Application.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using PagedList;
using System.Linq.Expressions;

namespace FarazWare.Persistence;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    /// <summary>
    /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
    /// </summary>
    /// <param name="dbContext">The database context to be used for operations.</param>
    public Repository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    #region Get With Options
 
    /// <summary>
    /// Queries the data source with an optional predicate.
    /// </summary>
    public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>>? predicate = null)
    {
        // If no predicate is provided, return all data. Otherwise, filter based on the predicate.
        return predicate == null ? _dbSet : _dbSet.Where(predicate);
    }

    /// <summary>
    /// Retrieves all entities without tracking changes.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This method is useful for data retrieval scenarios where
    /// the entities are not intended to be modified and then saved.
    /// By disabling change tracking, the performance of the data
    /// retrieval operation is improved.
    /// </para>
    /// <para>
    /// We use <see cref="IQueryable{T}.AsNoTracking()"/> to disable change tracking.
    /// This tells Entity Framework Core not to keep track of the entities we retrieve,
    /// which reduces the memory footprint of the operation and improves performance.
    /// </para>
    /// <para>
    /// Note that we use <see cref="IQueryable{T}.ToList"/> to retrieve the entities,
    /// which executes the query and returns the results as a list.
    /// </para>
    /// </remarks>
    public IEnumerable<TEntity> GetAll()
    {
        // Disable change tracking to improve performance.
        // This is safe because we don't intend to modify the entities.
        return [.. _dbSet.AsNoTracking()];
    }

    /// <summary>
    /// Asynchronously retrieves all entities without tracking changes.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This method is useful for data retrieval scenarios where the entities are not intended to be modified and then saved.
    /// By disabling change tracking, the performance of the data retrieval operation is improved.
    /// </para>
    /// <para>
    /// We use <see cref="IQueryable{T}.AsNoTracking()"/> to disable change tracking.
    /// This is safe because we don't intend to modify the entities.
    /// </para>
    /// <para>
    /// We use <see cref="IQueryable{T}.ToListAsync()"/> instead of <see cref="IQueryable{T}.AsEnumerable()"/> to allow Entity Framework to use a more efficient execution strategy that avoids unnecessary overhead.
    /// </para>
    /// </remarks>
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    /// <summary>
    /// Determines if the repository contains any entities that match the
    /// predicate.
    /// </summary>
    /// <param name="predicate">The predicate to use when searching the repository.</param>
    /// <returns>True if the repository contains any entities that match the predicate,
    /// otherwise false.</returns>
    public bool Any(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.Any(predicate);
    }

    /// <summary>
    /// Determines asynchronously if the repository contains any entities that match the
    /// predicate.
    /// </summary>
    /// <param name="predicate">The predicate to use when searching the repository.</param>
    /// <returns>True if the repository contains any entities that match the predicate,
    /// otherwise false.</returns>
    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
    {
        // Use the AnyAsync extension method to search the repository asynchronously
        return await _dbSet.AnyAsync(predicate);
    }

    /// <summary>
    /// Retrieves a single entity from the repository that satisfies the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to be met by the entity.</param>
    /// <returns>The entity that satisfies the predicate, or null if no such entity exists.</returns>
    public TEntity? GetEntity(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.FirstOrDefault(predicate);
    }

    /// <summary>
    /// Retrieves a single entity from the repository that satisfies the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to be met by the entity.</param>
    /// <returns>The entity that satisfies the predicate, or null if no such entity exists.</returns>
    public async Task<TEntity?> GetEntityAsync(Expression<Func<TEntity, bool>> predicate)
    {
        // Use the SingleOrDefaultAsync extension method to search the repository asynchronously
        return await _dbSet.FirstOrDefaultAsync(predicate);
    }
    /// <summary>
    /// Retrieves a single entity from the repository that satisfies the specified predicate,
    /// including those filtered out by global query filters (e.g., soft-deleted entities).
    /// </summary>
    /// <param name="predicate">The condition to be met by the entity.</param>
    /// <returns>The entity that satisfies the predicate, or null if no such entity exists.</returns>
    public async Task<TEntity?> GetEntityIgnoreFilterAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.IgnoreQueryFilters().FirstOrDefaultAsync(predicate);
    }

    /// <summary>
    /// Retrieves the last entity from the database based on a provided predicate.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity to retrieve.</typeparam>
    /// <param name="predicate">A function that specifies the condition to be met by elements in the sequence.</param>
    /// <returns>The last element that satisfies the conditions, or null if no such element is found.</returns>
    public TEntity? GetLastEntity(Expression<Func<TEntity, bool>> predicate)
    {
        // This method retrieves the last entity from the database based on a provided predicate.
        return _dbSet.LastOrDefault(predicate);
    }

    /// <summary>
    /// Retrieves a single entity from the repository that satisfies the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to be met by the entity.</param>
    /// <returns>The entity that satisfies the predicate, or null if no such entity exists.</returns>
    public async Task<TEntity?> GetLastEntityAsync(Expression<Func<TEntity, bool>> predicate)
    {
        // Use the LastOrDefaultAsync extension method to retrieve the entity from the repository asynchronously
        // that satisfies the predicate.
        return await _dbSet.LastOrDefaultAsync(predicate);
    }

    /// <summary>
    /// Retrieves a single entity from the repository that satisfies the specified predicate.
    /// </summary>
    /// <returns>The entity that satisfies the predicate, or null if no such entity exists.</returns>
    public TEntity? FirstOrDefault()
    {
        return _dbSet.FirstOrDefault();
    }

    /// <summary>
    /// Asynchronously retrieves the first entity from the repository or null if no such entity exists.
    /// </summary>
    /// <returns>The first entity found, or null if no entities exist.</returns>
    public async Task<TEntity?> FirstOrDefaultAsync()
    {
        // Use the FirstOrDefaultAsync extension method to retrieve the first entity asynchronously
        return await _dbSet.FirstOrDefaultAsync();
    }

    /// <summary>
    /// Retrieves the first entity from the repository that satisfies the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to be met by the entity.</param>
    /// <returns>The first entity found, or null if no such entity exists.</returns>
    public TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.FirstOrDefault(predicate);
    }

    /// <summary>
    /// Retrieves the first entity from the repository that satisfies the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to be met by the entity.</param>
    /// <returns>The first entity found, or null if no such entity exists.</returns>
    public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        // Use the FirstOrDefaultAsync extension method to retrieve the first entity asynchronously
        // that satisfies the predicate.
        return await _dbSet.FirstOrDefaultAsync(predicate);
    }

    /// <summary>
    /// Retrieves the first entity from the repository.
    /// </summary>
    /// <returns>The first entity found, or the default entity if no entities exist.</returns>
    public TEntity First()
    {
        return _dbSet.First();
    }

    /// <summary>
    /// Asynchronously retrieves the first entity from the repository.
    /// </summary>
    /// <returns>The first entity found, or the default entity if no entities exist.</returns>
    public async Task<TEntity> FirstAsync()
    {
        // Use the FirstAsync extension method to retrieve the first entity asynchronously
        return await _dbSet.FirstAsync();
    }

    /// <summary>
    /// Retrieves the first entity from the repository that satisfies the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to be met by the entity.</param>
    /// <returns>The first entity found, or throws an exception if no such entity exists.</returns>
    public TEntity First(Expression<Func<TEntity, bool>> predicate)
    {
        // Use the First extension method to retrieve the first entity from the repository
        // that satisfies the predicate.
        return _dbSet.First(predicate);
    }

    /// <summary>
    /// Retrieves the first entity from the repository that satisfies the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to be met by the entity.</param>
    /// <returns>The first entity found, or throws an exception if no such entity exists.</returns>
    public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate)
    {
        // Use the FirstAsync extension method to retrieve the first entity asynchronously
        // that satisfies the predicate.
        return await _dbSet.FirstAsync(predicate);
    }

    /// <summary>
    /// Retrieves the last entity from the repository.
    /// </summary>
    /// <returns>The last entity found, or throws an exception if no entities exist.</returns>
    public TEntity Last()
    {
        // Use the Last extension method to retrieve the last entity from the repository
        return _dbSet.Last();
    }

    /// <summary>
    /// Retrieves the last entity from the repository asynchronously.
    /// </summary>
    /// <returns>The last entity found, or throws an exception if no entities exist.</returns>
    public async Task<TEntity> LastAsync()
    {
        // Use the LastAsync extension method to retrieve the last entity asynchronously
        return await _dbSet.LastAsync();
    }

    /// <summary>
    /// Retrieves the last entity from the repository that satisfies the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to be met by the entity.</param>
    /// <returns>The last entity found, or throws an exception if no such entity exists.</returns>
    public TEntity Last(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.Last(predicate);
    }

    /// <summary>
    /// Retrieves the last entity from the repository that satisfies the specified predicate asynchronously.
    /// </summary>
    /// <param name="predicate">The condition to be met by the entity.</param>
    /// <returns>The last entity found that satisfies the predicate, or throws an exception if no such entity exists.</returns>
    public async Task<TEntity> LastAsync(Expression<Func<TEntity, bool>> predicate)
    {
        // Use the LastAsync extension method to retrieve the last entity asynchronously
        // that satisfies the predicate.
        return await _dbSet.LastAsync(predicate);
    }

    /// <summary>
    /// Retrieves the last entity from the repository or returns null if no entities exist.
    /// </summary>
    /// <returns>The last entity found, or null if no entities exist.</returns>
    public TEntity? LastOrDefault()
    {
        // Use the LastOrDefault method to retrieve the last entity from the repository
        return _dbSet.LastOrDefault();
    }

    /// <summary>
    /// Asynchronously retrieves the last entity from the repository or returns null if no entities exist.
    /// </summary>
    /// <returns>The last entity found, or null if no entities exist.</returns>
    public async Task<TEntity?> LastOrDefaultAsync()
    {
        // Use the LastOrDefaultAsync extension method to retrieve the last entity asynchronously
        return await _dbSet.LastOrDefaultAsync();
    }

    /// <summary>
    /// Retrieves the last entity from the repository that satisfies the specified predicate,
    /// or returns null if no such entity exists.
    /// </summary>
    /// <param name="predicate">The condition to be met by the entity.</param>
    /// <returns>The last entity found that satisfies the predicate, or null if no such entity exists.</returns>
    public TEntity? LastOrDefault(Expression<Func<TEntity, bool>> predicate)
    {
        // Use the LastOrDefault method to retrieve the last entity from the repository
        // that satisfies the predicate.
        return _dbSet.LastOrDefault(predicate);
    }

    /// <summary>
    /// Retrieves the last entity from the repository that satisfies the specified predicate,
    /// or returns null if no such entity exists.
    /// </summary>
    /// <param name="predicate">The condition to be met by the entity.</param>
    /// <returns>The last entity found that satisfies the predicate, or null if no such entity exists.</returns>
    public async Task<TEntity?> LastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        // Use the LastOrDefaultAsync extension method to retrieve the last entity asynchronously
        // that satisfies the predicate.
        return await _dbSet.LastOrDefaultAsync(predicate);
    }

    /// <summary>
    /// Retrieves a single entity from the repository, or returns the default value if no such entity exists.
    /// </summary>
    /// <returns>The single entity found, or the default value if no entity exists.</returns>
    public TEntity? SingleOrDefault()
    {
        // Use the SingleOrDefault method to retrieve a single entity from the repository
        return _dbSet.SingleOrDefault();
    }

    /// <summary>
    /// Retrieves a single entity from the repository asynchronously, or returns the default value if no such entity exists.
    /// </summary>
    /// <returns>The single entity found, or the default value if no entity exists.</returns>
    public async Task<TEntity?> SingleOrDefaultAsync()
    {
        // Use the SingleOrDefaultAsync extension method to retrieve a single entity from the repository asynchronously.
        return await _dbSet.SingleOrDefaultAsync();
    }

    /// <summary>
    /// Retrieves a single entity from the repository that satisfies the specified predicate,
    /// or returns the default value if no such entity exists.
    /// </summary>
    /// <param name="predicate">The condition to be met by the entity.</param>
    /// <returns>The single entity found that satisfies the predicate, or the default value if no such entity exists.</returns>
    public TEntity? SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
    {
        // Use the SingleOrDefault method to retrieve a single entity from the repository
        // that satisfies the predicate.
        return _dbSet.SingleOrDefault(predicate);
    }

    /// <summary>
    /// Retrieves a single entity from the repository asynchronously that satisfies the specified predicate,
    /// or returns the default value if no such entity exists.
    /// </summary>
    /// <param name="predicate">The condition to be met by the entity.</param>
    /// <returns>The single entity found that satisfies the predicate, or the default value if no such entity exists.</returns>
    public async Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        // Use the SingleOrDefaultAsync extension method to retrieve a single entity from the repository asynchronously
        // that satisfies the predicate.
        return await _dbSet.SingleOrDefaultAsync(predicate);
    }

    /// <summary>
    /// Retrieves a single entity from the repository.
    /// </summary>
    /// <returns>The single entity found.</returns>
    /// <exception cref="InvalidOperationException">Thrown if no entity or more than one entity is found.</exception>
    public TEntity Single()
    {
        // Use the Single method to retrieve a single entity from the repository.
        return _dbSet.Single();
    }

    /// <summary>
    /// Retrieves a single entity from the repository asynchronously.
    /// </summary>
    /// <returns>The single entity found.</returns>
    /// <exception cref="InvalidOperationException">Thrown if no entity or more than one entity is found.</exception>
    public async Task<TEntity> SingleAsync()
    {
        // Use the SingleAsync extension method to retrieve a single entity from the repository asynchronously.
        return await _dbSet.SingleAsync();
    }

    /// <summary>
    /// Retrieves a single entity from the repository that satisfies the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to be met by the entity.</param>
    /// <returns>The single entity found that satisfies the predicate.</returns>
    /// <exception cref="InvalidOperationException">Thrown if no entity or more than one entity is found.</exception>
    public TEntity Single(Expression<Func<TEntity, bool>> predicate)
    {
        // Use the Single method to retrieve a single entity from the repository
        // that satisfies the given predicate.
        return _dbSet.Single(predicate);
    }

    /// <summary>
    /// Retrieves a single entity from the repository asynchronously that satisfies the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to be met by the entity.</param>
    /// <returns>The single entity found that satisfies the predicate.</returns>
    /// <exception cref="InvalidOperationException">Thrown if no entity or more than one entity is found.</exception>
    public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
    {
        // Use the SingleAsync extension method to retrieve a single entity from the repository asynchronously
        // that satisfies the given predicate.
        return await _dbSet.SingleAsync(predicate);
    }

    /// <summary>
    /// Retrieves all entities from the repository that satisfy the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to be met by the entities.</param>
    /// <returns>An <see cref="IEnumerable{TEntity}"/> of entities that satisfy the predicate.</returns>
    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.Where(predicate);
    }

    /// <summary>
    /// Retrieves all entities from the repository asynchronously that satisfy the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to be met by the entities.</param>
    /// <returns>An <see cref="IEnumerable{TEntity}"/> of entities that satisfy the predicate.</returns>
    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        // Use the Where extension method to filter the entities that satisfy the predicate.
        // Use the AsNoTracking extension method to prevent the entities from being tracked.
        // Use the ToListAsync extension method to asynchronously retrieve the filtered entities.
        return await _dbSet.Where(predicate).AsNoTracking().ToListAsync();
    }

    /// <summary>
    /// Retrieves the top <paramref name="count"/> entities from the repository.
    /// </summary>
    /// <param name="count">The number of entities to retrieve.</param>
    /// <returns>An <see cref="IEnumerable{TEntity}"/> of the top <paramref name="count"/> entities.</returns>
    public IEnumerable<TEntity> GetTop(int count)
    {
        // Use the Skip and Take extension methods to retrieve the top 'count' entities.
        // The entities are not tracked by the context and are retrieved immediately.
        return _dbSet.Skip(0).Take(count).ToList();
    }

    /// <summary>
    /// Retrieves the top <paramref name="count"/> entities from the repository asynchronously.
    /// </summary>
    /// <param name="count">The number of entities to retrieve.</param>
    /// <returns>An <see cref="IEnumerable{TEntity}"/> of the top <paramref name="count"/> entities.</returns>
    public async Task<IEnumerable<TEntity>> GetTopAsync(int count)
    {
        // Use the Skip and Take extension methods to retrieve the top 'count' entities asynchronously.
        // The entities are not tracked by the context and are retrieved immediately.
        return await _dbSet.Skip(0).Take(count).ToListAsync();
    }

    /// <summary>
    /// Retrieves the top <paramref name="count"/> entities from the repository
    /// that satisfy the specified predicate.
    /// </summary>
    /// <param name="count">The number of entities to retrieve.</param>
    /// <param name="predicate">The condition to be met by the entities.</param>
    /// <returns>An <see cref="IEnumerable{TEntity}"/> of the top <paramref name="count"/> entities
    /// that satisfy the predicate.</returns>
    public IEnumerable<TEntity> GetTop(int count, Expression<Func<TEntity, bool>> predicate)
    {
        // Use the Where extension method to filter the entities that satisfy the predicate.
        // Use the Skip and Take extension methods to retrieve the top 'count' entities.
        // The entities are not tracked by the context and are retrieved immediately.
        return _dbSet.Where(predicate).Skip(0).Take(count).ToList();
    }

    /// <summary>
    /// Retrieves the top <paramref name="count"/> entities from the repository
    /// that satisfy the specified predicate asynchronously.
    /// </summary>
    /// <param name="count">The number of entities to retrieve.</param>
    /// <param name="predicate">The condition to be met by the entities.</param>
    /// <returns>An <see cref="IEnumerable{TEntity}"/> of the top <paramref name="count"/> entities
    /// that satisfy the predicate.</returns>
    public async Task<IEnumerable<TEntity>> GetTopAsync(int count, Expression<Func<TEntity, bool>> predicate)
    {
        // Use the Where extension method to filter the entities that satisfy the predicate.
        // Use the Skip and Take extension methods to retrieve the top 'count' entities.
        // The entities are not tracked by the context and are retrieved immediately asynchronously.
        return await _dbSet.Where(predicate).Skip(0).Take(count).ToListAsync();
    }

    /// <summary>
    /// Retrieves the last <paramref name="count"/> entities from the repository.
    /// </summary>
    /// <param name="count">The number of entities to retrieve.</param>
    /// <returns>An <see cref="IEnumerable{TEntity}"/> of the last <paramref name="count"/> entities.</returns>
    public IEnumerable<TEntity> GetLast(int count)
    {
        // Order the entities by descending order to retrieve the most recent ones
        // Use the Take extension method to get the specified number of entities
        // The entities are not tracked by the context
        return [.. _dbSet.OrderByDescending(x => x).Take(count).AsNoTracking()];
    }

    /// <summary>
    /// Retrieves the last 'count' entities from the database asynchronously.
    /// </summary>
    /// <param name="count">The number of entities to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation, returning an enumerable collection of the last 'count' entities.</returns>
    public async Task<IEnumerable<TEntity>> GetLastAsync(int count)
    {
        // Order all entities field in descending order and take the top 'count' entities
        return await _dbSet
            .OrderByDescending(x => x)
            .Take(count)                            // Take the first 'count' entities
            .AsNoTracking()                       // Avoid loading related data, improving performance for read-only operations
            .ToListAsync();                       // Asynchronously execute the query and convert the result to a List<TEntity>
    }

    /// <summary>
    /// Retrieves the last 'count' entities that match a specified condition from the database.
    /// </summary>
    /// <param name="count">The number of entities to retrieve.</param>
    /// <param name="predicate">A function to test each entity for a condition. Only entities that satisfy this condition are included in the result.</param>
    /// <returns>An enumerable collection of the last 'count' entities that match the specified condition.</returns>
    public IEnumerable<TEntity> GetLast(int count, Expression<Func<TEntity, bool>> predicate)
    {
        // Filter entities based on the provided predicate and take the top 'count' entities
        return [.. _dbSet
            .Where(predicate)
            .OrderByDescending(x => x)
            .Take(count)
            .AsNoTracking()];
    }

    /// <summary>
    /// Retrieves the last 'count' entities that match a specified condition from the database asynchronously.
    /// </summary>
    /// <param name="count">The number of entities to retrieve.</param>
    /// <param name="predicate">A function to test each entity for a condition. Only entities that satisfy this condition are included in the result.</param>
    /// <returns>A task that represents the asynchronous operation, returning an enumerable collection of the last 'count' entities that match the specified condition.</returns>
    public async Task<IEnumerable<TEntity>> GetLastAsync(int count, Expression<Func<TEntity, bool>> predicate)
    {
        // Apply the filtering condition to the database set
        var filteredEntities = _dbSet.Where(predicate);

        // Order all entities by their primary key in descending order and take the top 'count' entities
        return await filteredEntities.OrderByDescending(x => x).Take(count).AsNoTracking().ToListAsync();
    }

    #endregion Get With Options

    #region Get Paged Data

    /// <summary>
    /// Retrieves a paginated list of entities from the repository.
    /// </summary>
    /// <typeparam name="TOrder">The type of the property used for ordering the entities.</typeparam>
    /// <param name="pageNumber">The page number to retrieve (1-based index).</param>
    /// <param name="pageSize">The number of entities to include in each page. Defaults to 10.</param>
    /// <param name="isAscendingOrder">Indicates whether the results should be sorted in ascending order. Defaults to false.</param>
    /// <param name="predicate">An optional filter to apply to the entities.</param>
    /// <param name="orderByProperty">An optional expression to specify the property by which to order the results.</param>
    /// <returns>A <see cref="PagedList{TEntity}"/> containing the paginated entities and pagination details.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the page number is less than 1.</exception>
    public PagedList<TEntity> GetPaged<TOrder>(
        int pageNumber,
        int pageSize = 10,
        bool isAscendingOrder = false,
        Expression<Func<TEntity, bool>> predicate = null,
        Expression<Func<TEntity, TOrder>> orderByProperty = null)
    {
        return null;
        // Ensure the page size is positive; default to 10 if not.
        if (pageSize <= 0)
            pageSize = 10;

        // Create the query based on the provided predicate.
        var query = predicate == null ? Query() : Query(predicate);

        // Only get total rows count if it's needed for further processing.
        int rowsCount = 0;
        if (pageNumber > 1)
            rowsCount = query.Count();

        // Adjust the page number if it's out of bounds or not provided.
        if (rowsCount <= pageSize && rowsCount != 0 || pageNumber < 1)
            pageNumber = 1;

        // Apply ordering if an order by property is provided.
        if (orderByProperty != null)
        {
            query = isAscendingOrder ? query.OrderBy(orderByProperty) : query.OrderByDescending(orderByProperty);
        }

        // Skip unnecessary rows and get only needed ones for the current page.
        var pagedQuery = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        // If we don't have count yet, compute it now as last step before pagination.
        if (rowsCount == 0)
            rowsCount = query.Count();

        //return PagedList<TEntity>.Create(pagedQuery, pageNumber, pageSize, rowsCount);
    }

    /// <summary>
    /// Asynchronously retrieves a paginated list of entities from the repository.
    /// </summary>
    /// <typeparam name="TOrder">The type of the property used for ordering the entities.</typeparam>
    /// <param name="pageNumber">The page number to retrieve (1-based index).</param>
    /// <param name="pageSize">The number of entities to include in each page. Defaults to 10.</param>
    /// <param name="isAscendingOrder">Indicates whether the results should be sorted in ascending order. Defaults to false.</param>
    /// <param name="predicate">An optional filter to apply to the entities.</param>
    /// <param name="orderByProperty">An optional expression to specify the property by which to order the results.</param>
    /// <returns>A task representing the asynchronous operation, with a <see cref="PagedList{TEntity}"/> containing the paginated entities and pagination details.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the page number is less than 1.</exception>
    public async Task<PagedList<TEntity>> GetPagedAsync<TOrder>(
        int pageNumber,
        int pageSize = 10,
        bool isAscendingOrder = false,
        Expression<Func<TEntity, bool>> predicate = null,
        Expression<Func<TEntity, TOrder>> orderByProperty = null)
    {
        // Ensure the page size is positive; default to 10 if not.
        if (pageSize <= 0)
            pageSize = 10;

        // Create the query based on the provided predicate.
        var query = predicate == null ? Query() : Query(predicate);

        // Count the total number of rows that match the query asynchronously.
        var rowsCount = await query.CountAsync();

        // Adjust the page number if it's out of bounds.
        if (rowsCount <= pageSize || pageNumber <= 1)
            pageNumber = 1;

        // Apply ordering if an order by property is provided.
        if (orderByProperty != null)
        {
            query = isAscendingOrder ? query.OrderBy(orderByProperty) : query.OrderByDescending(orderByProperty);
        }
        return null;
        // Create and return the paginated list asynchronously.
        //return await PagedList<TEntity>.CreateAsync(query, pageNumber, pageSize, rowsCount);
    }

    /// <summary>
    /// Asynchronously retrieves a paginated list of entities from a provided query.
    /// </summary>
    /// <param name="pageNumber">The page number to retrieve (1-based index).</param>
    /// <param name="pageSize">The number of entities to include in each page.</param>
    /// <param name="query">An <see cref="IQueryable{TEntity}"/> representing the filtered entities to paginate.</param>
    /// <returns>A task representing the asynchronous operation, with a <see cref="PagedList{TEntity}"/> containing the paginated entities and pagination details.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the page number is less than 1.</exception>
    public async Task<PagedList<TEntity>> GetPagedAsync(int pageNumber, int pageSize, IQueryable<TEntity> query)
    {
        // Ensure the page size is positive; default to 10 if not provided.
        if (pageSize <= 0)
            pageSize = 10;

        // Count the total number of rows that match the query.
        var rowsCount = await query.CountAsync();

        // Adjust the page number if it's out of bounds.
        if (rowsCount <= pageSize || pageNumber <= 1)
            pageNumber = 1;

        return null;

        //    // Create and return the paginated list asynchronously.
        //    return await PagedList<TEntity>.CreateAsync(query, pageNumber, pageSize, rowsCount);
    }

    #endregion Get Paged Data

    #region Create Update Delete
    /// <summary>
    /// Retrieves an entity by its unique identifier.
    /// </summary>
    public async Task<TEntity?> GetByIdAsync(object id)
    {
        return await _dbSet.FindAsync(id);
    }

    /// <summary>
    /// Inserts a new entity into the data source.
    /// </summary>
    /// <param name="entity">The entity to be inserted.</param>
    public void Insert(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    /// <summary>
    /// Asynchronously inserts a new entity into the data source.
    /// </summary>
    /// <param name="entity">The entity to be inserted.</param>
    /// <returns>A task that represents the asynchronous insert operation.</returns>
    public async Task InsertAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    /// <summary>
    /// Inserts a range of new entities into the data source.
    /// </summary>
    /// <param name="entities">The list of entities to be inserted.</param>
    public void AddRange(List<TEntity> entities)
    {
        _dbSet.AddRange(entities);
    }

    /// <summary>
    /// Asynchronously inserts a range of new entities into the data source.
    /// </summary>
    /// <param name="entities">The list of entities to be inserted.</param>
    /// <returns>A task that represents the asynchronous insert operation.</returns>
    public async Task AddRangeAsync(List<TEntity> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    /// <summary>
    /// Updates an existing entity in the data source.
    /// </summary>
    /// <param name="entity">The entity to be updated.</param>
    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    /// <summary>
    /// Updates a range of existing entities in the data source.
    /// </summary>
    /// <param name="entities">The list of entities to be updated.</param>
    public void UpdateRange(List<TEntity> entities)
    {
        _dbSet.UpdateRange(entities);
    }

    /// <summary>
    /// Removes a specific entity from the data source.
    /// </summary>
    /// <param name="entity">The entity to be removed.</param>
    public void Remove(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    /// <summary>
    /// Removes a range of entities from the data source.
    /// </summary>
    /// <param name="entities">The list of entities to be removed.</param>
    public void RemoveRange(List<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
    }
    /// <summary>
    /// Soft deletes an entity if it implements IDeletableEntity, otherwise hard deletes it.
    /// </summary>
    /// <param name="entity">The entity to be deleted.</param>
    public void Delete(TEntity entity)
    {
        //if (entity is IDeletableEntity<int> deletableEntity)
        //{
        //    // Soft delete
        //    deletableEntity.IsDeleted = true;
        //    deletableEntity.DeletedAt = DateTime.Now;
        //    Update(entity);
        //}
        //else
        //{
        //    // Hard delete
        //    Remove(entity);
        //}
    }

    /// <summary>
    /// Asynchronously soft deletes an entity if it implements IDeletableEntity, otherwise hard deletes it.
    /// </summary>
    /// <param name="entity">The entity to be deleted.</param>
    /// <returns>A task that represents the asynchronous delete operation.</returns>
    public async Task DeleteAsync(TEntity entity)
    {
        Delete(entity);
        await Task.CompletedTask;
    }

    /// <summary>
    /// Soft deletes a range of entities if they implement IDeletableEntity, otherwise hard deletes them.
    /// </summary>
    /// <param name="entities">The list of entities to be deleted.</param>
    public void DeleteRange(List<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            Delete(entity);
        }
    }

    /// <summary>
    /// Asynchronously soft deletes a range of entities if they implement IDeletableEntity, otherwise hard deletes them.
    /// </summary>
    /// <param name="entities">The list of entities to be deleted.</param>
    /// <returns>A task that represents the asynchronous delete operation.</returns>
    public async Task DeleteRangeAsync(List<TEntity> entities)
    {
        DeleteRange(entities);
        await Task.CompletedTask;
    }
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }
    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }
    #endregion Create Update Delete
}