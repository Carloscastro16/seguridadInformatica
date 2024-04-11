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
using System.Text.Json;
using ApplicationCore.Commands.Users;
using ApplicationCore.DTOs.Log;
using ApplicationCore.DTOs.User;
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
        
        public async Task<Response<string>> GetIp()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            var ip = ipAddress?.ToString() ?? "No se encontro la ip";
            return new Response<string>(ip);
        }

        public async Task<Response<int>> UpdateUser(int pkcliente, UpdateUserDto request)
        {
            var existingUser = await _dbContext.Set<Domain.Entities.Users>().FindAsync(pkcliente);

            if (existingUser == null)
            {
                return new Response<int>(0, "Usuario no encontrado");
            }

            existingUser.Nombre = request.Nombre;
            existingUser.Apellido1 = request.Apellido1;
            existingUser.Apellido2 = request.Apellido2;
            existingUser.Ciudad = request.Ciudad;
            existingUser.fk_categoria = request.fk_categoria;

            try
            {
                await _dbContext.SaveChangesAsync();
                var logsObject = new LogsDto();
                logsObject.Datos = "Usuario Actualizado";
                logsObject.Fecha = DateTime.Now;
                logsObject.NombreFuncion = "Actualizar Usuario";
                logsObject.Response = "Exito al Actualizar el usuario";
                await CreateLogs(logsObject);
                return new Response<int>(existingUser.PkCliente, "Usuario actualizado correctamente");
            }
            catch (DbUpdateException)
            {
                await _dbContext.SaveChangesAsync();
                var logsObject = new LogsDto();
                logsObject.Datos = "Usuario No se pudo actualizar";
                logsObject.Fecha = DateTime.Now;
                logsObject.NombreFuncion = "Actualizar Usuarios";
                logsObject.Response = "Error al borrar el usuario";
                await CreateLogs(logsObject);
                return new Response<int>(0, "Error al actualizar el usuario");
            }
        }
        public async Task<Response<int>> DeleteUser(int pkcliente)
        {

            try
            {
                var existingUser = await _dbContext.Set<Domain.Entities.Users>().FindAsync(pkcliente);

                if (existingUser.State == 0)
                {
                    return new Response<int>(0, "Usuario no encontrado");
                }

                existingUser.State = 0;
                await _dbContext.SaveChangesAsync();
                var logsObject = new LogsDto();
                logsObject.Datos = "Usuario borrado";
                logsObject.Fecha = DateTime.Now;
                logsObject.NombreFuncion = "Borrar Usuarios";
                var responseObject = new Response<int>( "Registro Creado");
                logsObject.Response = JsonSerializer.Serialize(responseObject);
                await CreateLogs(logsObject);
                return responseObject;
            }
            catch (DbUpdateException)
            {
                var logsObject = new LogsDto();
                logsObject.Datos = "Error al borrar el usuario";
                logsObject.Fecha = DateTime.Now;
                logsObject.NombreFuncion = "Borrar Usuarios";
                var responseObject = new Response<int>( "Registro No Creado");
                logsObject.Response = JsonSerializer.Serialize(responseObject);
                await CreateLogs(logsObject);
                return responseObject;
            }
            
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

        public async Task<Response<object>> GetDataPaginated()
        {

            int pageNumber = 1;
            int pageSize = 10;

            var list = await _dbContext.Users
                .OrderBy(x => x.PkCliente)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new Response<object>(list);
        }
    }
}
