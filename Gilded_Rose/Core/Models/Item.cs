using Gilded_Rose.Core.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gilded_Rose.Core.Models
{
    public class Item: BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int InventoryCount { get; set; }


        public Item() { }

        [JsonConstructor]
        internal  Item(string name, string description, int price)
        {
            Name = name;
            Description = description;
            Price = price;
        }
    }
}
