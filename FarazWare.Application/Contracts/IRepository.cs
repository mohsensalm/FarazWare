using Microsoft.EntityFrameworkCore.Query;
using PagedList;
using System.Linq.Expressions;

namespace FarazWare.Application.Contracts;

public interface IRepository<TEntity> where TEntity : class
{
    #region Get With Options

    /// <summary>
    /// Queries the data source with an optional predicate.
    /// </summary>
    IQueryable<TEntity> Query(Expression<Func<TEntity, bool>>? predicate = null);

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
    IEnumerable<TEntity> GetAll();

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
    Task<IEnumerable<TEntity>> GetAllAsync();

    /// <summary>
    /// Determines if the repository contains any entities that match the
    /// predicate.
    /// </summary>
    /// <param name="predicate">The predicate to use when searching the repository.</param>
    /// <returns>True if the repository contains any entities that match the predicate,
    /// otherwise false.</returns>
    bool Any(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Determines asynchronously if the repository contains any entities that match the
    /// predicate.
    /// </summary>
    /// <param name="predicate">The predicate to use when searching the repository.</param>
    /// <returns>True if the repository contains any entities that match the predicate,
    /// otherwise false.</returns>
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Retrieves a single entity from the repository that satisfies the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to be met by the entity.</param>
    /// <returns>The entity that satisfies the predicate, or null if no such entity exists.</returns>
    TEntity? GetEntity(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Retrieves a single entity from the repository that satisfies the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to be met by the entity.</param>
    /// <returns>The entity that satisfies the predicate, or null if no such entity exists.</returns>
    Task<TEntity?> GetEntityAsync(Expression<Func<TEntity, bool>> predicate);
    /// <summary>
    /// Retrieves a single entity from the repository that satisfies the specified predicate,
    /// including those filtered out by global query filters (e.g., soft-deleted entities).
    /// </summary>
    /// <param name="predicate">The condition to be met by the entity.</param>
    /// <returns>The entity that satisfies the predicate, or null if no such entity exists.</returns>
    Task<TEntity?> GetEntityIgnoreFilterAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Retrieves the last entity from the database based on a provided predicate.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity to retrieve.</typeparam>
    /// <param name="predicate">A function that specifies the condition to be met by elements in the sequence.</param>
    /// <returns>The last element that satisfies the conditions, or null if no such element is found.</returns>
    TEntity? GetLastEntity(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Retrieves a single entity from the repository that satisfies the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to be met by the entity.</param>
    /// <returns>The entity that satisfies the predicate, or null if no such entity exists.</returns>
    Task<TEntity?> GetLastEntityAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Retrieves a single entity from the repository that satisfies the specified predicate.
    /// </summary>
    /// <returns>The entity that satisfies the predicate, or null if no such entity exists.</returns>
    TEntity? FirstOrDefault();

    /// <summary>
    /// Asynchronously retrieves the first entity from the repository or null if no such entity exists.
    /// </summary>
    /// <returns>The first entity found, or null if no entities exist.</returns>
    Task<TEntity?> FirstOrDefaultAsync();

    /// <summary>
    /// Retrieves the first entity from the repository that satisfies the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to be met by the entity.</param>
    /// <returns>The first entity found, or null if no such entity exists.</returns>
    TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Retrieves the first entity from the repository that satisfies the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to be met by the entity.</param>
    /// <returns>The first entity found, or null if no such entity exists.</returns>
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Retrieves the first entity from the repository.
    /// </summary>
    /// <returns>The first entity found, or the default entity if no entities exist.</returns>
    TEntity First();

    /// <summary>
    /// Asynchronously retrieves the first entity from the repository.
    /// </summary>
    /// <returns>The first entity found, or the default entity if no entities exist.</returns>
    Task<TEntity> FirstAsync();

    /// <summary>
    /// Retrieves the first entity from the repository that satisfies the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to be met by the entity.</param>
    /// <returns>The first entity found, or throws an exception if no such entity exists.</returns>
    TEntity First(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Retrieves the first entity from the repository that satisfies the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to be met by the entity.</param>
    /// <returns>The first entity found, or throws an exception if no such entity exists.</returns>
    Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Retrieves the last entity from the repository.
    /// </summary>
    /// <returns>The last entity found, or throws an exception if no entities exist.</returns>
    TEntity Last();

    /// <summary>
    /// Retrieves the last entity from the repository asynchronously.
    /// </summary>
    /// <returns>The last entity found, or throws an exception if no entities exist.</returns>
    Task<TEntity> LastAsync();

    /// <summary>
    /// Retrieves the last entity from the repository that satisfies the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to be met by the entity.</param>
    /// <returns>The last entity found, or throws an exception if no such entity exists.</returns>
    TEntity Last(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Retrieves the last entity from the repository that satisfies the specified predicate asynchronously.
    /// </summary>
    /// <param name="predicate">The condition to be met by the entity.</param>
    /// <returns>The last entity found that satisfies the predicate, or throws an exception if no such entity exists.</returns>
    Task<TEntity> LastAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Retrieves the last entity from the repository or returns null if no entities exist.
    /// </summary>
    /// <returns>The last entity found, or null if no entities exist.</returns>
    TEntity? LastOrDefault();

    /// <summary>
    /// Asynchronously retrieves the last entity from the repository or returns null if no entities exist.
    /// </summary>
    /// <returns>The last entity found, or null if no entities exist.</returns>
    Task<TEntity?> LastOrDefaultAsync();

    /// <summary>
    /// Retrieves the last entity from the repository that satisfies the specified predicate,
    /// or returns null if no such entity exists.
    /// </summary>
    /// <param name="predicate">The condition to be met by the entity.</param>
    /// <returns>The last entity found that satisfies the predicate, or null if no such entity exists.</returns>
    TEntity? LastOrDefault(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Retrieves the last entity from the repository that satisfies the specified predicate,
    /// or returns null if no such entity exists.
    /// </summary>
    /// <param name="predicate">The condition to be met by the entity.</param>
    /// <returns>The last entity found that satisfies the predicate, or null if no such entity exists.</returns>
    Task<TEntity?> LastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Retrieves a single entity from the repository, or returns the default value if no such entity exists.
    /// </summary>
    /// <returns>The single entity found, or the default value if no entity exists.</returns>
    TEntity? SingleOrDefault();

    /// <summary>
    /// Retrieves a single entity from the repository asynchronously, or returns the default value if no such entity exists.
    /// </summary>
    /// <returns>The single entity found, or the default value if no entity exists.</returns>
    Task<TEntity?> SingleOrDefaultAsync();

    /// <summary>
    /// Retrieves a single entity from the repository that satisfies the specified predicate,
    /// or returns the default value if no such entity exists.
    /// </summary>
    /// <param name="predicate">The condition to be met by the entity.</param>
    /// <returns>The single entity found that satisfies the predicate, or the default value if no such entity exists.</returns>
    TEntity? SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Retrieves a single entity from the repository asynchronously that satisfies the specified predicate,
    /// or returns the default value if no such entity exists.
    /// </summary>
    /// <param name="predicate">The condition to be met by the entity.</param>
    /// <returns>The single entity found that satisfies the predicate, or the default value if no such entity exists.</returns>
    Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Retrieves a single entity from the repository asynchronously that satisfies the specified predicate,
    /// or returns the default value if no such entity exists.
    /// </summary>
    /// <param name="predicate">The condition to be met by the entity.</param>
    /// <returns>The single entity found that satisfies the predicate, or the default value if no such entity exists.</returns>
    TEntity Single();

    /// <summary>
    /// Retrieves a single entity from the repository asynchronously.
    /// </summary>
    /// <returns>The single entity found.</returns>
    /// <exception cref="InvalidOperationException">Thrown if no entity or more than one entity is found.</exception>
    Task<TEntity> SingleAsync();

    /// <summary>
    /// Retrieves a single entity from the repository that satisfies the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to be met by the entity.</param>
    /// <returns>The single entity found that satisfies the predicate.</returns>
    /// <exception cref="InvalidOperationException">Thrown if no entity or more than one entity is found.</exception>
    TEntity Single(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Retrieves a single entity from the repository asynchronously that satisfies the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to be met by the entity.</param>
    /// <returns>The single entity found that satisfies the predicate.</returns>
    /// <exception cref="InvalidOperationException">Thrown if no entity or more than one entity is found.</exception>
    Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Retrieves all entities from the repository that satisfy the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to be met by the entities.</param>
    /// <returns>An <see cref="IEnumerable{TEntity}"/> of entities that satisfy the predicate.</returns>
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Retrieves all entities from the repository asynchronously that satisfy the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to be met by the entities.</param>
    /// <returns>An <see cref="IEnumerable{TEntity}"/> of entities that satisfy the predicate.</returns>
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Retrieves the top <paramref name="count"/> entities from the repository.
    /// </summary>
    /// <param name="count">The number of entities to retrieve.</param>
    /// <returns>An <see cref="IEnumerable{TEntity}"/> of the top <paramref name="count"/> entities.</returns>
    IEnumerable<TEntity> GetTop(int count);

    /// <summary>
    /// Retrieves the top <paramref name="count"/> entities from the repository asynchronously.
    /// </summary>
    /// <param name="count">The number of entities to retrieve.</param>
    /// <returns>An <see cref="IEnumerable{TEntity}"/> of the top <paramref name="count"/> entities.</returns>
    Task<IEnumerable<TEntity>> GetTopAsync(int count);

    /// <summary>
    /// Retrieves the top <paramref name="count"/> entities from the repository
    /// that satisfy the specified predicate.
    /// </summary>
    /// <param name="count">The number of entities to retrieve.</param>
    /// <param name="predicate">The condition to be met by the entities.</param>
    /// <returns>An <see cref="IEnumerable{TEntity}"/> of the top <paramref name="count"/> entities
    /// that satisfy the predicate.</returns>
    IEnumerable<TEntity> GetTop(int count, Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Retrieves the top <paramref name="count"/> entities from the repository
    /// that satisfy the specified predicate asynchronously.
    /// </summary>
    /// <param name="count">The number of entities to retrieve.</param>
    /// <param name="predicate">The condition to be met by the entities.</param>
    /// <returns>An <see cref="IEnumerable{TEntity}"/> of the top <paramref name="count"/> entities
    /// that satisfy the predicate.</returns>
    Task<IEnumerable<TEntity>> GetTopAsync(int count, Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Retrieves the last <paramref name="count"/> entities from the repository.
    /// </summary>
    /// <param name="count">The number of entities to retrieve.</param>
    /// <returns>An <see cref="IEnumerable{TEntity}"/> of the last <paramref name="count"/> entities.</returns>
    IEnumerable<TEntity> GetLast(int count);

    /// <summary>
    /// Retrieves the last 'count' entities from the database asynchronously.
    /// </summary>
    /// <param name="count">The number of entities to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation, returning an enumerable collection of the last 'count' entities.</returns>
    Task<IEnumerable<TEntity>> GetLastAsync(int count);

    /// <summary>
    /// Retrieves the last 'count' entities that match a specified condition from the database.
    /// </summary>
    /// <param name="count">The number of entities to retrieve.</param>
    /// <param name="predicate">A function to test each entity for a condition. Only entities that satisfy this condition are included in the result.</param>
    /// <returns>An enumerable collection of the last 'count' entities that match the specified condition.</returns>
    IEnumerable<TEntity> GetLast(int count, Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Retrieves the last 'count' entities that match a specified condition from the database asynchronously.
    /// </summary>
    /// <param name="count">The number of entities to retrieve.</param>
    /// <param name="predicate">A function to test each entity for a condition. Only entities that satisfy this condition are included in the result.</param>
    /// <returns>A task that represents the asynchronous operation, returning an enumerable collection of the last 'count' entities that match the specified condition.</returns>
    Task<IEnumerable<TEntity>> GetLastAsync(int count, Expression<Func<TEntity, bool>> predicate);

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
    PagedList<TEntity> GetPaged<TOrder>(int pageNumber, int pageSize = 10, bool isAscendingOrder = false, Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, TOrder>> orderByProperty = null);

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
    Task<PagedList<TEntity>> GetPagedAsync<TOrder>(int pageNumber, int pageSize = 10, bool isAscendingOrder = false, Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, TOrder>> orderByProperty = null);

    /// <summary>
    /// Asynchronously retrieves a paginated list of entities from a provided query.
    /// </summary>
    /// <param name="pageNumber">The page number to retrieve (1-based index).</param>
    /// <param name="pageSize">The number of entities to include in each page.</param>
    /// <param name="query">An <see cref="IQueryable{TEntity}"/> representing the filtered entities to paginate.</param>
    /// <returns>A task representing the asynchronous operation, with a <see cref="PagedList{TEntity}"/> containing the paginated entities and pagination details.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the page number is less than 1.</exception>
    Task<PagedList<TEntity>> GetPagedAsync(int pageNumber, int pageSize, IQueryable<TEntity> query);

    #endregion Get Paged Data

    #region Create Update Delete
    /// <summary>
    /// Retrieves an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    Task<TEntity?> GetByIdAsync(object id);

    /// <summary>
    /// Inserts a new entity into the data source.
    /// </summary>
    /// <param name="entity">The entity to be inserted.</param>
    void Insert(TEntity entity);

    /// <summary>
    /// Asynchronously inserts a new entity into the data source.
    /// </summary>
    /// <param name="entity">The entity to be inserted.</param>
    /// <returns>A task that represents the asynchronous insert operation.</returns>
    Task InsertAsync(TEntity entity);

    /// <summary>
    /// Inserts a range of new entities into the data source.
    /// </summary>
    /// <param name="entities">The list of entities to be inserted.</param>
    void AddRange(List<TEntity> entities);

    /// <summary>
    /// Asynchronously inserts a range of new entities into the data source.
    /// </summary>
    /// <param name="entities">The list of entities to be inserted.</param>
    /// <returns>A task that represents the asynchronous insert operation.</returns>
    Task AddRangeAsync(List<TEntity> entities);

    /// <summary>
    /// Updates an existing entity in the data source.
    /// </summary>
    /// <param name="entity">The entity to be updated.</param>
    void Update(TEntity entity);

    /// <summary>
    /// Updates a range of existing entities in the data source.
    /// </summary>
    /// <param name="entities">The list of entities to be updated.</param>
    void UpdateRange(List<TEntity> entities);

    /// <summary>
    /// Removes a specific entity from the data source.
    /// </summary>
    /// <param name="entity">The entity to be removed.</param>
    void Remove(TEntity entity);

    /// <summary>
    /// Removes a range of entities from the data source.
    /// </summary>
    /// <param name="entities">The list of entities to be removed.</param>
    void RemoveRange(List<TEntity> entities);
    /// <summary>
    /// Soft deletes an entity if it implements IDeletableEntity, otherwise hard deletes it.
    /// </summary>
    /// <param name="entity">The entity to be deleted.</param>
    void Delete(TEntity entity);

    /// <summary>
    /// Asynchronously soft deletes an entity if it implements IDeletableEntity, otherwise hard deletes it.
    /// </summary>
    /// <param name="entity">The entity to be deleted.</param>
    /// <returns>A task that represents the asynchronous delete operation.</returns>
    Task DeleteAsync(TEntity entity);

    /// <summary>
    /// Soft deletes a range of entities if they implement IDeletableEntity, otherwise hard deletes them.
    /// </summary>
    /// <param name="entities">The list of entities to be deleted.</param>
    void DeleteRange(List<TEntity> entities);

    /// <summary>
    /// Asynchronously soft deletes a range of entities if they implement IDeletableEntity, otherwise hard deletes them.
    /// </summary>
    /// <param name="entities">The list of entities to be deleted.</param>
    /// <returns>A task that represents the asynchronous delete operation.</returns>
    Task DeleteRangeAsync(List<TEntity> entities);

}
    #endregion Create Update Delete