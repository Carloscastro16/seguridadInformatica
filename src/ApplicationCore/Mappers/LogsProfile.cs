using ApplicationCore.Commands.Log;
using AutoMapper;
using Domain.Entities;

namespace ApplicationCore.Mappers;

public class LogsProfile : Profile
{
    public LogsProfile()
    {
        CreateMap<CreateLogCommand, Logs>()
            .ForMember(x => x.Id, y => y.Ignore());
    }
}