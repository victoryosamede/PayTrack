using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayTrackApplication.Application;
using PayTrackApplication.Application.Services.PayTrackServices;
using PayTrackApplication.Domain.Models;
using PayTrackApplication.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Infrastructure.Services
{
    public class PayTrackService<T> : IPayTrackService<T> where T : BaseModel
    {
        private readonly PayTrackApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        public PayTrackService(PayTrackApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public virtual async Task<ActionResponse> AddEntity(T entity)
        {
            _dbSet.Add(entity);
           return await Save();
        }

        public virtual async Task<ActionResponse> DeleteEntity(T entity)
        {
            _dbSet.Remove(entity);
           return await Save();
        }

        public virtual async Task<List<T>> GetAll()
        {
           return await _dbSet.ToListAsync();  
        }

        public virtual async Task<T?> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<ActionResponse> UpdateEntity(T entity)
        {
            _dbSet.Entry(entity).State = EntityState.Modified;
             return await Save();
        }
        public virtual async Task<T?> FindOneByExpression(Expression<Func<T, bool>> expression)
        {
            return await  _dbSet.Where(expression).FirstOrDefaultAsync();
        }

        private async Task<ActionResponse> Save()
        {
            var res = new ActionResponse();
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                res = new ActionResponse(ex);
            }
            finally
            {
                Dispose(true);
                Dispose();
            }
            return res;
            
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {   
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        private void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
