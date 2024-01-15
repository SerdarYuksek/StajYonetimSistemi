using StackExchange.Redis;

namespace InternService.Api.Services
{
    public interface IRedisService
    {
        void Connection(int dbId = 0);
        Task<string> StringGet(string key);
        Task StringSet(string key, string value);
        Task Keydelete(string key);
        IDatabaseAsync Database(int dbId = 0);
    }

    public class RedisService : IRedisService
    {
        private IConnectionMultiplexer _redis;
        private IDatabaseAsync _database;

        public RedisService(IConnectionMultiplexer redis, IDatabaseAsync database)
        {
            _redis = redis;
            _database = database;
        }

        public void Connection(int dbId = 0)
        {
            _database = _redis.GetDatabase(dbId);
        }

        public IDatabaseAsync Database(int dbId = 0)
        {
            return _redis.GetDatabase(dbId);
        }
        public async Task Keydelete(string key)
        {
            await _database.KeyDeleteAsync(key);
        }

        public async Task<string> StringGet(string key)
        {
            return await _database.StringGetAsync(key);
        }

        public async Task StringSet(string key, string value)
        {
            await _database.StringSetAsync(key, value);
        }
    }
}
