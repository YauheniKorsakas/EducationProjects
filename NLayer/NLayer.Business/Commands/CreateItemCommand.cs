using MediatR;
using NLayer.Business.Models.Item;

namespace NLayer.Business.Commands
{
    public class CreateItemCommand : IRequest
    {
        public ItemCreateDto Item { get; set; }
    }
}
