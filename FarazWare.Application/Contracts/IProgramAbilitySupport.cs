namespace Jpneo.CorpBanking.Application.Contracts;

/// <summary>
/// Interface for executing stored procedures and functions in a database.
/// Provides synchronous and asynchronous methods for executing commands and retrieving results.
/// </summary>
public interface IProgramAbilitySupport
{
    /// <summary>
    /// Executes a stored procedure and returns an integer result.
    /// </summary>
    /// <param name="name">The name of the stored procedure.</param>
    /// <param name="data">The data to pass to the stored procedure.</param>
    /// <param name="commandTimeout">The command timeout in seconds. Defaults to 0 (no timeout).</param>
    /// <returns>The result of the stored procedure execution as an integer.</returns>
    int ExecuteStoredProcedure(string name, object data, int commandTimeout = 0);

    /// <summary>
    /// Asynchronously executes a stored procedure and returns an integer result.
    /// </summary>
    /// <param name="name">The name of the stored procedure.</param>
    /// <param name="data">The data to pass to the stored procedure.</param>
    /// <param name="commandTimeout">The command timeout in seconds. Defaults to 0 (no timeout).</param>
    /// <returns>A task that represents the asynchronous operation, containing the result of the stored procedure execution as an integer.</returns>
    Task<int> ExecuteStoredProcedureAsync(string name, object data, int commandTimeout = 0);

    /// <summary>
    /// Executes a stored procedure and returns a single entity result.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity to return.</typeparam>
    /// <param name="name">The name of the stored procedure.</param>
    /// <param name="data">The data to pass to the stored procedure.</param>
    /// <param name="commandTimeout">The command timeout in seconds. Defaults to 0 (no timeout).</param>
    /// <returns>The single entity result from the stored procedure execution.</returns>
    TEntity ExecuteStoredProcedureSingle<TEntity>(string name, object data, int commandTimeout = 0);

    /// <summary>
    /// Asynchronously executes a stored procedure and returns a single entity result.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity to return.</typeparam>
    /// <param name="name">The name of the stored procedure.</param>
    /// <param name="data">The data to pass to the stored procedure.</param>
    /// <param name="commandTimeout">The command timeout in seconds. Defaults to 0 (no timeout).</param>
    /// <returns>A task that represents the asynchronous operation, containing the single entity result from the stored procedure execution.</returns>
    Task<TEntity> ExecuteStoredProcedureSingleAsync<TEntity>(string name, object data, int commandTimeout = 0);

    /// <summary>
    /// Executes a stored procedure and returns a single entity result without data parameter.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity to return.</typeparam>
    /// <param name="name">The name of the stored procedure.</param>
    /// <param name="commandTimeout">The command timeout in seconds. Defaults to 0 (no timeout).</param>
    /// <returns>The single entity result from the stored procedure execution.</returns>
    TEntity ExecuteStoredProcedureSingle<TEntity>(string name, int commandTimeout = 0);

    /// <summary>
    /// Asynchronously executes a stored procedure and returns a single entity result without data parameter.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity to return.</typeparam>
    /// <param name="name">The name of the stored procedure.</param>
    /// <param name="commandTimeout">The command timeout in seconds. Defaults to 0 (no timeout).</param>
    /// <returns>A task that represents the asynchronous operation, containing the single entity result from the stored procedure execution.</returns>
    Task<TEntity> ExecuteStoredProcedureSingleAsync<TEntity>(string name, int commandTimeout = 0);

    /// <summary>
    /// Executes a stored procedure and returns an enumerable collection of entities.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entities to return.</typeparam>
    /// <param name="name">The name of the stored procedure.</param>
    /// <param name="data">The data to pass to the stored procedure.</param>
    /// <param name="commandTimeout">The command timeout in seconds. Defaults to 0 (no timeout).</param>
    /// <returns>An enumerable collection of entities from the stored procedure execution.</returns>
    IEnumerable<TEntity> ExecuteStoredProcedure<TEntity>(string name, object data, int commandTimeout = 0);

    /// <summary>
    /// Asynchronously executes a stored procedure and returns an enumerable collection of entities.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entities to return.</typeparam>
    /// <param name="name">The name of the stored procedure.</param>
    /// <param name="data">The data to pass to the stored procedure.</param>
    /// <param name="commandTimeout">The command timeout in seconds. Defaults to 0 (no timeout).</param>
    /// <returns>A task that represents the asynchronous operation, containing an enumerable collection of entities from the stored procedure execution.</returns>
    Task<IEnumerable<TEntity>> ExecuteStoredProcedureAsync<TEntity>(string name, object data, int commandTimeout = 0);

    /// <summary>
    /// Executes a stored procedure and returns an enumerable collection of entities without data parameter.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entities to return.</typeparam>
    /// <param name="name">The name of the stored procedure.</param>
    /// <param name="commandTimeout">The command timeout in seconds. Defaults to 0 (no timeout).</param>
    /// <returns>An enumerable collection of entities from the stored procedure execution.</returns>
    IEnumerable<TEntity> ExecuteStoredProcedure<TEntity>(string name, int commandTimeout = 0);

    /// <summary>
    /// Asynchronously executes a stored procedure and returns an enumerable collection of entities without data parameter.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entities to return.</typeparam>
    /// <param name="name">The name of the stored procedure.</param>
    /// <param name="commandTimeout">The command timeout in seconds. Defaults to 0 (no timeout).</param>
    /// <returns>A task that represents the asynchronous operation, containing an enumerable collection of entities from the stored procedure execution.</returns>
    Task<IEnumerable<TEntity>> ExecuteStoredProcedureAsync<TEntity>(string name, int commandTimeout = 0);

    /// <summary>
    /// Executes a function and returns a single entity result.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity to return.</typeparam>
    /// <param name="name">The name of the function.</param>
    /// <param name="predicate">The predicate to filter the results.</param>
    /// <param name="data">The data to pass to the function.</param>
    /// <param name="commandTimeout">The command timeout in seconds. Defaults to 0 (no timeout).</param>
    /// <returns>The single entity result from the function execution.</returns>
    TEntity ExecuteFunctionSingle<TEntity>(string name, string predicate, object data, int commandTimeout = 0);

    /// <summary>
    /// Asynchronously executes a function and returns a single entity result.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity to return.</typeparam>
    /// <param name="name">The name of the function.</param>
    /// <param name="predicate">The predicate to filter the results.</param>
    /// <param name="data">The data to pass to the function.</param>
    /// <param name="commandTimeout">The command timeout in seconds. Defaults to 0 (no timeout).</param>
    /// <returns>A task that represents the asynchronous operation, containing the single entity result from the function execution.</returns>
    Task<TEntity> ExecuteFunctionSingleAsync<TEntity>(string name, string predicate, object data, int commandTimeout = 0);

    /// <summary>
    /// Executes a function and returns an enumerable collection of entities.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entities to return.</typeparam>
    /// <param name="name">The name of the function.</param>
    /// <param name="predicate">The predicate to filter the results.</param>
    /// <param name="data">The data to pass to the function.</param>
    /// <param name="commandTimeout">The command timeout in seconds. Defaults to 0 (no timeout).</param>
    /// <returns>An enumerable collection of entities from the function execution.</returns>
    IEnumerable<TEntity> ExecuteFunction<TEntity>(string name, string predicate, object data, int commandTimeout = 0);

    /// <summary>
    /// Asynchronously executes a function and returns an enumerable collection of entities.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entities to return.</typeparam>
    /// <param name="name">The name of the function.</param>
    /// <param name="predicate">The predicate to filter the results.</param>
    /// <param name="data">The data to pass to the function.</param>
    /// <param name="commandTimeout">The command timeout in seconds. Defaults to 0 (no timeout).</param>
    /// <returns>A task that represents the asynchronous operation, containing an enumerable collection of entities from the function execution.</returns>
    Task<IEnumerable<TEntity>> ExecuteFunctionAsync<TEntity>(string name, string predicate, object data, int commandTimeout = 0);

    /// <summary>
    /// Asynchronously executes a raw SQL query and returns an enumerable collection of dynamic results.
    /// </summary>
    /// <param name="query">The SQL query to execute.</param>
    /// <returns>A task that represents the asynchronous operation, containing an enumerable collection of dynamic results.</returns>
    Task<IEnumerable<dynamic>> ExecuteQueryAsync(string query);
}