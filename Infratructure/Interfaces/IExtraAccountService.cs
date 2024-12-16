using Domain.Entities;
using Infratructure.Responses;

namespace Infratructure.Interfaces;

public interface IExtraAccountService
{
    ApiResponse<List<Account>> GetByStatus(string status);
    public ApiResponse<List<Account>> GetByType(string type);
}