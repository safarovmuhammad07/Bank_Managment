using Domain.Entities;
using Infratructure.DataContext;
using Infratructure.Interfaces;
using Infratructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<DapperContext>();
builder.Services.AddScoped<IExtraCustomer, CustomerService>();
builder.Services.AddScoped<IExtraAccountService, AccountService>();
builder.Services.AddScoped<IGenericService<Branch>, BranchService>();
builder.Services.AddScoped<IGenericService<Loan>, LoanService>();
builder.Services.AddScoped<IExtraTransactionService, TransactionService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "BankManagment"));
}

app.MapControllers();
app.Run();

