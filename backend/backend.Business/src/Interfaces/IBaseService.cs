using backend.Domain.src.Common;

namespace backend.Business.src.Interfaces;

public interface IBaseService<T,TDto>
{
    IEnumerable<TDto> GetAll(QueryParameters queryParameters);
    TDto GetOneById(string id);
    TDto UpdateOneById(string id,TDto newEntity);
    bool DeleteOneById(string id);
    TDto CreateOne(T newEntity);
}
