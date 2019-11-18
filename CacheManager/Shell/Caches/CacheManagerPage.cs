namespace CacheManager.Shell.Caches
{
    using ComponentArt.Web.UI;
    using CacheManager.Models;
    using CacheManager.Services;
    using global::Sitecore;
    using global::Sitecore.Common;
    using global::Sitecore.DependencyInjection;
    using global::Sitecore.Diagnostics;
    using global::Sitecore.Extensions;
    using global::Sitecore.Shell.Framework.Commands;
    using global::Sitecore.Web.UI.Grids;
    using global::Sitecore.Web.UI.WebControls;
    using global::Sitecore.Web.UI.XamlSharp.Xaml;
    using System;

    public class CacheManagerPage : XamlMainControl, IHasCommandContext
    {
        /// <summary>
        ///   The items.
        /// </summary>
        protected Grid Items;
        protected ICacheService _cacheService;

        public CacheManagerPage()
            : this ((ICacheService)ServiceLocator.ServiceProvider.GetService(typeof(ICacheService)))
        {
        }

        public CacheManagerPage(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"></see> event to initialize the page.
        /// </summary>
        /// <param name="e">
        /// An <see cref="T:System.EventArgs"></see> that contains the event data.
        /// </param>
        protected override void OnInit(EventArgs e)
        {
            Assert.ArgumentNotNull(e, "e");
            base.OnInit(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Load"></see> event.
        /// </summary>
        /// <param name="e">
        /// The <see cref="T:System.EventArgs"></see> object that contains the event data.
        /// </param>
        protected override void OnLoad(EventArgs e)
        {
            Assert.ArgumentNotNull(e, "e");
            base.OnLoad(e);
            Assert.CanRunApplication("Cache Manager");

            var cacheEntries = GetCacheEntries();
            ComponentArtGridHandler<CacheEntry>.Manage(Items, new GridSource<CacheEntry>(cacheEntries), !AjaxScriptManager.IsEvent);
            Items.LocalizeGrid();
        }

        /// <summary>
        /// Gets the cache entries.
        /// </summary>
        /// <returns>
        /// The cache entries.
        /// </returns>
        private IPageable<CacheEntry> GetCacheEntries()
        {
            return new Pageable<CacheEntry>(( int pageIndex, int pageSize) => _cacheService.Get(pageIndex * pageSize, pageSize), () => _cacheService.Get(), () => _cacheService.GetCount());
        }

        CommandContext IHasCommandContext.GetCommandContext()
        {
            CommandContext context = new CommandContext();
            var itemNotNull = Client.GetItemNotNull("/sitecore/content/Applications/Cache Manager/Ribbon", Client.CoreDatabase);
            context.RibbonSourceUri = itemNotNull.Uri;
            var selectedValues = GridUtil.GetSelectedValue("Items");
            if (!string.IsNullOrWhiteSpace(selectedValues))
            {
                context.Parameters["Id"] = selectedValues;
            }

            context.Parameters["Targets"] = "web";
            return context;
        }
    }
}