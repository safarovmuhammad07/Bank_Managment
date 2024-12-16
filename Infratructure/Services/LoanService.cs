using System.Net;
using Dapper;
using Domain.Entities;
using Infratructure.DataContext;
using Infratructure.Interfaces;
using Infratructure.Responses;
using Npgsql;

namespace Infratructure.Services;

public class LoanService(DapperContext context) : IGenericService<Loan>
{
    public async Task<ApiResponse<List<Loan>>> GetAll()
    {
        using var connection = context.Connection();
        const string sql = "select * from loans where deletedat is null";
        var res =await connection.QueryAsync<Loan>(sql);
        return new ApiResponse<List<Loan>>(res.ToList());
    }

    public async Task<ApiResponse<Loan>> GetById(int id)
    {
        using var connection = context.Connection();
        const string sql = "select * from loans where loanid = @Id and deletedat is null";
        var res = await connection.QuerySingleOrDefaultAsync<Loan>(sql, new { Id = id });
        return res == null ? new ApiResponse<Loan>(HttpStatusCode.NotFound, "Loan not found") : new ApiResponse<Loan>(res);
    }

    public async Task<ApiResponse<bool>> Add(Loan data)
    {
        using var connection = context.Connection();
        const string sql = "insert into loans(loanamount, dateissued, createdat, customerid, branchid) values(@LoanAmount, @DateIssued, current_date, @CustomerId, @BranchId); ";
        var res = await connection.ExecuteAsync(sql, data);
        return res == 0 ? new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error") : new ApiResponse<bool>(res > 0);
    }

    public async Task<ApiResponse<bool>> Update(Loan data)
    {
        using var connection = context.Connection();
        const string sql = "update loans set loanamount = @LoanAmount, dateissued = @DateIssued, customerid = @CustomerId, branchid = @BranchId where loanid = @LoanId and deletedat is null;";
        var res = await connection.ExecuteAsync(sql, data);
        return res == 0 ? new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error") : new ApiResponse<bool>(res > 0);
    }

    public async Task<ApiResponse<bool>> Delete(int id)
    {
        using var connection = context.Connection();
        const string sql = "update loans set deletedat = current_date where loanid = @Id and deletedat is null";
        var res = await connection.ExecuteAsync(sql, new { Id = id });
        return res == 0 ? new ApiResponse<bool>(HttpStatusCode.NotFound, "Loan not found or already deleted") : new ApiResponse<bool>(res > 0);
    }
}
