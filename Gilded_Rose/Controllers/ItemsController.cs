using Microsoft.AspNetCore.Mvc;
using Gilded_Rose.Core.Models;
using Gilded_Rose.Core.Interfaces;

namespace Gilded_Rose.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;

        public ItemsController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        // GET api/items
        [HttpGet]
        public ActionResult Get()
        {
            return  Ok(_itemRepository.ListAll());
        }

        // GET api/items/5
        [HttpGet("{itemId}")]
        public ActionResult Get(int itemId)
        {
            if (itemId <= 0)
            {
                return BadRequest(new { message = "ItemId must be greater or equal to 1" });
            }

            Item item = _itemRepository.GetById(itemId);

            return  Ok(item);
        }
    }
}
