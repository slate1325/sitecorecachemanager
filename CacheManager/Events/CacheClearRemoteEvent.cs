namespace CacheManager.Events
{
    using System.Runtime.Serialization;

    [DataContract]
    public class CacheClearRemoteEvent
    {
        public CacheClearRemoteEvent(string[] cacheIds)
        {
            CacheIds = cacheIds;
        }

        [DataMember]
        public string[] CacheIds { get; set; }
    }
}