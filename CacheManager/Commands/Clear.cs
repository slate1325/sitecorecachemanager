namespace CacheManager.Commands
{
    using global::Sitecore.Diagnostics;
    using global::Sitecore.Web.UI.Sheer;
    using System.Collections.Generic;

    public class Clear : BaseClear
    {
        public override void Handle(ClientPipelineArgs args, IEnumerable<string> ids)
        {
            Assert.ArgumentNotNull(args, nameof(args));
            Assert.ArgumentNotNull(args, nameof(ids));

            _cacheService.Clear(ids);
        }
    }
}