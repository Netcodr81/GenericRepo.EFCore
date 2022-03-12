using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GenericRepo.EFCore.Contracts
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {

        Task<TEntity> GetAsync(object id);
        TEntity Get(object id);
        Task<List<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includedProperties);
        List<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includedProperties);

        Task<IEnumerable<TEntity>> GetAsync(
          Expression<Func<TEntity, bool>> filter = null,
          params Expression<Func<TEntity, object>>[] includedProperties);

        IEnumerable<TEntity> Get(
           Expression<Func<TEntity, bool>> filter = null,
           params Expression<Func<TEntity, object>>[] includedProperties);


        Task<bool> DeleteAsync(TEntity entityToDelete);
        Task<bool> DeleteAsync(object id);
        bool Delete(object id);
        bool Delete(TEntity entityToDelete);

        Task<TEntity> InsertAsync(TEntity entity);
        TEntity Insert(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entityToUpdate);
        TEntity Update(TEntity entityToUpdate);
    }
}
