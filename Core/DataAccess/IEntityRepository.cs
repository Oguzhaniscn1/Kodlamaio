using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{                                           //GEnereic constraint 
    public interface IEntityRepository<T> where T : class, IEntity,new() // t ientity olabilir veya ientityden türeyen class olabilir ve newlenebilir olmalı
    {
        List<T> GetAll(Expression<Func<T,bool>> filter=null);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
