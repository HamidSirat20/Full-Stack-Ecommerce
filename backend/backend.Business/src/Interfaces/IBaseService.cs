using backend.Domain.src.Common;

namespace backend.Business.src.Interfaces;

public interface IBaseService<T,TReadDto,TCreateDto,TUpdateDto>
{
    Task<IEnumerable<TReadDto>> GetAll(QueryParameters queryParameters);
    Task<TReadDto> GetOneById(string id);
    Task<TReadDto> UpdateOneById(string id,TUpdateDto newEntity);
    Task<bool> DeleteOneById(string id);
    Task<TReadDto> CreateOne(TCreateDto newEntity);
}
