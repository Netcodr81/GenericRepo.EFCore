using GenericRepo.EFCore.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GenericRepo.EFCore
{
    public class GenericRepositoryFactory<TEntity, TDataContext> : IGenericRepository<TEntity> where TEntity : class where TDataContext : DbContext
    {
        protected readonly IDbContextFactory<TDataContext> _contextFactory;

        public GenericRepositoryFactory(IDbContextFactory<TDataContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        #region Delete Methods

        public virtual bool Delete(object id)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                TEntity entityToDelete = context.Set<TEntity>().Find(id);
                return Delete(entityToDelete);
            }
        }

        public virtual bool Delete(TEntity entityToDelete)
        {
            using var context = _contextFactory.CreateDbContext();

            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                context.Set<TEntity>().Attach(entityToDelete);
            }
            context.Set<TEntity>().Remove(entityToDelete);
            return context.SaveChanges() >= 1;

        }

        public virtual async Task<bool> DeleteAsync(TEntity entityToDelete)
        {
            using var context = _contextFactory.CreateDbContext();

            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                context.Set<TEntity>().Attach(entityToDelete);
            }
            context.Set<TEntity>().Remove(entityToDelete);
            return await context.SaveChangesAsync() >= 1;

        }

        public virtual async Task<bool> DeleteAsync(object id)
        {
            using var context = _contextFactory.CreateDbContext();

            TEntity entityToDelete = await context.Set<TEntity>().FindAsync(id);
            return await DeleteAsync(entityToDelete);

        }

        #endregion

        #region Get Methods

        public virtual TEntity Get(object id)
        {
            using var context = _contextFactory.CreateDbContext();
            return context.Set<TEntity>().Find(id);
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includedProperties)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                // Get the dbSet from the Entity passed in                
                IQueryable<TEntity> query = context.Set<TEntity>();

                // Apply the filter
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                // Include the specified properties

                if (includedProperties != null)
                {

                    foreach (var includedProperty in includedProperties)
                    {
                        query = query.Include(includedProperty);
                    }
                }

                return query.ToList();

            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public virtual List<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includedProperties)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                // Get the dbSet from the Entity passed in                
                IQueryable<TEntity> query = context.Set<TEntity>();

                // Include the specified properties

                if (includedProperties != null)
                {

                    foreach (var includedProperty in includedProperties)
                    {
                        query = query.Include(includedProperty);
                    }
                }

                return query.ToList();

            }
            catch (Exception ex)
            {
                return null;
            }

        }


        public virtual async Task<List<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includedProperties)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                // Get the dbSet from the Entity passed in                
                IQueryable<TEntity> query = context.Set<TEntity>();

                // Include the specified properties
                if (includedProperties != null)
                {
                    foreach (var includeProperty in includedProperties)
                    {
                        query = query.Include(includeProperty);
                    }
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public virtual async Task<TEntity> GetAsync(object id)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includedProperties)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                // Get the dbSet from the Entity passed in                
                IQueryable<TEntity> query = context.Set<TEntity>();

                // Apply the filter
                if (filter != null)
                {
                    query = query.Where(filter);
                }


                // Include the specified properties
                if (includedProperties != null)
                {
                    foreach (var includeProperty in includedProperties)
                    {
                        query = query.Include(includeProperty);
                    }
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region Add Methods

        public virtual TEntity Insert(TEntity entity)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Set<TEntity>().Add(entity);
            context.SaveChanges();
            return entity;
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            using var context = _contextFactory.CreateDbContext();
            await context.Set<TEntity>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        #endregion

        #region Update Methods

        public virtual TEntity Update(TEntity entityToUpdate)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Set<TEntity>().Update(entityToUpdate);
            context.SaveChanges();
            return entityToUpdate;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entityToUpdate)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Set<TEntity>().Update(entityToUpdate);
            await context.SaveChangesAsync();
            return entityToUpdate;
        }

        #endregion
    }
}
