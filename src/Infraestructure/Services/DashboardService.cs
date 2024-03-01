using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using ApplicationCore.Wrappers;
using AutoMapper;
using Dapper;
using Infraestructure.Persistence;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using System.Net;
using ApplicationCore.DTOs.Log;
using Microsoft.AspNetCore.Http;

namespace Infraestructure.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public DashboardService(ApplicationDbContext dbContext, ICurrentUserService currentUserService, IMapper mapper)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<Response<object>> GetData()
        {
            object list = new object();
            list = await _dbContext.Users.ToListAsync();
            return new Response<object>(list);
        }
        public async Task<Response<string>> GetIp()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            var ip = ipAddress?.ToString() ?? "No se encontro la ip";
            return new Response<string>(ip);
        }

        public async Task<Response<int>> CreateLogs(LogsDto request)
        {
            var ipAddress = await GetIp();
            var dd = ipAddress.Message;

            var u = new LogsDto();
            u.NombreFuncion = request.NombreFuncion;
            u.Fecha = request.Fecha;
            u.Ip = dd.ToString();
            u.Response = request.Response;
            u.Datos = request.Datos;
        
            var us = _mapper.Map<Domain.Entities.Logs>(u);
            
            await _dbContext.Logs.AddAsync(us);
            await _dbContext.SaveChangesAsync();
            return new Response<int>(us.Id, "Registro Creado");
        }
    }
}
