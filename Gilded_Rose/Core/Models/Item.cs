using Gilded_Rose.Core.Shared;
using Newtonsoft.Json;

namespace Gilded_Rose.Core.Models
{
    public class Item: BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int InventoryCount { get; set; }


        public Item() { }


    }
}
