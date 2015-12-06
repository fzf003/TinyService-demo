using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TinyService.Infrastructure
{
    public class Worker 
    {
        private readonly string _actionName;
        private readonly Action _action;
        private CancellationTokenSource _cts = null;

        public string WorkerName
        {
            get { return _actionName; }
        }


        public Worker(string workerName, Action action)
        {
            _actionName = workerName;
            _action = action;
        }

        public void Start()
        {
            _cts = new CancellationTokenSource();

            Task.Factory.StartNew((state) =>
            {
                CancellationToken token = (CancellationToken)state;
                while (!token.IsCancellationRequested)
                {
                    this._action();
                }
            }, _cts.Token, TaskCreationOptions.LongRunning);

        }



        public void Stop()
        {
            _cts.Cancel();
            _cts.Dispose();
        }

    }
}
