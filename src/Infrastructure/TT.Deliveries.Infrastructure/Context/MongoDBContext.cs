using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;
using TT.Deliveries.Domain.Common;

namespace TT.Deliveries.Persistence.Context
{
    public class MongoDBContext<TEntity>
    {
        private readonly IMongoCollection<TEntity> collection;
        private readonly ILogger<TEntity> logger;
        private readonly MongoDBSettings mongoDBSettings;

        public MongoDBContext(IMongoClient mongoClient,
        ILogger<TEntity> logger,
        IOptions<MongoDBSettings> mongoDbSettings)
        {
            this.logger = logger;
            this.mongoDBSettings = mongoDbSettings.Value;
            var database = mongoClient.GetDatabase(this.mongoDBSettings.DatabaseName);
            collection = database.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public async Task<TEntity> GetByFilter(IDictionary<Expression<Func<TEntity, object>>, object> filters)
        {
            logger.LogDebug("Getting single record from database.");
            try
            {

                var filterBuilder = new FilterDefinitionBuilder<TEntity>();
                var items = new List<FilterDefinition<TEntity>>();
                foreach (var item in filters)
                {
                    var filter = Builders<TEntity>.Filter.Eq(item.Key, item.Value);
                    items.Add(filter);

                }
                var result = await collection.FindAsync(filterBuilder.And(items)).Result.FirstOrDefaultAsync();
                return result;
            }
            catch
            {
                logger.LogCritical(
                    $"Error while getting single data from collection '{typeof(TEntity).Name}'.");
                throw;
            }
        }

        public async Task<List<TEntity>> GetAll()
        {
            logger.LogDebug("Getting multiple record from database.");
            try
            {
                var result = await collection.Find(_ => true).ToListAsync();
                return result;
            }
            catch
            {
                logger.LogCritical(
                    $"Error while getting all data from collection '{typeof(TEntity).Name}'.");
                throw;
            }
        }

        public async Task<TEntity> InsertOne(TEntity entity)
        {
            logger.LogDebug("Inserting new record into database.");

            try
            {
                await collection.InsertOneAsync(entity);
                return entity;
            }
            catch (Exception ex)
            {
                logger.LogCritical(
                    $"Error inserting single entity into collection '{typeof(TEntity).Name}'.");
                throw;
            }
        }

        public async Task<UpdateResult> UpdateOne(Expression<Func<TEntity, string>> searchExpr, string id, Expression<Func<TEntity, object>> setExpr, object setValue)
        {
            logger.LogDebug($"Updating '{typeof(TEntity).Name}' into database.");

            try
            {
                var filter = Builders<TEntity>.Filter.Eq(searchExpr, id);
                var update = Builders<TEntity>.Update.Set(setExpr, setValue);   //TODO - foreach if there are multiple 
                var result = await collection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = false });
                return result;
            }
            catch
            {
                logger.LogCritical(
                    $"Error inserting single entity into collection '{typeof(TEntity).Name}'.");
                throw;
            }
        }

        public async Task InsertMany(IEnumerable<TEntity> entities)
        {
            logger.LogDebug($"Inserting new {entities.Count()} into database.");

            try
            {
                await collection.InsertManyAsync(entities);
            }
            catch
            {
                logger.LogCritical(
                    $"Error inserting multiple ({entities.Count()}) entities into collection '{typeof(TEntity).Name}'.");
                throw;
            }
        }

        public async Task<DeleteResult> DeleteOne(Expression<Func<TEntity, string>> searchExpr, string id)
        {
            try
            {
                var filter = Builders<TEntity>.Filter.Eq(searchExpr, id);
                var result = await collection.DeleteOneAsync(filter);
                return result;
            }
            catch
            {
                logger.LogCritical(
                    $"Error deleting all records from Mongo Database collection '{typeof(TEntity).Name}'.");
                throw;
            }
        }
    }
}
