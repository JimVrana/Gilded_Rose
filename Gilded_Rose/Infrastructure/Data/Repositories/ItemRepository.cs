using Gilded_Rose.Core.Interfaces;
using Gilded_Rose.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gilded_Rose.Infrastructure.Data.Repositories
{
    internal sealed class ItemRepository : EfRepository<Item>, IItemRepository
    {
        public ItemRepository(AppDbContext appDbContext) : base(appDbContext)
        {
           
        }

        public bool CheckIfItemExists(int itemId)
        {            
            Item item = (from c in this._appDbContext.Items where c.Id == itemId select c).SingleOrDefault();

            if (item == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public int CheckStock(int itemId)
        {
            int ItemCount = (from c in this._appDbContext.Items where c.Id == itemId select c.InventoryCount).SingleOrDefault();

            return ItemCount;
        }

        public OrderItem ValidateItem(OrderItem orderItem)
        {
            if(orderItem.ItemId <=0)
            {
                orderItem.isValid = false;
                orderItem.Message = "ItemId must be greater or equal to 1";
                return orderItem;
            }

            if (orderItem.Quantity <= 0)
            {
                orderItem.isValid = false;
                orderItem.Message = "Quantity must be greater or equal to 1";
                return orderItem;
            }

            orderItem.InventoryQuantity = CheckStock(orderItem.ItemId);
            orderItem.ItemExists = CheckIfItemExists(orderItem.ItemId);

            if(!orderItem.ItemExists)
            {
                orderItem.isValid = false;
                orderItem.Message = "This is an invalid Item.";
            }
            else if (orderItem.InventoryQuantity > 0 && orderItem.Quantity > orderItem.InventoryQuantity )
            {
                orderItem.isValid = false;
                orderItem.Message = "This order exceeds the maximum stock.";
            }
            else
            {
                orderItem.isValid = true;
                orderItem.Message = "Order successfully placed";
            }

            return orderItem;
        }
    }
}

