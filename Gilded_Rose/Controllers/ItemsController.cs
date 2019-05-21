using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Gilded_Rose.Core.Models;
using Gilded_Rose.Core.Repositories;

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
        public async Task<ActionResult<IEnumerable<Item>>> Get()
        {
            return await _itemRepository.ListAll();
        }

        // GET api/items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> Get(int id)
        {
            return await _itemRepository.GetById(id);
        }

    }
}
