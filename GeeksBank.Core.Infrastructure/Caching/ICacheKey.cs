namespace GeeksBank.Core.Infrastructure.Caching
{
    public interface ICacheKey<TItem>
    {
        string CacheKey { get; }
    }
}
