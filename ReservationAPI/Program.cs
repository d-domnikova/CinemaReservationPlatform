using ReservationServiceBLL.Services;
using ReservationServiceBLL.Interfaces;
using Microsoft.OpenApi.Models;
using ReservationAPI;
using ReservationServiceBLL.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ReservationService API",
        Description = "EntityFramework part of Asp.Net project"
    });
});

builder.Services.AddServices(builder.Configuration);

builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<ITicketTypeService, TicketTypeService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint(
                  "/swagger/v1/swagger.json",
                  "ReservationAPI v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
