namespace GeeksBank.Core.Infrastructure.Caching
{
    public interface ICacheStoreItem
    {
        string CacheKey { get; }
    }
}
