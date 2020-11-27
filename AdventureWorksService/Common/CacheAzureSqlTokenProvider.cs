using AdventureWorksService.WebApi.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventureWorksService.WebApi.Common
{
    // Decorator that caches tokens in the in-memory cache
    public class CacheAzureSqlTokenProvider : IAzureSqlTokenProvider
    {
        private const string _cacheKey = nameof(CacheAzureSqlTokenProvider);
        private readonly IAzureSqlTokenProvider _inner;
        private readonly IMemoryCache _cache;

        public CacheAzureSqlTokenProvider(
            IAzureSqlTokenProvider inner,
            IMemoryCache cache)
        {
            _inner = inner;
            _cache = cache;
        }

        public async Task<(string AccessToken, DateTimeOffset ExpiresOn)> GetAccessTokenAsync(CancellationToken cancellationToken = default)
        {
            return await _cache.GetOrCreateAsync(_cacheKey, async cacheEntry =>
            {
                var (token, expiresOn) = await _inner.GetAccessTokenAsync(cancellationToken);

                // AAD access tokens have a default lifetime of 1 hour, so we take a small safety margin
                cacheEntry.SetAbsoluteExpiration(expiresOn.AddMinutes(-10));

                return (token, expiresOn);
            });
        }

        public (string AccessToken, DateTimeOffset ExpiresOn) GetAccessToken()
        {
            return _cache.GetOrCreate(_cacheKey, cacheEntry =>
            {
                var (token, expiresOn) = _inner.GetAccessToken();

                // AAD access tokens have a default lifetime of 1 hour, so we take a small safety margin
                cacheEntry.SetAbsoluteExpiration(expiresOn.AddMinutes(-10));

                return (token, expiresOn);
            });
        }
    }

}
