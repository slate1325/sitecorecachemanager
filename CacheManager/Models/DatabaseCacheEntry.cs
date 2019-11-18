namespace CacheManager.Models
{
	public class DatabaseCacheEntry : CacheEntry
	{
		internal DatabaseCacheEntry (int count, string id, string name, long size, long remainingSpace)
			: base(count, id,name, size, remainingSpace) { }

		public string Database { get; set; }
	}
}