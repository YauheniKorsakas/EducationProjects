using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NLayer.Business.Commands;
using NLayer.Business.Models.Order;
using NLayer.Business.Queries;
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
        public async Task<ActionResult<IEnumerable<OrderViewModel>>> Get() {
            var source = await sender.Send(new GetAllOrdersQuery());
            var result = mapper.Map<IReadOnlyCollection<OrderViewModel>>(source);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderViewModel>> Get(int id) {
            var source = await sender.Send(new GetOrderQuery { Id = id });
            var result = mapper.Map<OrderViewModel>(source);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post([FromBody] OrderCreateViewModel model) {
            var mappedModel = mapper.Map<OrderCreateDto>(model);
            await sender.Send(new CreateOrderCommand { Order = mappedModel });

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}