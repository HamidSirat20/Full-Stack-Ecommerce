using backend.Domain.src.Common;

namespace backend.Domain.src.RepoInterfaces;

public interface IBaseRepo<T>
{
    IEnumerable<T> GetAll( QueryParameters queryParameters);
    T GetOneById(string id);
    T UpdateOneById(T foundEntity,T newEntity);
    bool DeleteOneById(T newEntity);
    T CreateOne(T newEntity);
}
