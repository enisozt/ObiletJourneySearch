using Microsoft.Extensions.Caching.Memory;
using ObiletJourneySearch.Services.Abstract;

namespace ObiletJourneySearch.Services.Concrete
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public MemoryCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public bool TryGetValue<T>(object key, out T value)
        {
            return _cache.TryGetValue(key, out value);
        }

        public void Set<T>(object key, T value, TimeSpan? absoluteExpiration = null)
        {
            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpiration ?? TimeSpan.FromHours(1)
            };
            _cache.Set(key, value, options);
        }

        public void Remove(object key)
        {
            _cache.Remove(key);
        }
    }
}
