namespace Domain.IRepository;

public interface IGenericRepositoryEF<TInterface, TDomain, TDataModel>
        where TInterface : class
        where TDomain : class, TInterface
        where TDataModel : class
{
    TInterface Add(TInterface entity);
    Task<TInterface> AddAsync(TInterface entity);
    void AddRange(IEnumerable<TInterface> entities);
    Task AddRangeAsync(IEnumerable<TInterface> entities);
    void Remove(TInterface entity);
    Task RemoveAsync(TInterface entity);
    void RemoveRange(IEnumerable<TInterface> entities);
    Task RemoveRangeAsync(IEnumerable<TInterface> entities);
    Task<int> SaveChangesAsync();
}
