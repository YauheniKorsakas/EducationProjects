using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NLayer.Web.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected readonly ISender sender;
        protected readonly IMapper mapper;

        public BaseController(ISender sender, IMapper mapper) {
            this.sender = sender ?? throw new ArgumentNullException(nameof(sender));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
