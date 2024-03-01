using ApplicationCore.Commands.Users;
using AutoMapper;
using Domain.Entities;

namespace ApplicationCore.Mappers;

public class UsersProfile : Profile
{
    public UsersProfile()
    {
        CreateMap<CreateUserCommand, Users>()
            .ForMember(x => x.PkCliente, y => y.Ignore());
    }
}