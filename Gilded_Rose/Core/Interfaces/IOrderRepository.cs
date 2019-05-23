using Gilded_Rose.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gilded_Rose.Core.Interfaces
{
    public interface IOrderRepository : IRepository<Item>
    {
       void Post(Item value);
    }
}
