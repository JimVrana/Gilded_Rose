
using System.Net;
using System.Threading.Tasks;
using Gilded_Rose.Controllers;
using Gilded_Rose.Core.Models;
using Gilded_Rose.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System.Linq;
using FluentAssertions;
using System.Collections.Generic;


namespace Gilded_Rose.UnitTests.Controllers
{
    public class ItemsControllerUnitTests
    {
        private ItemsController _controller;
        private IItemRepository _repo;

        public ItemsControllerUnitTests()
        {
            _repo = new ItemRepositoryFake();
            _controller = new ItemsController(_repo);
        }


        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var result = _controller.Get();

            // Assert 
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            //Arrage 
            int ExpectedItemCount = 6;

            // Act 
            var result = _controller.Get();

            List<Item> items = (List<Item>)(((ObjectResult)result).Value);
            // Assert          
            items.Count().Should().Equals(ExpectedItemCount);
        }

        [Fact]
        public void GetById_WhenCalled_Returns_Expected_Item()
        {
            //Arrage
            int ItemId = 3;

            // Act 
            var result = _controller.Get(ItemId);
            Item item = (Item)(((ObjectResult)result).Value);

            // Assert          
            item.Id.Should().Equals(ItemId);
        }

        [Fact]
        public void GetById_WhenCalled_With_NonExistant_Id_Returns_No_Item()
        {
            //Arrange
            int ItemId = -1;

            // Act 
            var result = _controller.Get(ItemId);
            var message = ((ObjectResult)result).Value;

            // Assert          
            result.Should().BeOfType<BadRequestObjectResult>();
            message.Should().Equals("ItemId must be greater or equal to 1");
        }

    }

}

