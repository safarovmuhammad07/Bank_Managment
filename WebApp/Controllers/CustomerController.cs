using Domain.Entities;
using Infratructure.Interfaces;
using Infratructure.Responses;
using Infratructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController: ControllerBase
{
    
    
    private readonly IExtraCustomer _customerService;
    private readonly IGenericService<Customer> customerService;

    public CustomerController(IGenericService<Customer> _customerService)
    {
        customerService = _customerService;
    }

    public CustomerController(IExtraCustomer customerService)
    {
        _customerService = customerService;
    }
    
    
    [HttpGet]
    public ApiResponse<List<Customer>> GetAll()
    {
        return customerService.GetAll();
    }
    [HttpGet("{id:int}")]
    public ApiResponse<Customer> GetById(int id)
    {
        return customerService.GetById(id);
    }

    [HttpGet("{customerId}")]
    public ApiResponse<int> GetCustomerCount()
    {
        return _customerService.GetCustomerCount();
    }
    
    [HttpPost]
    public ApiResponse<bool> Add(Customer customer)
    {
        return customerService.Add(customer);
    }
    [HttpPut]
    public ApiResponse<bool> Update(Customer customer)
    {
        return customerService.Update(customer);
    }
    [HttpDelete]
    public ApiResponse<bool> Delete(int id)
    {
        return customerService.Delete(id);
    }
}