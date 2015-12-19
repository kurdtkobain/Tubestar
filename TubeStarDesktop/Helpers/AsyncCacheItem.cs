using System;
using System.Threading;
using System.Threading.Tasks;

namespace TubeStar
{
    public enum AsyncCacheState
    {
        Empty,
        Fetching,
        Populated,
    }

    public class AsyncCacheItem<T> : IDisposable
    {
        public event EventHandler ValuePopulated;

        public T Value { get; private set; }
        public AsyncCacheState State { get; protected set; }

        private Func<T> _fetchMethod;

        protected CancellationTokenSource _cancellationTokenSource;
        protected CancellationToken _cancellationToken;

        public AsyncCacheItem(Func<T> fetchMethod, bool startNow = true)
            : this()
        {
            _fetchMethod = fetchMethod;

            if (startNow)
                Populate();
        }

        protected AsyncCacheItem()
        {
            State = AsyncCacheState.Empty;
        }

        public void Populate()
        {
            if (State == AsyncCacheState.Empty)
            {
                State = AsyncCacheState.Fetching;

                _cancellationTokenSource = new CancellationTokenSource();
                _cancellationToken = _cancellationTokenSource.Token;

                var task = StartTask();
                task.ContinueWith((t) =>
                {
                    Value = t.Result;
                    State = AsyncCacheState.Populated;
                    OnPopulated();
                });
            }
            else if (State == AsyncCacheState.Populated)
            {
                OnPopulated();
            }
        }

        protected virtual Task<T> StartTask()
        {
            return System.Threading.Tasks.Task.Factory.StartNew(_fetchMethod, _cancellationToken);
        }

        protected void OnPopulated()
        {
            if (ValuePopulated != null)
                ValuePopulated(Value, EventArgs.Empty);
        }

        public void Dispose()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
            }
        }

        /// <summary>
        /// Will wait until the cache is Populated.
        /// </summary>
        public void WaitForPopulate()
        {
            if (State != AsyncCacheState.Populated)
            {
                var autoResetEvent = new System.Threading.AutoResetEvent(false);
                EventHandler populateHandler = (sender, e) =>
                {
                    autoResetEvent.Set();
                };
                ValuePopulated += populateHandler;

                if (State == AsyncCacheState.Empty)
                {
                    Populate();
                }

                autoResetEvent.WaitOne();
                ValuePopulated -= populateHandler;
            }
        }
    }
}