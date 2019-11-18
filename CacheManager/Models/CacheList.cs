namespace CacheManager.Models
{
	public class CacheList
	{
		public CacheList(CacheEntry[] caches)
		{
			Caches = caches;
		}

		public CacheEntry[] Caches { get; }
	}
}