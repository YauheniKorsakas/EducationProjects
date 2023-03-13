using MediatR;
using NLayer.Business.Models.Item;

namespace NLayer.Business.Queries
{
    public class GetItemQuery : IRequest<ItemDto>
    {
        public int Id { get; set; }
    }
}
