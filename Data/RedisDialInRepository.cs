using System.Text.Json;
using DialInApi.Models;
using StackExchange.Redis;

namespace DialInApi.Data
{
    public class RedisDialInRepository : IDialInRepository
    {
        private IConnectionMultiplexer _redis;

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

            await db.HashSetAsync($"dialins", new HashEntry[] { new HashEntry(dialIn.DialInId, serialDialIn)});
        }

        public void DeleteDialInAsync(DialIn dialIn)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DialIn>> GetAllDialInsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DialIn?> GetDialInByIdAsync(string dialInId)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}