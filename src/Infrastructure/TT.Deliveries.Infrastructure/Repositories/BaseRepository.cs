using System.Linq.Expressions;
using TT.Deliveries.Application.Repositories;
using TT.Deliveries.Data.Dto;
using TT.Deliveries.Persistence.Context;

namespace TT.Deliveries.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly MongoDBContext<T> dbContext;

        public BaseRepository(MongoDBContext<T> dbContext)
        {

            this.dbContext = dbContext;
        }

        public async Task<T> InsertOne(T entity)
        {
            return await dbContext.InsertOne(entity);
        }

        public async Task InsertMany(IEnumerable<T> entity)
        {
            await dbContext.InsertMany(entity);
        }

        public async Task UpdateOne(Expression<Func<T, string>> searchExpr, string id, Expression<Func<T, object>> setExpr, object setValue)
        {
            var result = await dbContext.UpdateOne(searchExpr, id, setExpr, setValue);
        }

        public async Task Delete(Expression<Func<T, string>> searchExpr, string id)
        {
            await dbContext.DeleteOne(searchExpr, id);
        }

        public async Task<T> GetByParam(IDictionary<Expression<Func<T, object>>, object> filters)
        {
            return await dbContext.GetByFilter(filters);
        }

        public async Task<List<T>> GetAll()
        {
            return await dbContext.GetAll();
        }

    }
}
