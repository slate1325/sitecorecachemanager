namespace CacheManager.Commands
{
    using CacheManager.Services;
    using global::Sitecore.Diagnostics;
    using global::Sitecore.Shell.Framework.Commands;
    using global::Sitecore.Text;
    using global::Sitecore.Web.UI.Sheer;
    using global::Sitecore.Web.UI.WebControls;
    using global::Sitecore.Web.UI.XamlSharp.Continuations;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class BaseClear : Command, ISupportsContinuation
    {
        protected readonly ICacheService _cacheService;

        public BaseClear(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        
        public BaseClear()
            : this (new DefaultCacheService())
        {

        }

        public override void Execute(CommandContext context)
        {
            Assert.ArgumentNotNull(context, "context");

            var id = context.Parameters["Id"];
            if (string.IsNullOrEmpty(id))
            {
                SheerResponse.Alert("Select a cache first.");
                return;
            }
            
            ClientPipelineArgs args = new ClientPipelineArgs(context.Parameters);
            ContinuationManager.Current.Start(this, "Run", args);
        }

        public virtual void Run(ClientPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, "args");

            var idParameter = args.Parameters["Id"];

            var idList = new ListString(idParameter);

            var ids = idParameter == "all" ? Enumerable.Empty<string>() : idList.Items;

            Handle(args, ids);

            SheerResponse.SetLocation(string.Empty);
        }

        public abstract void Handle(ClientPipelineArgs args, IEnumerable<string> ids);
    }
}