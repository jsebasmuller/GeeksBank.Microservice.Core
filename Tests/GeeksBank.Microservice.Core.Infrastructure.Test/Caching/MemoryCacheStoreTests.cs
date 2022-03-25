using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using GeeksBank.Microservice.Core.Infrastructure.Caching;
using GeeksBank.Microservice.Core.Infrastructure.Models;
using Xunit;

namespace GeeksBank.Microservice.Core.Infrastructure.Test.Caching
{
    public class MemoryCacheStoreTests
    {
        internal class MemCacheKey : ICacheKey<Product>
        {
            public string CacheKey => "ProductCacheKey";
        }
        
        // Controller for Testing
        private readonly MemoryCacheStore _cache;
        private readonly IMemoryCache _memoryCache;
        private readonly Dictionary<string, TimeSpan> _expirationConfiguration;
        private readonly MemCacheKey _cacheKey;

        private readonly Product _productMock;
        
        public MemoryCacheStoreTests()
        {
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            _expirationConfiguration = new Dictionary<string, TimeSpan>();
            _cache = new MemoryCacheStore(_memoryCache, _expirationConfiguration);
            _cacheKey = new MemCacheKey();
            _productMock = new Product();
            _productMock.ProductId = "1";
        }

        [Fact]
        public void GetKeyNotFoundTest()
        {
            // Act
            var result = _cache.Get(_cacheKey);
            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void GetKeyNotFoundByExpiredTest()
        {
            _cache.Add(_productMock, _cacheKey, DateTime.Now);
            // Act
            var result = _cache.Get(_cacheKey);
            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void GetKeyFoundNotExpiredTest()
        {
            _cache.Add(_productMock, _cacheKey, DateTime.Now.Add(TimeSpan.FromMinutes(30)));
            // Act
            var result = _cache.Get(_cacheKey);
            // Assert
            Assert.NotNull(result);
        }
        
        [Fact]
        public void GetKeyNotFoundRemovedTest()
        {
            _cache.Add(_productMock, _cacheKey, DateTime.Now.Add(TimeSpan.FromMinutes(30)));
            // Act
            _cache.Remove(_cacheKey);
            var result = _cache.Get(_cacheKey);
            // Assert
            Assert.Null(result);
        }
    }
}
