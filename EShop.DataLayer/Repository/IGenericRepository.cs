using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Repository
{
    public interface IGenericRepository<TEntity> : IDisposable where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetEntitiesQuery();

        TEntity GetEntityById(long id);

        void AddEntity(TEntity entity);
        void AddRangeEntity(List<TEntity> entity);

        void UpdateEntity(TEntity entity);

        void RemoveEntity(TEntity entity);
        void RemoveRangeEntity(List<TEntity> entities);

        void RemoveEntity(long id);

        void SaveChanges();
    }
}