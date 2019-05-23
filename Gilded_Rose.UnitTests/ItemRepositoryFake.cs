using Gilded_Rose.Core.Models;
using Gilded_Rose.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Gilded_Rose.UnitTests
{
    public class ItemRepositoryFake : IItemRepository
    {
        private readonly List<Item> _items;


        public ItemRepositoryFake()
        {
            _items = new List<Item>()
            {
                new Item(){Id = 1, Name = "Red Lego", Description = "A Red Lego", Price = 2, InventoryCount = 5},
                new Item(){Id = 2, Name = "Orange Lego", Description = "A Orange Lego", Price = 5, InventoryCount = 5},
                new Item(){Id = 3, Name = "Yellow Lego", Description = "A Yellow Lego", Price = 4, InventoryCount = 5},
                new Item(){Id = 4, Name = "Green Lego", Description = "A Green Lego", Price = 9, InventoryCount = 5},
                new Item(){Id = 5, Name = "Blue Lego", Description = "A Blue Lego", Price = 4, InventoryCount = 5},
                new Item(){Id = 6, Name = "Purple Lego", Description = "A Purple Lego", Price = 1, InventoryCount = 5}
            };

        }

        public bool CheckIfItemExists(int itemId)
        {
            Item item = (from c in _items where c.Id == itemId select c).SingleOrDefault();

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
            int ItemCount = (from c in _items where c.Id == itemId select c.InventoryCount).SingleOrDefault();

            return ItemCount;
        }

        public  Item GetById(int id)
        {
                return _items.Where(a => a.Id == id).FirstOrDefault();           
        }

        public  List<Item> ListAll()
        {
                return  _items;
        }

        public OrderItem ValidateItem(OrderItem orderItem)
        {
            if (orderItem.ItemId <= 0)
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

            if (!orderItem.ItemExists)
            {
                orderItem.isValid = false;
                orderItem.Message = "This is an invalid Item.";
            }
            else if (orderItem.InventoryQuantity > 0 && orderItem.Quantity > orderItem.InventoryQuantity)
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
