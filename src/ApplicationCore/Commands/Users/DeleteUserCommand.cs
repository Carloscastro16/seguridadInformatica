using ApplicationCore.DTOs.User;
using ApplicationCore.Wrappers;
using MediatR;

namespace ApplicationCore.Commands.Users;

public class DeleteUserCommand : DeleteUserDto, IRequest<Response<int>>
{
    
}