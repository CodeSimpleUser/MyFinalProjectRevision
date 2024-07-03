using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntitiyRepositoryBase <TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public void Add(TEntity entitiy)
        {
            //When the job done , it will be clear from the memory
            //It is better for performance
            //IDispsible pattern implement of C#

            using (TContext context = new TContext())
            {
                var AddedEntity = context.Entry(entitiy);
                AddedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entitiy)
        {
            using (TContext context = new TContext())
            {
                var DeletedEntitiy = context.Entry(entitiy);
                DeletedEntitiy.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();

            }
        }

        public void Update(TEntity entitiy)
        {
            using (var context = new TContext())
            {
                var UpdatedEntitiy = context.Entry(entitiy);
                UpdatedEntitiy.State = EntityState.Modified;
                context.SaveChanges();

            }
        }
    }
}
