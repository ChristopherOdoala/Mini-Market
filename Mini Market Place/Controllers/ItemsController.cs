using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mini_Market_Place.Services;
using Mini_Market_Place.ViewModels;

namespace Mini_Market_Place.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;
        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpPost]
        public IActionResult CreateItem([FromBody]CreateItemViewModel model)
        {
            var res = _itemService.CreateItem(model);
            if (res.ErrorMessages.Any())
            {
                return Ok(res.ErrorMessages);
            }

            return Ok(res);
        }
    }
}