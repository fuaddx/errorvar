using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Twitter.Core.Entity.Common;

namespace Twitter.Business.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        DbSet<T>Table {  get; }
        IQueryable<T> GetAll(bool noTracking = true);
        Task<T> GetByIdAsync(int id,bool noTracking = true);
        Task<bool> IsExistAsync(Expression<Func<T,bool>>expression);
        Task CreateAsync(T data);
        void Remove(T data);
        Task SaveAsync();
    }
}
