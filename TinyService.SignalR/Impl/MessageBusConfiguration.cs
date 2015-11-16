using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.MessageBus.Impl
{
    /// <summary>
    /// 配置信息
    /// </summary>
    public class MessageBusConfiguration 
    {
        public string RemoteUrl { get; set; }
        public string QueryString { get; set; }

        public string HubName { get; set; }
    }
}
