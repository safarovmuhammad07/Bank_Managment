using System.Transactions;
using Infratructure.Responses;
using Transaction = Domain.Entities.Transaction;

namespace Infratructure.Interfaces;

public interface IExtraTransactionService:IGenericService<Transaction>
{
     Task<ApiResponse<List<Transaction>>> GetByStatus(string status);
}