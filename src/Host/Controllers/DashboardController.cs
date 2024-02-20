using ApplicationCore.Interfaces;
using ApplicationCore.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _service;
        private readonly IMediator _mediator;
        public DashboardController(IDashboardService service, IMediator mediator)
        {
            _service = service;
            _mediator = mediator;
        }
        /// <summary>
        ///  Get de todos mis usuarios
        /// </summary>
        /// <returns></returns>
        [Route("getData")]
        [HttpGet()]
        [Authorize]
        /*public async Task<IActionResult> GetUsuarios()
        {
            var result = await _service.GetData();
            return Ok(result);
        }*/
        public async Task<IActionResult> GastoPendienteArea()
        {
            var origin = Request.Headers["origin"];
            return Ok("test");
        }


    }
}
