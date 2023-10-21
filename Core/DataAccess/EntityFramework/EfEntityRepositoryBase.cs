using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity,TContext>:IEntityRepository<TEntity>
        where TEntity : class,IEntity, new()
        where TContext:DbContext, new()
    {

        public void Add(TEntity entity)
        {
            //Idisposable pattern implementation, using bittiği anda garbage collectore gidip kendisini sıfırlıyor.
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity);//refereansı yakala
                addedEntity.State = EntityState.Added;//ekle
                context.SaveChanges();//kaydet
            }
        }

        public void Delete(TEntity entity)
        {
            //Idisposable pattern implementation, using bittiği anda garbage collectore gidip kendisini sıfırlıyor.
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);//refereansı yakala
                deletedEntity.State = EntityState.Deleted;//sil
                context.SaveChanges();//kaydet
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                return filter == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public void Update(TEntity entity)
        {
            //Idisposable pattern implementation, using bittiği anda garbage collectore gidip kendisini sıfırlıyor.
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);//refereansı yakala
                updatedEntity.State = EntityState.Modified;//değiştir
                context.SaveChanges();//kaydet
            }
        }
    }
}
