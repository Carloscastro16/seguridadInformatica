using System.Text.Json;
using ApplicationCore.Commands.Users;
using ApplicationCore.DTOs.Log;
using ApplicationCore.Interfaces;
using ApplicationCore.Wrappers;
using AutoMapper;
using Infraestructure.Persistence;
using MediatR;

namespace Infraestructure.EventHandlers.Users;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Response<int>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IDashboardService _dashboardService;
    public DeleteUserHandler(ApplicationDbContext context, IMapper mapper, IDashboardService dashboardService)
    {
        _context = context;
        _mapper = mapper;
        _dashboardService = dashboardService;
    }

    public async Task<Response<int>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var u = new DeleteUserCommand();
        u.PkCliente = request.PkCliente;

        var jsonData = JsonSerializer.Serialize(u);
        var us = _mapper.Map<Domain.Entities.Users>(u);
        await _context.Users.AddAsync(us);
        await _context.SaveChangesAsync();
        var logsObject = new LogsDto();
        
        logsObject.Datos = jsonData;
        logsObject.Fecha = DateTime.Now;
        logsObject.NombreFuncion = "Borrar Usuario";
        var responseObject = new Response<int>(us.PkCliente, "Usuario eliminado");
        logsObject.Response = JsonSerializer.Serialize(responseObject);
        await _dashboardService.CreateLogs(logsObject);
        return responseObject;
    }
}