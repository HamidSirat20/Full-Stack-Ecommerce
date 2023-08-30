using AutoMapper;
using backend.Business.src.Common;
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
        try
        {
            var entity = await _baseRepo.CreateOne(_mapper.Map<T>(newEntity));
            return _mapper.Map<TReadDto>(entity);
        }
        catch (Exception ex)
        {
            throw CustomErrorHandler.CreateEntityException(ex.Message);
        }
    }

    public async Task<bool> DeleteOneById(Guid id)
    {
        var foundItem = await _baseRepo.GetOneById(id);
        if (foundItem is not null)
        {
            await _baseRepo.DeleteOneById(foundItem);
            return true;
        }
        throw CustomErrorHandler.NotFoundException();
    }

    public virtual async Task<IEnumerable<TReadDto>> GetAll(QueryParameters queryParameters)
    {
        try
        {
            var entities = await _baseRepo.GetAll(queryParameters);
            var dtoEntities = _mapper.Map<IEnumerable<TReadDto>>(entities);

            if (dtoEntities.Any())
            {
                return dtoEntities;
            }
            else
            {
                throw CustomErrorHandler.NotFoundException("There is no item in the Repo");
            }
        }
        catch (System.Exception)
        {
            throw CustomErrorHandler.NotFoundException();
        }
    }

    public virtual async Task<TReadDto> GetOneById(Guid id)
    {
        try
        {
            var entity = _mapper.Map<TReadDto>(await _baseRepo.GetOneById(id));
            if (entity is not null)
            {
                return entity;
            }
            else
            {
                throw new Exception($"item with {id} id not found");
            }
        }
        catch (System.Exception)
        {
            throw CustomErrorHandler.NotFoundException();
        }
    }

    public virtual async Task<TReadDto> UpdateOneById(Guid id, TUpdateDto newEntity)
    {
        try
        {
            var foundItem = await _baseRepo.GetOneById(id);
            if (foundItem is null)
            {
                await _baseRepo.DeleteOneById(foundItem);
            }

            var updatedEntity = _baseRepo.UpdateOneById(_mapper.Map<T>(newEntity));
            return _mapper.Map<TReadDto>(updatedEntity);
        }
        catch (System.Exception)
        {
            throw CustomErrorHandler.NotFoundException();
        }
    }
}
