using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Twitter.Business.Repositories.Interfaces;
using Twitter.Core.Entity.Common;
using Twitter.Dal.Contexts;

namespace Twitter.Business.Repositories.Implements
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        TwitterContext _db { get; }

        public GenericRepository(TwitterContext db)
        {
            _db = db;
        }

        public DbSet<T> Table => _db.Set<T>();
        public IQueryable<T> GetAll(bool noTracking = true)
            => noTracking ? Table.AsNoTracking() : Table;

        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression)
        {
            return await Table.AnyAsync(expression);
        }
        public async Task CreateAsync(T data)
        {
            await Table.AddAsync(data);
        }
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void Remove(T data)
        {
            Table.Remove(data); 
        }

        public async Task<T> GetByIdAsync(int id,bool noTracking=true)
        {
            return noTracking ? await Table.AsNoTracking().SingleOrDefaultAsync(t => t.Id == id) : await Table.FindAsync(id);
        }
    }
}
