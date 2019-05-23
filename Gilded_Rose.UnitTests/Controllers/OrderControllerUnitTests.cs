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
using Gilded_Rose.Core.Services;

namespace Gilded_Rose.UnitTests.Controllers
{
    public class OrderControllerUnitTests
    {

        private OrderController _controller;
        private IOrderService _orderService;
        private IItemRepository _itemRepo;

        public OrderControllerUnitTests()
        {
            _itemRepo = new ItemRepositoryFake();
            _orderService = new OrderService(_itemRepo);
            _controller = new OrderController(_orderService);
        }

        [Fact]
        public void Submit_Order_with_valid_item_and_quantity_succeeds()
        {
            //Arrage 
            int ItemId = 3;
            int Quantity = 2;

            // Act 
            var result = _controller.Post(ItemId, Quantity);
            var message = ((ObjectResult)result).Value;

            // Assert          
            result.Should().BeOfType<OkObjectResult>();
            message.Should().Equals("Order successfully placed");
        }

        [Fact]
        public void Submit_Order_with_Zero_returns_bad_request()
        {
            //Arrage 
            int ItemId = 3;
            int Quantity = 0;

            // Act 
            var result = _controller.Post(ItemId, Quantity);
            var message = ((ObjectResult)result).Value;

            // Assert  
            result.Should().BeOfType<BadRequestObjectResult>();
            message.Should().Equals("Quantity must be greater or equal to 1");
        }

        [Fact]
        public void Submit_Order_with_Negative_quantity_returns_bad_request()
        {
            //Arrage 
            int ItemId = 3;
            int Quantity = -1;

            // Act 
            var result = _controller.Post(ItemId, Quantity);
            var message = ((ObjectResult)result).Value;

            // Assert          
            result.Should().BeOfType<BadRequestObjectResult>();
            message.Should().Equals("Quantity must be greater or equal to 1");
        }

        [Fact]
        public void Submit_Order_with_ItemId_0_returns_bad_request()
        {
            //Arrage 
            int ItemId = 0;
            int Quantity = 1;

            // Act 
            var result = _controller.Post(ItemId, Quantity);
            var message = ((ObjectResult)result).Value;

            // Assert          
            result.Should().BeOfType<BadRequestObjectResult>();
            message.Should().Equals("ItemId must be greater or equal to 1");
        }
        [Fact]
        public void Submit_Order_with_negative_ItemId_returns_bad_request()
        {
            //Arrage 
            int ItemId = -1;
            int Quantity = 1;

            // Act 
            var result = _controller.Post(ItemId, Quantity);
            var message = ((ObjectResult)result).Value;

            // Assert          
            result.Should().BeOfType<BadRequestObjectResult>();
            message.Should().Equals("ItemId must be greater or equal to 1");
        }
    }
}
