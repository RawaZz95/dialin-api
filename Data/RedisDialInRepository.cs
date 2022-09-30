using System.Text.Json;
using DialInApi.Models;
using StackExchange.Redis;

namespace DialInApi.Data
{
    public class RedisDialInRepository : IDialInRepository
    {
        private IConnectionMultiplexer _redis;
        private readonly string _dbKey = "dialins";

        public RedisDialInRepository(IConnectionMultiplexer redis)
        {
            _redis = redis;
        } 

        public async Task CreateDialInAsync(DialIn dialIn)
        {
            if (dialIn == null)
            {
                throw new ArgumentNullException(nameof(dialIn));
            }

            var db = _redis.GetDatabase();
            
            var serialDialIn = JsonSerializer.Serialize(dialIn);

            await db.HashSetAsync(_dbKey, new HashEntry[] { new HashEntry(dialIn.DialInId, serialDialIn)});
        }

        public void DeleteDialInAsync(DialIn dialIn)
        {
            var db = _redis.GetDatabase();

            db.HashDelete(_dbKey, dialIn.DialInId);
        }

        public async Task<IEnumerable<DialIn?>?> GetAllDialInsAsync()
        {
            var db = _redis.GetDatabase();

            var completeSet = await db.HashGetAllAsync(_dbKey);

            if (completeSet.Length > 0) 
            {
                return Array.ConvertAll(completeSet, val => JsonSerializer.Deserialize<DialIn>(val.Value));
            }

            return new List<DialIn>();
        }

        public async Task<DialIn?> GetDialInByIdAsync(string dialInId)
        {
            var db = _redis.GetDatabase();

            var dialIn = await db.HashGetAsync(_dbKey, dialInId);
            if (!string.IsNullOrEmpty(dialIn))
            {
                return JsonSerializer.Deserialize<DialIn>(dialIn);
            }

            return null;
        }

        public async Task SaveChangesAsync()
        {
            Console.WriteLine("---> SaveChangesAsync in RedisDialInRepository called redundantly...");
            await Task.CompletedTask;
        }
    }
}