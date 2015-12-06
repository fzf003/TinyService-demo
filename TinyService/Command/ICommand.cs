using System;
using System.Collections.Generic;
using System.Linq;
 using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure;
namespace TinyService.Command
{
    public interface ICommand:IMessage
    {
        string AggregateRootId { get; set; }
    }
}
