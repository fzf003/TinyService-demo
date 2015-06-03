using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.Domain.Entities
{
    public interface IEntity<T>
    {
        T ID { get; set; }
    }

    public interface IEntity:IEntity<long>
    {

    }
}
