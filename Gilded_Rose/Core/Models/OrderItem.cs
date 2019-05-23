namespace Gilded_Rose.Core.Models
{
    public class OrderItem
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public int InventoryQuantity { get; set; }
        public bool isValid { get; set; }
        public bool ItemExists { get; set; }
        public string Message { get; set; }


        public OrderItem(int itemId, int qty)
        {
            ItemId = itemId;
            Quantity = qty;
        }
    }
}
