using backend.Domain.src.Common;

namespace backend.Business.src.Interfaces;

public interface IBaseService<T,TReadDto,TCreateDto,TUpdateDto>
{
    Task<IEnumerable<TReadDto>> GetAll(QueryParameters queryParameters);
    Task<TReadDto> GetOneById(Guid id);
    Task<TReadDto> UpdateOneById(Guid id,TUpdateDto newEntity);
    Task<bool> DeleteOneById(Guid id);
    Task<TReadDto> CreateOne(TCreateDto newEntity);
}
