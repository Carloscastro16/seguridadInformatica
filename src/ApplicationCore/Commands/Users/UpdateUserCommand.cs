namespace ApplicationCore.Commands.Users;
using ApplicationCore.DTOs.User;
using ApplicationCore.Wrappers;
using MediatR;

public class UpdateUserCommand : UpdateUserDto, IRequest<Response<int>>
{
    
}