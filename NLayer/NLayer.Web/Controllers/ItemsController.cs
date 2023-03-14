using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NLayer.Business.Commands;
using NLayer.Business.Models.Item;
using NLayer.Business.Queries;
using NLayer.Web.Models.Item;

namespace NLayer.Web.Controllers
{
    public class ItemsController : BaseController
    {
        public ItemsController(ISender sender, IMapper mapper) : base(sender, mapper) { }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ItemViewModel>>> Get() {
            var source = await sender.Send(new GetAllItemsQuery());
            var result = mapper.Map<IReadOnlyCollection<ItemViewModel>>(source);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ItemViewModel>> Get(int id) {
            var source = await sender.Send(new GetItemQuery { Id = id });
            var result = mapper.Map<ItemViewModel>(source);
            
            return result is not null ? Ok(result) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post([FromBody] ItemCreateViewModel model) {
            var mappedModel = mapper.Map<ItemCreateDto>(model);
            await sender.Send(new CreateItemCommand { Item = mappedModel });

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] ItemUpdateViewModel item) {
            var mappedModel = mapper.Map<ItemUpdateDto>(item);
            await sender.Send(new UpdateItemCommand { Item = mappedModel });

            return Ok();
        }
    }
}
