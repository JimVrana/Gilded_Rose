using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gilded_Rose.Core.Models
{
    public class OrderLine
    {

        public Item ItemOrdered { get; set; }
        public int Quantity { get; set; }

    }
}
