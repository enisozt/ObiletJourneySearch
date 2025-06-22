namespace ObiletJourneySearch.Services.Abstract
{
    public interface ICacheService
    {
        bool TryGetValue<T>(object key, out T value);
        void Set<T>(object key, T value, TimeSpan? absoluteExpiration = null);
        void Remove(object key);
    }
}
