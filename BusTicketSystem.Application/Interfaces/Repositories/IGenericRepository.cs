using BusTicketSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BusTicketSystem.Application.Interfaces.Repositories
{
    //T bir class olmalı ve BaseEntity'edn türemeli
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<List<T>> GetAllAsync();

        //Navigation Properties getirmek için params diyerek istediğimiz kadar tablo adı verebileceğiz
        Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);

        //Şartlı sorgular için Expression kullancaz
        Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> expression);

        Task<bool> AddAsync(T entity);
        bool Remove(T entity);
        bool Update(T entity);

    }
}
