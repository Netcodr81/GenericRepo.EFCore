using GenericRepo.EFCore.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GenericRepo.EFCore
{
    public class GenericRepository<TEntity, TDataContext> : IGenericRepository<TEntity> where TEntity : class where TDataContext : DbContext
    {
        protected readonly TDataContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(TDataContext dataContext)
        {
            context = dataContext;
            dbSet = context.Set<TEntity>();
        }

        #region Delete Methods

        public virtual bool Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            return Delete(entityToDelete);
        }

        public virtual bool Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
            return context.SaveChanges() >= 1;
        }

        public virtual async Task<bool> DeleteAsync(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
            return await context.SaveChangesAsync() >= 1;
        }

        public virtual async Task<bool> DeleteAsync(object id)
        {
            TEntity entityToDelete = await dbSet.FindAsync(id);
            return await DeleteAsync(entityToDelete);
        }

        #endregion

        #region Get Methods

        public virtual TEntity Get(object id)
        {
            return dbSet.Find(id);
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includedProperties)
        {
            try
            {
                // Get the dbSet from the Entity passed in                
                IQueryable<TEntity> query = dbSet;

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
                // Get the dbSet from the Entity passed in                
                IQueryable<TEntity> query = dbSet;

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
                // Get the dbSet from the Entity passed in                
                IQueryable<TEntity> query = dbSet;

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
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includedProperties)
        {
            try
            {
                // Get the dbSet from the Entity passed in                
                IQueryable<TEntity> query = dbSet;

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
            dbSet.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        #endregion

        #region Update Methods

        public virtual TEntity Update(TEntity entityToUpdate)
        {

            context.Set<TEntity>().Update(entityToUpdate);
            context.SaveChanges();
            return entityToUpdate;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entityToUpdate)
        {

            context.Set<TEntity>().Update(entityToUpdate);
            await context.SaveChangesAsync();
            return entityToUpdate;
        }

        #endregion


    }
}
