using Azure;
using Domain.Entities;
using Infratructure.Responses;

namespace Infratructure.Interfaces;

public interface IExtraCustomer
{
    ApiResponse<int> GetCustomerCount();
}