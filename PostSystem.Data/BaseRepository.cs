using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PostSystem.Data
{
    public class BaseRepository<TEntity> where TEntity : class, new()
    {
        private readonly PostSystemDbContext dbContext;
        protected DbSet<TEntity> Entities { get; private set; }

        public BaseRepository(PostSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
            Entities = dbContext.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter != null)
            {
                return Entities
                    .Where(filter)
                    .ToList();
            }

            return Entities.ToList();
        }

        public virtual TEntity GetById(int id)
        {
            return Entities.Find(id);
        }

        public virtual TEntity Create(TEntity entity)
        {
            Entities.Add(entity);

            return entity;
        }

        public virtual void Update(TEntity entity)
        {
            Entities.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity)
        {
            Entities.Remove(entity);
        }
    }
}
