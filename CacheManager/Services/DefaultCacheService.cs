namespace CacheManager.Services
{
    using global::Sitecore.Caching;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class DefaultCacheService : ICacheService
    {
        public DefaultCacheService()
        {

        }

        public IEnumerable<CacheEntry> Get()
        {
            return GetInternal();
        }

        public IEnumerable<CacheEntry> Get(int pageIndex, int pageSize)
        {
            return GetInternal(pageIndex, pageSize);
        }

        private CacheEntry[] GetInternal(int? skip = null, int? take = null)
        {
            IEnumerable<ICacheInfo> allCaches = CacheManager.GetAllCaches();

            if (skip.HasValue)
            {
                allCaches = allCaches.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                allCaches = allCaches.Take(take.Value);
            }

            return allCaches.Select(Cast).ToArray();
        }

        private CacheEntry Cast(ICacheInfo value)
        {
            var match = Regex.Match(value.Name, "(.*)\\[(.*)\\]$");

            if (match != null && match.Success)
            {
                var prefix = match.Groups[1].Value;
                var type = match.Groups[2].Value;

                if (prefix == "core" || prefix == "master" || prefix == "web")
                {
                    return new DatabaseCacheEntry(value.Count, value.Id.ToString(), value.Name, value.Size, value.RemainingSpace)
                    {
                        Enabled = value.Enabled,
                        MaxSize = value.MaxSize,
                        Scavengable = value.Scavengable,
                        Database = prefix,
                        Type = type
                    };
                }
                else
                {
                    return new SiteCacheEntry(value.Count, value.Id.ToString(), value.Name, value.Size, value.RemainingSpace)
                    {
                        Enabled = value.Enabled,
                        MaxSize = value.MaxSize,
                        Scavengable = value.Scavengable,
                        Site = prefix,
                        Type = type
                    };
                }
            }

            match = Regex.Match(value.Name, "(.*) - Prefetch data\\((.*)\\)$");

            if (match != null && match.Success)
            {
                var providerType = match.Groups[1].Value;
                var database = match.Groups[2].Value;

                return new DatabaseCacheEntry(value.Count, value.Id.ToString(), value.Name, value.Size, value.RemainingSpace)
                {
                    Enabled = value.Enabled,
                    MaxSize = value.MaxSize,
                    Scavengable = value.Scavengable,
                    Database = database,
                    Type = $"{providerType} - Prefetch data"
                };
            }

            return new CacheEntry(value.Count, value.Id.ToString(), value.Name, value.Size, value.RemainingSpace)
            {
                Enabled = value.Enabled,
                MaxSize = value.MaxSize,
                Scavengable = value.Scavengable
            };
        }

        public int GetCount()
        {
            return Get().Count();
        }

        public void Clear(IEnumerable<string> cacheIds)
        {
            var caches = CacheManager.GetAllCaches();

            var hasCacheArgument = cacheIds != null && cacheIds.Any();

            foreach (var cache in caches)
            {
                if (hasCacheArgument && !cacheIds.Contains(cache.Id.ToString()))
                {
                    continue;
                }

                cache.Clear();
            }
        }
    }
}