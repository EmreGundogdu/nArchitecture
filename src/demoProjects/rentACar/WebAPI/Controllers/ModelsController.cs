using Application.Features.Brands.Queries.GetListBrand;
using Application.Features.Models.Queries.GetListModel;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListModelQuery getListModelQuery= new() { PageRequest = pageRequest };
            var result = await Mediator.Send(getListModelQuery);
            return Ok(result);
        }
    }
}
