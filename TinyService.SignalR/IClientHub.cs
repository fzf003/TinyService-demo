using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.MessageBus.Contract;
 

namespace TinyService.MessageBus
{
    public interface IClientHub
    {
        void Close(AppMessage message);
        /// <summary>
        /// 回复
        /// </summary>
        /// <param name="message"></param>
        void Receive(AppMessage message);
        /// <summary>
        /// 通知
        /// </summary>
        /// <param name="message"></param>
        void Notify(AppMessage message);
    }
}
