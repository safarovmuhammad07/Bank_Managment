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
    public ApiResponse<List<Transaction>> GetAll()
    {
        return _transactionService.GetAll();
    }

    [HttpGet("{id:int}")]
    public ApiResponse<Transaction> GetById(int id)
    {
        return _transactionService.GetById(id);
    }

    [HttpGet("{status}")]
    public ApiResponse<List<Transaction>> GetByStatus(string status)
    {
        return transactionService.GetByStatus(status);
    }

    [HttpPost]
    public ApiResponse<bool> Add(Transaction transaction)
    {
        return _transactionService.Add(transaction);
    }

    [HttpPut]
    public ApiResponse<bool> Update(Transaction transaction)
    {
        return _transactionService.Update(transaction);
    }

    [HttpDelete("{id:int}")]
    public ApiResponse<bool> Delete(int id)
    {
        return _transactionService.Delete(id);
    }
}