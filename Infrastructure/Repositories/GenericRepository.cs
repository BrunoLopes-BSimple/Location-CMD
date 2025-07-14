using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public abstract class GenericRepositoryEF<TInterface, TDomain, TDataModel>
        where TInterface : class
        where TDomain : class, TInterface
        where TDataModel : class
{
    protected readonly DbContext _context;
    private readonly IMapper _mapper;
    public GenericRepositoryEF(DbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public TInterface Add(TInterface entity)
    {
        var domainEntity = (TDomain)entity;
        var dataModel = _mapper.Map<TDomain, TDataModel>(domainEntity);
        var dm = _context.Set<TDataModel>().Add(dataModel);

        return _mapper.Map<TDataModel, TDomain>(dm.Entity);
    }

    public async Task<TInterface> AddAsync(TInterface entity)
    {
        var domainEntity = (TDomain)entity;
        var dataModel = _mapper.Map<TDomain, TDataModel>(domainEntity);
        _context.Set<TDataModel>().Add(dataModel);
        await SaveChangesAsync();
        return _mapper.Map<TDataModel, TDomain>(dataModel);
    }

    public void AddRange(IEnumerable<TInterface> entities)
    {
        var dataModels = entities.Select(e => _mapper.Map<TDomain, TDataModel>((TDomain)e));
        _context.Set<TDataModel>().AddRange(dataModels);
    }

    public async Task AddRangeAsync(IEnumerable<TInterface> entities)
    {
        var dataModels = entities.Select(e => _mapper.Map<TDomain, TDataModel>((TDomain)e));
        _context.Set<TDataModel>().AddRange(dataModels);
        await SaveChangesAsync();
    }

    public void Remove(TInterface entity)
    {
        var domainEntity = (TDomain)entity;
        var dataModel = _mapper.Map<TDomain, TDataModel>(domainEntity);
        _context.Set<TDataModel>().Remove(dataModel);
    }

    public async Task RemoveAsync(TInterface entity)
    {
        var dataModel = _mapper.Map<TDomain, TDataModel>((TDomain)entity);
        _context.Set<TDataModel>().Remove(dataModel);
        await SaveChangesAsync();
    }

    public void RemoveRange(IEnumerable<TInterface> entities)
    {
        var dataModels = entities.Select(e => _mapper.Map<TDomain, TDataModel>((TDomain)e));
        _context.Set<TDataModel>().RemoveRange(dataModels);
    }

    public async Task RemoveRangeAsync(IEnumerable<TInterface> entities)
    {
        var dataModels = entities.Select(e => _mapper.Map<TDomain, TDataModel>((TDomain)e));
        _context.Set<TDataModel>().RemoveRange(dataModels);
        await SaveChangesAsync();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
