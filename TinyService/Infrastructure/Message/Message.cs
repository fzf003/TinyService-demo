using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.Infrastructure.Message
{
    public interface IMessage<T>
    {
        T Id { get; set; }
        DateTime Timestamp { get; set; }
    }

    public abstract class Message : IMessage<string>
    {

        public Message()
        {
            Id = Guid.NewGuid().ToString("N");
            Timestamp = DateTime.Now;
        }


        public string Id
        {
            get;
            set;
        }

        public DateTime Timestamp
        {
            get;
            set;
        }
    }
}
