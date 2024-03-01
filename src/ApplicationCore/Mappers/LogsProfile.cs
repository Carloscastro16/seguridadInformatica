using ApplicationCore.Commands.Log;
using ApplicationCore.DTOs.Log;
using AutoMapper;
using Domain.Entities;

namespace ApplicationCore.Mappers;

public class LogsProfile : Profile
{
    public LogsProfile()
    {
        CreateMap<LogsDto, Logs>()
            .ForMember(x => x.Id, y => y.Ignore());
    }
}