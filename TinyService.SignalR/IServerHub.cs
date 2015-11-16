using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.MessageBus.Contract;


namespace TinyService.MessageBus
{
    public interface IServerHub
    {
        /// <summary>
        /// 发送消息(调用者)
        /// </summary>
        /// <param name="message"></param>
        void Send(AppMessage message);
        /// <summary>
        /// 加入一个话题
        /// </summary>
        /// <param name="topicName"></param>
        void JoinTopic(string topicName);

        /// <summary>
        /// 离开一个话题
        /// </summary>
        /// <param name="topicName"></param>
        void LeaveTopic(string topicName);
        /// <summary>
        /// 发送消息到这个话题上
        /// </summary>
        /// <param name="message"></param>
        /// <param name="topicName"></param>

        void SendMessageToTopic(AppMessage message, string topicName);
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="message"></param>
        void Publish(AppMessage message);
 
    }

}
