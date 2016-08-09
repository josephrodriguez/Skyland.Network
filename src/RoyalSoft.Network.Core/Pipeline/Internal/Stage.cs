#region using

using System;
using System.Collections.Concurrent;
using System.Threading;
using RoyalSoft.Network.Core.Pipeline.Handlers;
using RoyalSoft.Network.Core.Threading;

#endregion

namespace RoyalSoft.Network.Core.Pipeline.Internal
{
    class Stage<T>
    {
        private readonly IPipelineComponent<T> _pipelineComponent;
        private readonly CancellationToken _token;
        private readonly BlockingCollection<T> _buffer;
        private readonly Worker _worker;

        public int StageId { get; private set; }

        public Stage(IPipelineComponent<T> pipelineComponent, CancellationToken token, int stageId, int boundCapacity)
        {
            if(pipelineComponent == null)
                throw new ArgumentNullException();

            _pipelineComponent = pipelineComponent;
            _token = token;
            _buffer = new BlockingCollection<T>(boundCapacity);

            _worker = new Worker(ExecuteOnBackground);
            _worker.Start();

            StageId = stageId;
        }

        public event StageCompletedEventHandler<T> OnCompleted;
        public event StageErrorEventHandler<T> OnError;


        public void Execute(T element)
        {
            _buffer.Add(element, _token);
        }

        private void ExecuteOnBackground()
        {
            foreach (var element in _buffer.GetConsumingEnumerable())
            {
                if (_token.IsCancellationRequested) break;

                try
                {
                    var output = _pipelineComponent.Execute(element);
                    RaiseOnCompleted(output);
                }
                catch (Exception exception)
                {
                    var args = new ErrorArgs<T>(element, exception);
                    RaiseOnError(_pipelineComponent, args);
                }
            }
        }

        private void RaiseOnCompleted(T element)
        {
            if(OnCompleted == null) return;
            OnCompleted(this, element);
        }

        private void RaiseOnError(object sender, ErrorArgs<T> args)
        {
            if (OnError == null) return;
            OnError(sender, args);
        }
    }
}
