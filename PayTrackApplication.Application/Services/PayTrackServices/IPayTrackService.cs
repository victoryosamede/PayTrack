using Microsoft.AspNetCore.Mvc;
using PayTrackApplication.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Application.Services.PayTrackServices
{
    public interface IPayTrackService<T> where T : BaseModel
    {
        Task<ActionResponse> AddEntity(T entity);
        Task<ActionResponse> UpdateEntity(T entity);
        Task<ActionResponse> DeleteEntity(T entity);
        Task<T?> GetById (int id);
        Task<List<T>> GetAll();
        Task<T?> FindOneByExpression(Expression<Func<T, bool>> expression);
    }  
}
