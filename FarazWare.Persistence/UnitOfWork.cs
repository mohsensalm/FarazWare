using Dapper;
using FarazWare.Application.Contracts;
using Jpneo.CorpBanking.Application.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FarazWare.Persistence;

/// <summary>
/// Implements the Unit of Work pattern, managing database transactions and operations.
/// </summary>
public class UnitOfWork(DbContext context, IDbConnection connection) : IUnitOfWork, IDisposable
{
    private readonly DbContext _context = context;
    private readonly IDbConnection _connection = connection;
    private bool _disposed;

    #region IProgramAbilitySupport

    #region Execute Stored Procedures

    /// <summary>
    /// Executes a stored procedure that does not return any results.
    /// </summary>
    /// <param name="name">The name of the stored procedure to execute.</param>
    /// <param name="data">The parameters to pass to the stored procedure.</param>
    /// <param name="commandTimeout">The command timeout in seconds (default is 0, which means no timeout).</param>
    /// <returns>The number of rows affected by the execution.</returns>
    public int ExecuteStoredProcedure(string name, object data, int commandTimeout = 0)
    {
        return _connection.Execute(name, data, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
    }

    /// <summary>
    /// Asynchronously executes a stored procedure that does not return any results.
    /// </summary>
    /// <param name="name">The name of the stored procedure to execute.</param>
    /// <param name="data">The parameters to pass to the stored procedure.</param>
    /// <param name="commandTimeout">The command timeout in seconds (default is 0, which means no timeout).</param>
    /// <returns>A task representing the asynchronous operation, with the number of rows affected as the result.</returns>
    public async Task<int> ExecuteStoredProcedureAsync(string name, object data, int commandTimeout = 0)
    {
        return await _connection.ExecuteAsync(name, data, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes a stored procedure and retrieves a single result.
    /// </summary>
    /// <typeparam name="TEntity">The type of the result to be returned.</typeparam>
    /// <param name="name">The name of the stored procedure to execute.</param>
    /// <param name="data">The parameters to pass to the stored procedure.</param>
    /// <param name="commandTimeout">The command timeout in seconds (default is 0, which means no timeout).</param>
    /// <returns>The single result returned by the stored procedure.</returns>
    public TEntity ExecuteStoredProcedureSingle<TEntity>(string name, object data, int commandTimeout = 0)
    {
        return _connection.QuerySingle<TEntity>(name, data, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
    }

    /// <summary>
    /// Executes a stored procedure and retrieves a single result.
    /// </summary>
    /// <typeparam name="TEntity">The type of the result to be returned.</typeparam>
    /// <param name="name">The name of the stored procedure to execute.</param>
    /// <param name="commandTimeout">The command timeout in seconds (default is 0, which means no timeout).</param>
    /// <returns>The single result returned by the stored procedure.</returns>
    public TEntity ExecuteStoredProcedureSingle<TEntity>(string name, int commandTimeout = 0)
    {
        return _connection.QuerySingle<TEntity>(name, commandType: CommandType.StoredProcedure, commandTimeout: _connection.ConnectionTimeout);
    }

    /// <summary>
    /// Asynchronously executes a stored procedure and retrieves a single result.
    /// </summary>
    /// <typeparam name="TEntity">The type of the result to be returned.</typeparam>
    /// <param name="name">The name of the stored procedure to execute.</param>
    /// <param name="data">The parameters to pass to the stored procedure.</param>
    /// <param name="commandTimeout">The command timeout in seconds (default is 0, which means no timeout).</param>
    /// <returns>A task representing the asynchronous operation, with the single result as the result.</returns>
    public async Task<TEntity> ExecuteStoredProcedureSingleAsync<TEntity>(string name, object data, int commandTimeout = 0)
    {
        return await _connection.QuerySingleAsync<TEntity>(name, data, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout).ConfigureAwait(false);
    }

    /// <summary>
    /// Asynchronously executes a stored procedure and retrieves a single result.
    /// </summary>
    /// <typeparam name="TEntity">The type of the result to be returned.</typeparam>
    /// <param name="name">The name of the stored procedure to execute.</param>
    /// <param name="commandTimeout">The command timeout in seconds (default is 0, which means no timeout).</param>
    /// <returns>A task representing the asynchronous operation, with the single result as the result.</returns>
    public async Task<TEntity> ExecuteStoredProcedureSingleAsync<TEntity>(string name, int commandTimeout = 0)
    {
        return await _connection.QuerySingleAsync<TEntity>(name, commandType: CommandType.StoredProcedure, commandTimeout: _connection.ConnectionTimeout).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes a stored procedure that returns multiple results.
    /// </summary>
    /// <typeparam name="TEntity">The type of the results to be returned.</typeparam>
    /// <param name="name">The name of the stored procedure to execute.</param>
    /// <param name="data">The parameters to pass to the stored procedure.</param>
    /// <param name="commandTimeout">The command timeout in seconds (default is 0, which means no timeout).</param>
    /// <returns>An enumerable collection of results returned by the stored procedure.</returns>
    public IEnumerable<TEntity> ExecuteStoredProcedure<TEntity>(string name, object data, int commandTimeout = 0)
    {
        return _connection.Query<TEntity>(name, data, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
    }

    /// <summary>
    /// Asynchronously executes a stored procedure that returns multiple results.
    /// </summary>
    /// <typeparam name="TEntity">The type of the results to be returned.</typeparam>
    /// <param name="name">The name of the stored procedure to execute.</param>
    /// <param name="data">The parameters to pass to the stored procedure.</param>
    /// <param name="commandTimeout">The command timeout in seconds (default is 0, which means no timeout).</param>
    /// <returns>A task representing the asynchronous operation, with an enumerable collection of results as the result.</returns>
    public async Task<IEnumerable<TEntity>> ExecuteStoredProcedureAsync<TEntity>(string name, object data, int commandTimeout = 0)
    {
        return await _connection.QueryAsync<TEntity>(name, data, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes a stored procedure that returns multiple results without parameters.
    /// </summary>
    /// <typeparam name="TEntity">The type of the results to be returned.</typeparam>
    /// <param name="name">The name of the stored procedure to execute.</param>
    /// <param name="commandTimeout">The command timeout in seconds (default is 0, which means no timeout).</param>
    /// <returns>An enumerable collection of results returned by the stored procedure.</returns>
    public IEnumerable<TEntity> ExecuteStoredProcedure<TEntity>(string name, int commandTimeout = 0)
    {
        return _connection.Query<TEntity>(name, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
    }

    /// <summary>
    /// Asynchronously executes a stored procedure that returns multiple results without parameters.
    /// </summary>
    /// <typeparam name="TEntity">The type of the results to be returned.</typeparam>
    /// <param name="name">The name of the stored procedure to execute.</param>
    /// <param name="commandTimeout">The command timeout in seconds (default is 0, which means no timeout).</param>
    /// <returns>A task representing the asynchronous operation, with an enumerable collection of results as the result.</returns>
    public async Task<IEnumerable<TEntity>> ExecuteStoredProcedureAsync<TEntity>(string name, int commandTimeout = 0)
    {
        return await _connection.QueryAsync<TEntity>(name, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout).ConfigureAwait(false);
    }

    #endregion Execute Stored Procedures

    #region Execute Functions

    /// <summary>
    /// Executes a database function and retrieves a single result.
    /// </summary>
    /// <typeparam name="TEntity">The type of the result to be returned.</typeparam>
    /// <param name="name">The name of the function to execute.</param>
    /// <param name="predicate">The arguments for the function in a comma-separated format.</param>
    /// <param name="data">The parameters to pass to the function.</param>
    /// <param name="commandTimeout">The command timeout in seconds (default is 0, which means no timeout).</param>
    /// <returns>The single result returned by the function.</returns>
    public TEntity ExecuteFunctionSingle<TEntity>(string name, string predicate, object data, int commandTimeout = 0)
    {
        return _connection.QuerySingle<TEntity>($"SELECT {name}({predicate})", data, commandTimeout: commandTimeout);
    }

    /// <summary>
    /// Asynchronously executes a database function and retrieves a single result.
    /// </summary>
    /// <typeparam name="TEntity">The type of the result to be returned.</typeparam>
    /// <param name="name">The name of the function to execute.</param>
    /// <param name="predicate">The arguments for the function in a comma-separated format.</param>
    /// <param name="data">The parameters to pass to the function.</param>
    /// <param name="commandTimeout">The command timeout in seconds (default is 0, which means no timeout).</param>
    /// <returns>A task representing the asynchronous operation, with the single result as the result.</returns>
    public async Task<TEntity> ExecuteFunctionSingleAsync<TEntity>(string name, string predicate, object data, int commandTimeout = 0)
    {
        return await _connection.QuerySingleAsync<TEntity>($"SELECT {name}({predicate})", data, commandTimeout: commandTimeout).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes a database function that returns multiple results.
    /// </summary>
    /// <typeparam name="TEntity">The type of the results to be returned.</typeparam>
    /// <param name="name">The name of the function to execute.</param>
    /// <param name="predicate">The arguments for the function in a comma-separated format.</param>
    /// <param name="data">The parameters to pass to the function.</param>
    /// <param name="commandTimeout">The command timeout in seconds (default is 0, which means no timeout).</param>
    /// <returns>An enumerable collection of results returned by the function.</returns>
    public IEnumerable<TEntity> ExecuteFunction<TEntity>(string name, string predicate, object data, int commandTimeout = 0)
    {
        return _connection.Query<TEntity>($"SELECT {name}({predicate})", data, commandTimeout: commandTimeout);
    }

    /// <summary>
    /// Asynchronously executes a database function that returns multiple results.
    /// </summary>
    /// <typeparam name="TEntity">The type of the results to be returned.</typeparam>
    /// <param name="name">The name of the function to execute.</param>
    /// <param name="predicate">The arguments for the function in a comma-separated format.</param>
    /// <param name="data">The parameters to pass to the function.</param>
    /// <param name="commandTimeout">The command timeout in seconds (default is 0, which means no timeout).</param>
    /// <returns>A task representing the asynchronous operation, with an enumerable collection of results as the result.</returns>
    public async Task<IEnumerable<TEntity>> ExecuteFunctionAsync<TEntity>(string name, string predicate, object data, int commandTimeout = 0)
    {
        return await _connection.QueryAsync<TEntity>($"SELECT {name}({predicate})", data, commandTimeout: commandTimeout).ConfigureAwait(false);
    }

    #endregion Execute Functions

    #region Execute Raw Queries

    /// <summary>
    /// Asynchronously executes a raw SQL query and returns dynamic results.
    /// </summary>
    /// <param name="query">The SQL query to execute.</param>
    /// <returns>A task representing the asynchronous operation, with an enumerable collection of dynamic results as the result.</returns>
    public async Task<IEnumerable<dynamic>> ExecuteQueryAsync(string query)
    {
        return await _connection.QueryAsync(query, commandTimeout: _connection.ConnectionTimeout).ConfigureAwait(false);
    }

    #endregion Execute Raw Queries

    #endregion IProgramAbilitySupport

    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        return new Repository<TEntity>(_context);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
                _connection.Dispose(); // Dispose the connection if it's managed here
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this); // Suppress finalization for this object
    }
}