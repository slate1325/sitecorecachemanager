namespace CacheManager.Eventing.Remote
{
    using CacheManager.Events;
    using global::Sitecore.Diagnostics;
    using global::Sitecore.Eventing;
    using global::Sitecore.Events;
    using global::Sitecore.Pipelines;
    using System;
    using System.Threading;

    public class RemoteEventMap
    {
        /// <summary>
        /// First event flag.
        /// </summary>
        private static int firstEvent;

        private readonly IEventQueue _queue;

        static RemoteEventMap()
        {
            firstEvent = 1;
        }

        public RemoteEventMap(IEventQueue queue)
        {
            _queue = queue;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public static void Initialize(IEventQueue queue)
        {
            SetupSubscribers(queue);
        }

        /// <summary>
        /// Initializes from pipeline.
        /// </summary>
        /// <param name="args">
        /// The pipeline args.
        /// </param>
        public void InitializeFromPipeline(PipelineArgs args)
        {
            Initialize(_queue);
        }

        /// <summary>
        /// Setups the remote events.
        /// </summary>
        private static void SetupSubscribers(IEventQueue queue)
        {
            if (Interlocked.Exchange(ref firstEvent, 0) == 1)
            {
                SetupRemoteEventSubscribers();
                SetupGlobalEventSubscribers(queue);
            }
        }

        /// <summary>
        /// Setups the remote event subscribers.
        /// </summary>
        private static void SetupRemoteEventSubscribers()
        {
            EventManager.Subscribe(new Action<CacheClearRemoteEvent>(OnCacheClearRemoteEvent));
        }

        /// <summary>
        /// Setups the global event subscribers.
        /// </summary>
        private static void SetupGlobalEventSubscribers(IEventQueue queue)
        {
            Event.Subscribe("cachemanager:clear:remote", (object sender, EventArgs args) => queue.QueueEvent(new CacheClearRemoteEvent(Event.ExtractParameter<CacheClearRemoteEventArgs>(args, 0).CacheIds)));
        }

        private static void OnCacheClearRemoteEvent(CacheClearRemoteEvent @event)
        {
            Assert.ArgumentNotNull(@event, "event");
            Event.RaiseEvent("cachemanager:clear:remote", new CacheClearRemoteEventArgs(@event));
        }
    }
}