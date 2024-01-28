using ImagineBookStore.Core.Constants;
using ImagineBookStore.Core.Interfaces;
using LazyCache;

namespace ImagineBookStore.Core.Services;

/// <summary>
/// Implementation of caching service operations using an app cache.
/// </summary>
public class CacheService : ICacheService
{
    private readonly IAppCache _cache;

    /// <summary>
    /// Initializes a new instance of the <see cref="CacheService"/> class.
    /// </summary>
    /// <param name="cache">The app cache implementation used for caching operations.</param>
    public CacheService(IAppCache cache)
    {
        _cache = cache;
    }

    /// <inheritdoc cref="ICacheService.AddToken"/>
    public void AddToken(string key, string token, DateTime expiresAt)
        => _cache.Add($"{AuthKeys.CacheKey}:{key}", token, expiresAt);

    /// <inheritdoc cref="ICacheService.GetToken"/>
    public async Task<string> GetToken(string key)
        => await _cache.GetAsync<string>($"{AuthKeys.CacheKey}:{key}");

    /// <inheritdoc cref="ICacheService.RemoveToken"/>
    public void RemoveToken(string key)
        => _cache.Remove($"{AuthKeys.CacheKey}:{key}");
}