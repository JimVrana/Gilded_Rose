using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gilded_Rose.Core.Shared;

namespace Gilded_Rose.Core.Models
{
    public class Order : BaseModel
    {

        public int BuyerId { get; set; }

        private List<OrderLine> _orderLines = new List<OrderLine>();

        public List<OrderLine> OrderLines => _orderLines.ToList();
    }
}
