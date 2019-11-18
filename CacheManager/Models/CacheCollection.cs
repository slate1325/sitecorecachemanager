namespace CacheManager.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class CacheCollection
    {
        internal CacheCollection(IEnumerable<CacheEntry> all, IEnumerable<CacheEntry> filtered, int total)
        {
            AllEntries = all;
            FilteredEntries = filtered;
            TotalEntries = total;
        }

        public IEnumerable<CacheEntry> AllEntries { get; }
        public IEnumerable<CacheEntry> FilteredEntries { get; }
        public int TotalEntries { get; }

        public IEnumerable<CacheEntry> Page(int pageIndex, int pageSize)
        {
            return FilteredEntries.Skip(pageIndex * pageSize).Take(pageSize);
        }
    }
}