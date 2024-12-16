using System.Net;
using Dapper;
using Domain.Entities;
using Infratructure.DataContext;
using Infratructure.Interfaces;
using Infratructure.Responses;
using Npgsql;

namespace Infratructure.Services;

public class TransactionService(DapperContext context) :IExtraTransactionService
{
    public async Task<ApiResponse<List<Transaction>>> GetAll()
    {
        using var connection = context.Connection();
        const string sql = "select * from transactions where deletedat is null";
        var res = await connection.QueryAsync<Transaction>(sql);
        return new ApiResponse<List<Transaction>>(res.ToList());
    }

    public async Task<ApiResponse<Transaction>> GetById(int id)
    {
        using var connection = context.Connection();
        const string sql = "select * from transactions where transactionid = @Id and deletedat is null";
        var res = await connection.QuerySingleOrDefaultAsync<Transaction>(sql, new { Id = id });
        return res == null ? new ApiResponse<Transaction>(HttpStatusCode.NotFound, "Transaction not found") : new ApiResponse<Transaction>(res);
    }

    public async Task<ApiResponse<List<Transaction>>> GetByStatus(string status)
    {
        using var connection = context.Connection();
        const string sql = "select * from transactions where status = @status";
        var res =  await connection.QueryAsync<Transaction>(sql, new { Status = status });
        return new ApiResponse<List<Transaction>>(res.ToList());
    }
    

    public async Task<ApiResponse<bool>> Add(Transaction data)
    {
        using var connection = context.Connection();
        const string sql = "insert into transactions(transactionstatus, dateissued, amount, createdat, fromaccountid, toaccountid) values(@TransactionStatus, @DateIssued, @Amount, current_date, @FromAccountId, @ToAccountId);";
        var res = await connection.ExecuteAsync(sql, data);
        return res == 0 ? new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error") : new ApiResponse<bool>(res > 0);
    }

    public async Task<ApiResponse<bool>> Update(Transaction data)
    {
        using var connection = context.Connection();
        const string sql = "update transactions set transactionstatus = @TransactionStatus, dateissued = @DateIssued, amount = @Amount, fromaccountid = @FromAccountId, toaccountid = @ToAccountId where transactionid = @TransactionId and deletedat is null;";
        var res = await connection.ExecuteAsync(sql, data);
        return res == 0 ? new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error") : new ApiResponse<bool>(res > 0);
    }

    public async Task<ApiResponse<bool>> Delete(int id)
    {
        using var connection = context.Connection();
        const string sql = "update transactions set deletedat = current_date where transactionid = @Id ";
        var res = await connection.ExecuteAsync(sql, new { Id = id });
        return res == 0 ? new ApiResponse<bool>(HttpStatusCode.NotFound, "Transaction not found or already deleted") : new ApiResponse<bool>(res > 0);
    }
}
