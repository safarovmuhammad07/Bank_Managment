using Azure;
using Domain.Entities;
using Infratructure.Responses;

namespace Infratructure.Interfaces;

public interface IExtraCustomer:IGenericService<Customer>
{
    Task<ApiResponse<int>> GetCustomerCount();
}