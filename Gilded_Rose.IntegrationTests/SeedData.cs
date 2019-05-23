using Gilded_Rose.Core.Models;
using Gilded_Rose.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gilded_Rose.IntegrationTests
{
    public static class SeedData
    {
        public static void PopulateTestData(AppDbContext dbContext)
        {

            dbContext.Items.Add(new Item() { Id = 1, Name = "Red Lego", Description = "A Red Lego", Price = 2, InventoryCount = 5 });
            dbContext.Items.Add(new Item() { Id = 2, Name = "Orange Lego", Description = "A Orange Lego", Price = 5, InventoryCount = 5 });
            dbContext.Items.Add(new Item() { Id = 3, Name = "Yellow Lego", Description = "A Yellow Lego", Price = 4, InventoryCount = 5 });
            dbContext.Items.Add(new Item() { Id = 4, Name = "Green Lego", Description = "A Green Lego", Price = 9, InventoryCount = 5 });
            dbContext.Items.Add(new Item() { Id = 5, Name = "Blue Lego", Description = "A Blue Lego", Price = 4, InventoryCount = 5 });
            dbContext.Items.Add(new Item() { Id = 6, Name = "Purple Lego", Description = "A Purple Lego", Price = 1, InventoryCount = 5 });


            dbContext.SaveChanges();
        }
    }
}
