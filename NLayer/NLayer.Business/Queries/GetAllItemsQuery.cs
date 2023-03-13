using MediatR;
using NLayer.Business.Models.Item;

namespace NLayer.Business.Queries
{
    public class GetAllItemsQuery : IRequest<IReadOnlyCollection<ItemDto>> { }
}
