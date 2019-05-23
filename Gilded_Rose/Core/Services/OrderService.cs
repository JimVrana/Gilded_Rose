using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gilded_Rose.Core.Interfaces;
using Gilded_Rose.Core.Models;

namespace Gilded_Rose.Core.Services
{
    public class OrderService : IOrderService
    {

        private IItemRepository ItemRepository;

        public OrderService(IItemRepository itemRepository)
        {
            ItemRepository = itemRepository;

        }


        public OrderItem CreateOrder(int itemId, int quantity)
        {

            OrderItem orderItem = new OrderItem(itemId, quantity);

            orderItem = ItemRepository.ValidateItem(orderItem);

            //if (itemId < 0)
            //    return false;

            //if (Quantity <= 0)
            //    return false;

            ////check if the item is valid
            //bool ValidItem = ItemRepository.CheckIfItemExists(itemId);


            //int InventoryCount = ItemRepository.CheckStock(itemId);


            //if (Quantity > InventoryCount)
            //{
            //    return false;
            //}
            //else
            //{
            //    return true;
            //}

            return orderItem;
        }
    }

}
