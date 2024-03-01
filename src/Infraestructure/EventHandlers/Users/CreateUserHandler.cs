using ApplicationCore.Commands.Users;
using ApplicationCore.Wrappers;
using AutoMapper;
using Infraestructure.Persistence;
using MediatR;

namespace Infraestructure.EventHandlers.Users;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, Response<int>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateUserHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var u = new CreateUserCommand();
        u.Nombre = request.Nombre;
        u.Apellido1 = request.Apellido1;
        u.Apellido2 = request.Apellido2;
        u.Ciudad = request.Ciudad;
        u.fk_categoria = request.fk_categoria;
        
        var us = _mapper.Map<Domain.Entities.Users>(u);
        await _context.Users.AddAsync(us);
        await _context.SaveChangesAsync();
        return new Response<int>(us.PkCliente, "Registro Creado");
    }
}