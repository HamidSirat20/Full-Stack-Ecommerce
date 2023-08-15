using backend.Domain.src.Common;

namespace backend.Domain.src.RepoInterfaces;

public interface IBaseRepo<T>
{
    Task<IEnumerable<T>> GetAll(QueryParameters queryParameters);
    Task<T> GetOneById(Guid id);
    Task<T> UpdateOneById(T newEntity);
    Task<bool> DeleteOneById(T newEntity);
    Task<T> CreateOne(T newEntity);
}
