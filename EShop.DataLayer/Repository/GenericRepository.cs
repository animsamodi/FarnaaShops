using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace EShop.DataLayer.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        #region constructor

        private readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            this._context = context;
            this._dbSet = this._context.Set<TEntity>();
        }

        #endregion


        public IQueryable<TEntity> GetEntitiesQuery()
        {
            return _dbSet.AsQueryable().Where(c=>!c.IsDelete);
        }

        public TEntity GetEntityById(long id)
        {
            return  _dbSet.SingleOrDefault(e => e.Id == id);
        }

        public void AddEntity(TEntity entity)
        {
            entity.CreateDate = DateTime.Now;
            entity.LastUpdateDate = entity.CreateDate;
             _dbSet.AddAsync(entity);
        }

        public void AddRangeEntity(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreateDate = DateTime.Now;
                entity.LastUpdateDate = entity.CreateDate;
                
            }
             _dbSet.AddRange(entities);
        }

        public void UpdateEntity(TEntity entity)
        {
            entity.LastUpdateDate = DateTime.Now;
            _dbSet.Update(entity);
        }

        public void RemoveEntity(TEntity entity)
        {
            entity.IsDelete = true;
            UpdateEntity(entity);
        }

        public void RemoveRangeEntity(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsDelete = true;
                UpdateEntity(entity);
            }
           
        }

        public  void RemoveEntity(long id)
        {
            var entity =  GetEntityById(id);
            RemoveEntity(entity);
        }

        public void SaveChanges()
        {
             _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
