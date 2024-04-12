using System.Text.Json;
using ApplicationCore.Commands.Users;
using ApplicationCore.DTOs.Log;
using ApplicationCore.Interfaces;
using ApplicationCore.Wrappers;
using AutoMapper;
using Infraestructure.Persistence;
using MediatR;

namespace Infraestructure.EventHandlers.Users;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Response<int>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IDashboardService _dashboardService;
    public UpdateUserHandler(ApplicationDbContext context, IMapper mapper, IDashboardService dashboardService)
    {
        _context = context;
        _mapper = mapper;
        _dashboardService = dashboardService;
    }

    public async Task<Response<int>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var u = new UpdateUserCommand();
        u.Nombre = request.Nombre;
        u.Apellido1 = request.Apellido1;
        u.Apellido2 = request.Apellido2;
        u.Ciudad = request.Ciudad;
        u.fk_categoria = request.fk_categoria;

        var jsonData = JsonSerializer.Serialize(u);
        var us = _mapper.Map<Domain.Entities.Users>(u);
        await _context.Users.AddAsync(us);
        await _context.SaveChangesAsync();
        var logsObject = new LogsDto();
        
        logsObject.Datos = jsonData;
        logsObject.Fecha = DateTime.Now;
        logsObject.NombreFuncion = "Actualizar Usuarios";
        var responseObject = new Response<int>(us.PkCliente, "Registro Creado");
        logsObject.Response = JsonSerializer.Serialize(responseObject);
        await _dashboardService.CreateLogs(logsObject);
        return responseObject;
    }
}