using MetricsAPI.Domain.Common.Core.Primitives;

namespace MetricsAPI.Database.Data.Interfaces;

/// <summary>
/// Represents the generic mongo repository interface.
/// </summary>
/// <typeparam name="T">The <see cref="Entity"/> type.</typeparam>
internal interface IMongoRepository<T> 
  where T : Entity
{
    /// <summary>
    /// Get all mongo entity async.
    /// </summary>
    /// <returns>List by <see cref="Entity"/> classes.</returns>
    Task<List<T>> GetAllAsync();
  
    /// <summary>
    /// Insert in database the entity.
    /// </summary>
    /// <param name="type">The generic type.</param>
    /// <returns>Returns <see cref="Task{TResult}"/>.</returns>
    Task InsertAsync(T type);
    
    /// <summary>
    /// Insert any entities in database.
    /// </summary>
    /// <param name="types">The enumerable of generic types classes.</param>
    /// <returns>Returns <see cref="Task{TResult}"/>.</returns>
    Task InsertRangeAsync(IEnumerable<T> types);
  
    /// <summary>
    /// Remove from database the entity.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>Returns <see cref="Task{TResult}"/>.</returns>
    Task RemoveAsync(string id);
}