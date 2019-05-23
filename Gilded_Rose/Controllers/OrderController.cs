using Gilded_Rose.Core.Interfaces;
using Gilded_Rose.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gilded_Rose.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private IOrderService OrderService;

        public OrderController(IOrderService orderService)
        {
            OrderService = orderService;
        }
     
        // POST api/Order/{id}/{quantity}
         [Authorize(Roles = "Admin,ApiUser")]
        [HttpPost("{itemId}/{quantity}")]
        public ActionResult Post(int itemId, int quantity)
        {
            OrderItem orderItem = OrderService.CreateOrder(itemId, quantity);

            if (!orderItem.isValid)
            {
                return BadRequest(new { message = orderItem.Message });
            }
            else
            {
                return Ok(new { message = orderItem.Message });
            }
        }
    }
}
