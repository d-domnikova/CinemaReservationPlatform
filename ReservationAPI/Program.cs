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
        Title = "Reservation API",
        Description = "EntityFramework part of Asp.Net project"
    });
});

builder.Services.AddServices(builder.Configuration);

builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<ITicketTypeService, TicketTypeService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(opt => opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Reservation API v1"));

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
