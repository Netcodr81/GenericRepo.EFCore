using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GenericRepo.EFCore.Contracts
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {

        Task<TEntity> GetAsync(object id);
        TEntity Get(object id);
        Task<List<TEntity>> GetAllAysnc();
        List<TEntity> GetAll();

        Task<IEnumerable<TEntity>> GetAsync(
          Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          string includeProperties = "");

        IEnumerable<TEntity> Get(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = "");


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
