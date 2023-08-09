using AutoMapper;
using backend.Business.src.Interfaces;
using backend.Domain.src.Common;
using backend.Domain.src.RepoInterfaces;

namespace backend.Business.src.Implementations;

public class BaseService<T, TReadDto, TCreateDto, TUpdateDto>
    : IBaseService<T, TReadDto, TCreateDto, TUpdateDto>
{
    private readonly IBaseRepo<T> _baseRepo;
    protected readonly IMapper _mapper;

    public BaseService(IBaseRepo<T> baseRepo, IMapper mapper)
    {
        _baseRepo = baseRepo;
        _mapper = mapper;
    }

    public virtual async Task<TReadDto> CreateOne(TCreateDto newEntity)
    {
        var entity = await _baseRepo.CreateOne(_mapper.Map<T>(newEntity));
        return _mapper.Map<TReadDto>(entity);
    }

    public async Task<bool> DeleteOneById(string id)
    {
        //! original code
        // var foundEntity = _baseRepo.GetOneById(id);
        // if (foundEntity != null)
        // {
        //     _baseRepo.DeleteOneById(foundEntity);
        //     return true;
        // }
        // return false;
        try
        {
            var foundEntity = await _baseRepo.GetOneById(id);
            if (foundEntity != null)
            {
                await _baseRepo.DeleteOneById(foundEntity);
                return true;
            }
            else
            {
                throw new Exception($"Entity with ID '{id}' not found.");
                return false;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<IEnumerable<TReadDto>> GetAll(QueryParameters queryParameters)
    {
        IEnumerable<TReadDto> getAllEntity =
            (IEnumerable<TReadDto>)await _baseRepo.GetAll(queryParameters);
        return getAllEntity;
    }

    public async Task<TReadDto> GetOneById(string id)
    {
        var foundEntity = await _baseRepo.GetOneById(id);
        if (foundEntity != null)
        {
            return _mapper.Map<TReadDto>(foundEntity);
        }
        else
        {
            throw new Exception($"Entity with ID '{id}' not found.");
        }
    }

    public async Task<TReadDto> UpdateOneById(string id, TUpdateDto newEntity)
    {
        var foundEntity = await _baseRepo.GetOneById(id);
        if (foundEntity == null)
        {
            throw new Exception($"Item with {id} id not found");
        }
        else
        {
            var updated = await _baseRepo.UpdateOneById(foundEntity, _mapper.Map<T>(newEntity));
            return _mapper.Map<TReadDto>(updated);
        }
    }
}
