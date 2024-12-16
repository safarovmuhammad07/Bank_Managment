using System.Net;
using Dapper;
using Domain.Entities;
using Infratructure.DataContext;
using Infratructure.Interfaces;
using Infratructure.Responses;
using Npgsql;

namespace Infratructure.Services;

public class AccountService(DapperContext context) : IGenericService<Account>,IExtraAccountService
{
    public ApiResponse<List<Account>> GetAll()
    {
        using var connection = context.Connection();
        const string sql = "select * from accounts where deletedat is null";
        var res = connection.Query<Account>(sql).ToList();
        return new ApiResponse<List<Account>>(res);
    }
    
    
    public ApiResponse<Account> GetById(int id)
    {
        using var connection = context.Connection();
        const string sql = "select * from accounts where accountid = @Id and deletedat is null";
        var res = connection.QuerySingleOrDefault<Account>(sql, new { Id = id });
        return res == null ? new ApiResponse<Account>(HttpStatusCode.NotFound, "Account not found") : new ApiResponse<Account>(res);
    }

    public ApiResponse<List<Account>> GetByStatus(string status)
    {
        using var connection = context.Connection();
        const string sql = "select * from accounts where status = @status";
        var res = connection.Query<Account>(sql, new { Status = status }).ToList();
        return new ApiResponse<List<Account>>(res);
    }
    
    
    public ApiResponse<List<Account>> GetByType(string type)
    {
        using var connection = context.Connection();
        const string sql = "select * from accounts where  accountType= @type";
        var res = connection.Query<Account>(sql, new { AccountType=type  }).ToList();
        return new ApiResponse<List<Account>>(res);
    }
    public ApiResponse<bool> Add(Account data)
    {
        using var connection = context.Connection();
        const string sql = "insert into accounts(balance, accountstatus, accounttype, currency, createdat) values(@Balance, @AccountStatus, @AccountType, @Currency, current_date);";
        var res = connection.Execute(sql, data);
        return res == 0 ? new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error") : new ApiResponse<bool>(res > 0);
    }

    public ApiResponse<bool> Update(Account data)
    {
        using var connection = context.Connection();
        const string sql = "update accounts set balance = @Balance, accountstatus = @AccountStatus, accounttype = @AccountType, currency = @Currency where accountid = @AccountId and deletedat is null;";
        var res = connection.Execute(sql, data);
        return res == 0 ? new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error") : new ApiResponse<bool>(res > 0);
    }

    public ApiResponse<bool> Delete(int id)
    {
        using var connection = context.Connection();
        const string sql = "update accounts set deletedat = current_date where accountid = @Id and deletedat is null";
        var res = connection.Execute(sql, new { Id = id });
        return res == 0 ? new ApiResponse<bool>(HttpStatusCode.NotFound, "Account not found or already deleted") : new ApiResponse<bool>(res > 0);
    }
}
