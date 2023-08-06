using AutoMapper;
using backend.Business.src.Interfaces;
using backend.Domain.src.Common;
using backend.Domain.src.RepoInterfaces;

namespace backend.Business.src.Implementations;

public class BaseService<T, TDto> : IBaseService<T, TDto>
{
    private readonly IBaseRepo<T> _baseRepo;
    protected readonly IMapper _mapper;

    public BaseService(IBaseRepo<T> baseRepo, IMapper mapper)
    {
        _baseRepo = baseRepo;
        _mapper = mapper;
    }

    public TDto CreateOne(T newEntity)
    {
        return _mapper.Map<TDto>(_baseRepo.CreateOne(newEntity));
    }

    public bool DeleteOneById(string id)
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
            var foundEntity = _baseRepo.GetOneById(id);
            if (foundEntity != null)
            {
                _baseRepo.DeleteOneById(foundEntity);
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

    public IEnumerable<TDto> GetAll(QueryParameters queryParameters)
    {
        IEnumerable<TDto> getAllEntity = (IEnumerable<TDto>)_baseRepo.GetAll(queryParameters);
        return getAllEntity;
    }

    public TDto GetOneById(string id)
    {
        var foundEntity = _baseRepo.GetOneById(id);
        if (foundEntity != null)
        {
            return _mapper.Map<TDto>(foundEntity);
        }
        else
        {
            throw new Exception($"Entity with ID '{id}' not found.");
        }
    }

    public TDto UpdateOneById(string id, TDto newEntity)
    {
        var foundEntity = _baseRepo.GetOneById(id);
        if (foundEntity == null)
        {
            throw new Exception($"Item with {id} id not found");
        }
        else
        {
            var updated = _baseRepo.UpdateOneById(foundEntity, _mapper.Map<T>(newEntity));
            return _mapper.Map<TDto>(updated);
        }
    }
}
