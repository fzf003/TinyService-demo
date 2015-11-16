using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure.Message;

namespace TinyService.Infrastructure.Process.Actor
{
   public  sealed class MailBox : IDisposable
    {
        private bool _disposed;
        public int Index { get; set; }

        public MailBox(BlockingCollection<ActorMessage> blockingCollection)
        {
            Messages = blockingCollection;
        }

        public BlockingCollection<ActorMessage> Messages { get; private set; }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Add(ActorMessage message)
        {
            Messages.Add(message);
        }


        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                Messages.Dispose();
            }

            
            _disposed = true;
        }


        ~MailBox()
        {
            Dispose(false);
        }
    }
}
