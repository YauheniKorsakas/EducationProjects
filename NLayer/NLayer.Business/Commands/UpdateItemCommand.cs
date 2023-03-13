using MediatR;
using NLayer.Business.Models.Item;

namespace NLayer.Business.Commands
{
    public class UpdateItemCommand : IRequest
    {
        public ItemUpdateDto Item { get; set; }
    }
}
