using Gilded_Rose.Core.Models;

namespace Gilded_Rose.Core.Interfaces
{
    public interface IItemRepository:IRepository<Item>
    {
        bool CheckIfItemExists(int itemId);

        int  CheckStock(int itemId);

        OrderItem ValidateItem(OrderItem orderItem);
    }
}
