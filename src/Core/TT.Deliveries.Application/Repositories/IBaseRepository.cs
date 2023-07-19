using System.Linq.Expressions;
using TT.Deliveries.Data.Dto;
namespace TT.Deliveries.Application.Repositories;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<T> InsertOne(T entity);
    Task InsertMany(IEnumerable<T> entity);
    Task UpdateOne(Expression<Func<T, string>> searchExpr, string id, Expression<Func<T, object>> setExpr, object setValue);
    Task Delete(Expression<Func<T, string>> searchExpr, string id);
    Task<T> GetByParam(IDictionary<Expression<Func<T, object>>, object> filters);
    Task<List<T>> GetAll();
}
