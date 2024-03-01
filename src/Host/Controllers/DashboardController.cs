using ApplicationCore.Commands.Log;
using ApplicationCore.Commands.Users;
using ApplicationCore.DTOs.Log;
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

        [Route("getData")]
        [HttpGet()]
        public async Task<IActionResult> GetUsuarios()
        {
            var result = await _service.GetData();
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<ActionResult<Response<int>>> Create(CreateUserCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("getIp")]
        public async Task<ActionResult<Response<string>>> GetIp()
        {
            var result = _service.GetIp();
            return Ok(result);
        }
        
        [HttpPost("createLogs")]
        public async Task<ActionResult<Response<int>>> CreateLogs([FromBody] LogsDto request)
        {
            var result = await _service.CreateLogs(request);
            return Ok(result);
        }
    }
}
