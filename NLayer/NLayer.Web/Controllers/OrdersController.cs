using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NLayer.Web.Models.Order;

namespace NLayer.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseController
    {
        public OrdersController(ISender sender, IMapper mapper) : base(sender, mapper) { }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<OrderViewModel>> Get() {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<OrderViewModel> Get(int id) {
            throw new NotImplementedException();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Post([FromBody] OrderCreateViewModel model) {
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}