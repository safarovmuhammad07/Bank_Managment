using System.Net;
using Domain.Entities;
using Infratructure.Interfaces;
using Infratructure.Responses;
using Infratructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoanController : ControllerBase
{
    private readonly IGenericService<Loan> _loanService;

    public LoanController(IGenericService<Loan> loanService)
    {
        _loanService = loanService;
    }

    [HttpGet]
    public ApiResponse<List<Loan>> GetAll()
    {
        return _loanService.GetAll();
    }

    [HttpGet("{id:int}")]
    public ApiResponse<Loan> GetById(int id)
    {
        return _loanService.GetById(id);
    }

    [HttpPost]
    public ApiResponse<bool> Add(Loan loan)
    {
        return _loanService.Add(loan);
    }

    [HttpPut]
    public ApiResponse<bool> Update(Loan loan)
    {
        return _loanService.Update(loan);
    }

    [HttpDelete("{id:int}")]
    public ApiResponse<bool> Delete(int id)
    {
        return _loanService.Delete(id);
    }
}