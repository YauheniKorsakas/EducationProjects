using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NLayer.Business.Commands;
using NLayer.Business.Models.Customer;
using NLayer.Business.Queries;
using NLayer.Web.Models.Customer;

namespace NLayer.Web.Controllers
{
    public class CustomersController : BaseController
    {
        public CustomersController(ISender sender, IMapper mapper) : base(sender, mapper) { }

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
        public async Task<ActionResult<CustomerViewModel>> Get(int id) {
            var source = await sender.Send(new GetCustomerQuery() { Id = id });
            var result = mapper.Map<CustomerViewModel>(source);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post([FromBody] CustomerCreateViewModel model) {
            var mappedModel = mapper.Map<CustomerCreateDto>(model);
            await sender.Send(new CreateCustomerCommand { Customer = mappedModel });

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
