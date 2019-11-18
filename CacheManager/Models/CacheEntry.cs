namespace CacheManager.Models
{
	public class CacheEntry
	{
		internal CacheEntry (int count, string id, string name, long size, long remainingSpace)
		{
			Count = count;
			Id = id;
			Name = name;
			Size = size;
			RemainingSpace = remainingSpace;
		}

		public int Count { get; }
		public bool Enabled { get; set; }
		public string Id { get; }
		public long MaxSize { get; set; }
		public string Name { get; }
		public long RemainingSpace { get; }
		public bool Scavengable { get; set; }
		public long Size { get; }
		public string Type { get; set; }
	}
}