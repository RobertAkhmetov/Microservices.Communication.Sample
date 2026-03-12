using ApplicationDependencyInjection = Microservices.Communication.Sample.Service1.Application.DependencyInjection;
using InfrastructureDependencyInjection = Microservices.Communication.Sample.Service1.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

ApplicationDependencyInjection.AddApplication(builder.Services);
InfrastructureDependencyInjection.AddInfrastructure(builder.Services, builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
