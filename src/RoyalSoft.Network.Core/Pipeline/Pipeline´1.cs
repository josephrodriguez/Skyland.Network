#region using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using RoyalSoft.Network.Core.Pipeline.Handlers;
using RoyalSoft.Network.Core.Pipeline.Internal;

#endregion

namespace RoyalSoft.Network.Core.Pipeline
{
    public class Pipeline<T> : IPipeline<T>
    {
        #region Fields

        private int _count;

        private readonly CancellationTokenSource _cancellationSource;
        private readonly IList<Stage<T>> _stages; 

        #endregion

        #region Events

        public event PipelineErrorEventHandler<T> OnError;

        public event PipelineCompletedEventHandler<T> OnCompleted;

        #endregion

        public Pipeline()
        {
            _stages = new List<Stage<T>>();
            _cancellationSource = new CancellationTokenSource();
        }

        public int Count { get; private set; }

        public void Execute(T element)
        {
            var root = _stages.FirstOrDefault();
            if(root == null)
                return;

            Count++;
            root.Execute(element);
        }

        public void Wait()
        {
        }

        public void Cancel()
        {
            _cancellationSource.Cancel();
        }

        public IPipeline<T> Register(IPipelineComponent<T> pipelineComponent)
        {
            var stageId = _stages.Count;

            var stage = new Stage<T>(pipelineComponent, _cancellationSource.Token, stageId, 10);
            stage.OnCompleted += StageOnCompleted;
            stage.OnError += StageOnError;

            _stages.Add(stage);
            return this;
        }

        private void StageOnError(object sender, ErrorArgs<T> args)
        {
            Count--;
            if (OnError == null) return;
            OnError(sender, args);
        }
        
        private void StageOnCompleted(object sender, T element)
        {
            var senderStage = sender as Stage<T>;
            if (senderStage == null)
                return;

            if (senderStage.StageId < _stages.Count - 1)
            {
                var nextStageId = senderStage.StageId + 1;
                var nextStage = _stages[nextStageId];

                nextStage.Execute(element);
            }
            else
            {
                Count--;
                if (OnCompleted != null) OnCompleted(element);
            }
        }
    }
}
