namespace CacheManager.Caching
{
    using CacheManager.Events;
    using CacheManager.Services;
    using global::Sitecore.Diagnostics;
    using global::Sitecore.Events;
    using System;

    public class RemoteCacheClearer
    {
        private readonly ICacheService _cacheService;

        public RemoteCacheClearer(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public void ClearCache(object sender, EventArgs args)
        {
            Assert.ArgumentNotNull(sender, "sender");
            Assert.ArgumentNotNull(args, "args");

            CacheClearRemoteEventArgs clearRemoteEventArgs = null;

            if (args is SitecoreEventArgs)
            {
                clearRemoteEventArgs = Event.ExtractParameter<CacheClearRemoteEventArgs>(args, 0);
            }
            else if (args is CacheClearRemoteEventArgs)
            {
                clearRemoteEventArgs = (CacheClearRemoteEventArgs)args;
            }

            Assert.IsNotNull(clearRemoteEventArgs, "userNames");

            _cacheService.Clear(clearRemoteEventArgs.CacheIds);
        }
    }
}