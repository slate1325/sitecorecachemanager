namespace CacheManager.Services
{
    using Models;
    using System.Collections.Generic;

    public interface ICacheService
    {
        IEnumerable<CacheEntry> Get();
        IEnumerable<CacheEntry> Get(int pageIndex, int pageSize);

        int GetCount();

        void Clear(IEnumerable<string> cacheIds);
    }
}