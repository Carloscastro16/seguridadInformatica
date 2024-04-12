using ApplicationCore.Commands.Log;
using ApplicationCore.Commands.Users;
using ApplicationCore.DTOs.Log;
using ApplicationCore.DTOs.User;
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

        [Route("getDataPaginated")]
        [HttpGet()]
        public async Task<IActionResult> GetDataPaginated()
        {
            var result = await _service.GetDataPaginated();
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
        
        
        /*[Route("getPagination")]
        [HttpGet()]
        public async Task<ActionResult> GetDataPaginate()
        {
            var result = _service.GetDataPaginate();
            return Ok(result);
        }*/
        
        [HttpPost("createLogs")]
        public async Task<ActionResult<Response<int>>> CreateLogs([FromBody] LogsDto request)
        {
            var result = await _service.CreateLogs(request);
            return Ok(result);
        }
        [HttpGet("deleteUser/{pkcliente}")]
        public async Task<ActionResult<Response<int>>> DeleteUser(int pkcliente)
        {
            var result = await _service.DeleteUser(pkcliente);
            return Ok(result);
        }
        [HttpPost("updateUser/{pkcliente}")]
        public async Task<ActionResult<Response<int>>> UpdateUser(int pkcliente, [FromBody] UpdateUserDto request)
        {
            var result = await _service.UpdateUser(pkcliente, request);
            return Ok(result);
        }
    }
}
