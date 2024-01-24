namespace ImagineBookStore.Core.Interfaces;

/// <summary>
/// Interface for caching service operations.
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// Adds a token to the cache with the specified key and expiration time.
    /// </summary>
    /// <param name="key">The key to identify the cached token.</param>
    /// <param name="value">The token value to be cached.</param>
    /// <param name="expiresAt">The expiration time for the cached token.</param>
    void AddToken(string key, string value, DateTime expiresAt);

    /// <summary>
    /// Retrieves a token from the cache using the specified key.
    /// </summary>
    /// <param name="key">The key to identify the cached token.</param>
    /// <returns>A task representing the asynchronous operation with the cached token value.</returns>
    Task<string> GetToken(string key);

    /// <summary>
    /// Removes a token from the cache using the specified key.
    /// </summary>
    /// <param name="key">The key to identify the cached token to be removed.</param>
    void RemoveToken(string key);
}