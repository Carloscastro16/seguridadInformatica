using ApplicationCore.DTOs;
using ApplicationCore.DTOs.Log;
using ApplicationCore.DTOs.User;
using ApplicationCore.Wrappers;

namespace ApplicationCore.Interfaces
{
    public interface IDashboardService
    {
        Task<Response<object>> GetDataPaginated();
        Task<Response<string>> GetIp();
        Task<Response<int>> CreateLogs(LogsDto request);
        Task<Response<int>> UpdateUser(int pkcliente, UpdateUserDto request);
        Task<Response<int>> DeleteUser(int pkcliente);
        
    }
}
