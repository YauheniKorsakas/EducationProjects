using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NLayer.Business.Queries;
using NLayer.Web.Models.Command.Customer;
using NLayer.Web.Models.Query.Customer;

namespace NLayer.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ISender sender;
        private readonly IMapper mapper;

        public CustomersController(ISender sender, IMapper mapper) {
            this.sender = sender ?? throw new ArgumentNullException(nameof(sender));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyCollection<CustomerViewModel>>> Get() {
            var source = await sender.Send(new GetAllCustomersQuery());
            var result = mapper.Map<IReadOnlyCollection<CustomerViewModel>>(source);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CustomerViewModel> Get(int id) {
            throw new NotImplementedException();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Post([FromBody] CustomerCreateViewModel model) {
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
