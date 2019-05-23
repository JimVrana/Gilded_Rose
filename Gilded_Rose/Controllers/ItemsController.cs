using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Gilded_Rose.Core.Models;
using Gilded_Rose.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Gilded_Rose.Controllers
{
    //[Authorize]
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
      //  [Authorize(Roles ="Admin,ApiUser")]
        [HttpGet]
        public ActionResult Get()
        {
            //return new string[] { "value1", "value2" };
            return  Ok(_itemRepository.ListAll());
        }

        // GET api/items/5
      //  [Authorize(Roles = "Admin,ApiUser")]
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
