using System.Transactions;
using Infratructure.Responses;
using Transaction = Domain.Entities.Transaction;

namespace Infratructure.Interfaces;

public interface IExtraTransactionService
{
    public ApiResponse<List<Transaction>> GetByStatus(string status);
}