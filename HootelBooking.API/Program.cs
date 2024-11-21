using Hangfire;
using HootelBooking.API;
using HootelBooking.API.Filters;
using HootelBooking.API.Middlewares;
using HootelBooking.Application.Models;
using HootelBooking.Persistence.Jobs;
using HootelBooking.Persistence.Services;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddHangfire(config =>
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHangfireServer();

// Configure API services, etc.
builder.Services.AddApiServices(builder.Configuration);

// Configure Hangfire recurring jobs
builder.Services.AddScoped<ExpiredCodesJob>();
builder.Services.AddScoped<ExpiredReservationsJob>();

builder.Services.ConfigureHangfireJobs();




SerilogConfiguration.ConfigureSerilog(builder.Configuration);
builder.Host.UseSerilog();



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();



app.UseMiddleware<GlobalErrorHandlingMiddleware>();
app.UseMiddleware<CustomAuthorizationMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.UseHangfireDashboard("/dashboard", new DashboardOptions
{
    Authorization = new[] { new HangfireAuthorizationFilter("Admin", "Owner") }
});

app.MapControllers();


try
{
    Log.Information("Starting the application\n");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "The application failed to start correctly\n");
}
finally
{
    Log.CloseAndFlush();  // Ensure Serilog flushes all logs and closes correctly
}
