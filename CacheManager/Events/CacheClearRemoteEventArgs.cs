namespace CacheManager.Events
{
    public class CacheClearRemoteEventArgs : System.EventArgs
    {
        public string[] CacheIds { get; set; }

        public CacheClearRemoteEventArgs(CacheClearRemoteEvent @event)
        {
            CacheIds = @event.CacheIds;
        }
    }
}