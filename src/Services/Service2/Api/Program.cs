using Microservices.Communication.Sample.Service2.Application;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/service1/base", async ([FromBody] SendBaseMessageRequest request, CancellationToken token) =>
{
    try
    {
        //var result = await sender
    }
    catch (Exception)
    {
        throw;
    }
})
.WithName("Send message to service 1");

app.Run();
