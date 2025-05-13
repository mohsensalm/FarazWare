
using Jpneo.CorpBanking.Application.Contracts;

namespace FarazWare.Application.Contracts;

/// <summary>
/// Interface for the Unit of Work pattern, providing a way to group multiple operations into a single transaction.
/// Inherits from <see cref="IProgramAbilitySupport"/> and <see cref="IDisposable"/>.
/// </summary>
public interface IUnitOfWork : IProgramAbilitySupport, IDisposable
{
    /// <summary>
    /// Retrieves a repository for the specified entity type.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <returns>An instance of the <see cref="IRepository{TEntity}"/> for the specified entity type.</returns>
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

    /// <summary>
    /// Asynchronously saves all changes made in this unit of work to the database.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SaveChangesAsync();

    /// <summary>
    /// Saves all changes made in this unit of work to the database.
    /// </summary>
    void SaveChanges();
}