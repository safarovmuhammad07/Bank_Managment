using System.Net;
using Domain.Entities;
using Infratructure.Interfaces;
using Infratructure.Responses;
using Infratructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly IGenericService<Transaction> _transactionService;
    private readonly IExtraTransactionService transactionService;

    public TransactionController(IGenericService<Transaction> transactionService)
    {
        _transactionService = transactionService;
    }

    public TransactionController( IExtraTransactionService _transactionService)
    {
        transactionService = _transactionService;
    }

    [HttpGet]
    public async Task<ApiResponse<List<Transaction>>> GetAll()
    {
        return await _transactionService.GetAll();
    }

    [HttpGet("{id:int}")]
    public async Task<ApiResponse<Transaction>> GetById(int id)
    {
        return await _transactionService.GetById(id);
    }

    [HttpGet("{status}")]
    public async Task<ApiResponse<List<Transaction>>> GetByStatus(string status)
    {
        return await transactionService.GetByStatus(status);
    }

    [HttpPost]
    public async Task<ApiResponse<bool>> Add(Transaction transaction)
    {
        return await _transactionService.Add(transaction);
    }

    [HttpPut]
    public async Task<ApiResponse<bool>> Update(Transaction transaction)
    {
        return await _transactionService.Update(transaction);
    }

    [HttpDelete("{id:int}")]
    public async Task<ApiResponse<bool>> Delete(int id)
    {
        return await _transactionService.Delete(id);
    }
}