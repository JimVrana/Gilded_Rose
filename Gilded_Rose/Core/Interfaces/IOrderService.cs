using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gilded_Rose.Core.Models;

namespace Gilded_Rose.Core.Interfaces
{
    public interface IOrderService
    {

        OrderItem CreateOrder(int iteitemId, int Quantity);

    }
}
