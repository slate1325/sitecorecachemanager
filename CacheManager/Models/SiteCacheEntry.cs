namespace CacheManager.Models
{
	public class SiteCacheEntry : CacheEntry
	{
		internal SiteCacheEntry (int count, string id, string name, long size, long remainingSpace)
			: base(count, id,name, size, remainingSpace) { }

		public string Site { get; set; }
	}
}