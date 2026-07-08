using System.Text.Json.Serialization;
using Hangfire;
using Hangfire.PostgreSql;
using SecretManager.API.Middleware;
using SecretManager.API.Services;
using SecretManager.Application;
using SecretManager.Application.Common.Interfaces;
using SecretManager.Infrastructure;
using SecretManager.Infrastructure.Jobs;

DotNetEnv.Env.Load("../../.env");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<ILoggedInUserService, CurrentUserService>();

builder.Services.AddHangfire(config => config
    .UsePostgreSqlStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHangfireServer();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHangfireDashboard("/hangfire");
RecurringJob.AddOrUpdate<AuditLogCleanupJob>(
    "cleanup-old-audit-logs",
    job => job.Execute(),
    Cron.Daily);


app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/health", () => Results.Ok("Healthy"));

app.MapControllers();

app.Run();