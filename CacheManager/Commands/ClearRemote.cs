namespace CacheManager.Commands
{
    using CacheManager.Events;
    using global::Sitecore.Configuration;
    using global::Sitecore.Diagnostics;
    using global::Sitecore.Text;
    using global::Sitecore.Web.UI.Sheer;
    using System.Collections.Generic;
    using System.Linq;

    public class ClearRemote : BaseClear
    {
        public override void Handle(ClientPipelineArgs args, IEnumerable<string> ids)
        {
            Assert.ArgumentNotNull(args, nameof(args));
            Assert.ArgumentNotNull(args, nameof(ids));

            var targets = new ListString(args.Parameters["Targets"]);

            QueueEvent(new CacheClearRemoteEvent(ids.ToArray()), targets);
        }

        /// <summary>
        /// Queues the event.
        /// </summary>
        /// <typeparam name="T">Type of the event.</typeparam>
        /// <param name="event">The event instance.</param>
        protected void QueueEvent<T>(T @event, IEnumerable<string> targets)
        {
            foreach (string target in targets)
            {
                Factory.GetDatabase(target).RemoteEvents.EventQueue.QueueEvent(@event);
            }
        }
    }
}