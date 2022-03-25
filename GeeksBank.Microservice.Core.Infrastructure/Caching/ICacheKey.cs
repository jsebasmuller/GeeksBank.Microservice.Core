namespace GeeksBank.Microservice.Core.Infrastructure.Caching
{
    public interface ICacheKey<TItem>
    {
        string CacheKey { get; }
    }
}
