using System.Net;
using Dapper;
using Domain.Entities;
using Infratructure.DataContext;
using Infratructure.Interfaces;
using Infratructure.Responses;
using Npgsql;

namespace Infratructure.Services;

public class CustomerService(DapperContext context) :IExtraCustomer
{
    public async Task<ApiResponse<List<Customer>>> GetAll()
    {
        using var connection = context.Connection();
        string sql = "select * from customers";
        var res = await connection.QueryAsync<Customer>(sql);
        return new ApiResponse<List<Customer>>(res.ToList());
    }

    public async Task<ApiResponse<Customer>> GetById(int id)
    {
        using var connection = context.Connection();
        const string sql = "select * from customers where customerid = @Id";
        var res = await connection.QuerySingleOrDefaultAsync<Customer>(sql, new { Id = id });
        return res == null ? new ApiResponse<Customer>(HttpStatusCode.NotFound, "Customer not found") : new ApiResponse<Customer>(res);
    }

    public async Task<ApiResponse<int>> GetCustomerCount()
    {
        using var connection = context.Connection();
        string sql = "select count(CustomerId) from customers";
        var res = await connection.QuerySingleOrDefaultAsync<int>(sql);
        return new ApiResponse<int>(res);
    }
    public async Task<ApiResponse<bool>> Add(Customer data)
    {
        using var connection = context.Connection();
        const string sql = "insert into customers(firstname, lastname, city, phonenumber, pancardno, dob, createdat) values(@firstname, @lastname, @city, @phonenumber, @pancardno, @dob, current_date);";
        var res = await connection.ExecuteAsync(sql, data);
        return res == 0 ? new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error") : new ApiResponse<bool>(res > 0);
    }

    public async Task<ApiResponse<bool>> Update(Customer data)
    {
        using var connection = context.Connection();
        const string sql = "update customers set firstname = @firstname, lastname = @lastname, city = @city, phonenumber = @phonenumber, pancardno = @pancardno, dob = @dob where customerid = @customerid;";
        var res = await connection.ExecuteAsync(sql, data);
        return res == 0 ? new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error") : new ApiResponse<bool>(res > 0);
    }

    public async Task<ApiResponse<bool>> Delete(int id)
    {
        using var connection = context.Connection();
        const string sql = "delete from customers where customerid = @Id";
        var res = await connection.ExecuteAsync(sql, new { Id = id });
        return res == 0 ? new ApiResponse<bool>(HttpStatusCode.NotFound, "Customer not found") : new ApiResponse<bool>(true);
    }

    
}