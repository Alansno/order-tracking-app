using Application;
using Application.Shared;
using Application.Shipping.Services;
using Hangfire;
using Hangfire.SqlServer;
using Infrastructure;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Presentation.WebApi;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Connection");
builder.Services.AddDbContext<OrderTrackingContext>(options => options.UseSqlServer(connectionString));

builder.Services
    .AddApplication()
    .AddRepositories();

builder.Services.AddSingleton<AssignShipmentJob>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
}).AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
}) ;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//hangfire connection
builder.Services.AddHangfire(
    config => config
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
        {
            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            QueuePollInterval = TimeSpan.Zero,
            UseRecommendedIsolationLevel = false,
            DisableGlobalLocks = false,
        })
);

builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHangfireDashboard();

//Execute job with Hangfire
ConfigureRecurringJobs(app);

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureRecurringJobs(WebApplication app)
{
    // Accedes al servicio RecurringJobService
    var recurringJobService = app.Services.GetRequiredService<AssignShipmentJob>();
    
    // Configurar el trabajo recurrente para ejecutarlo cada minuto
    RecurringJob.AddOrUpdate("AssignShipment",() => recurringJobService.ExecuteAssignShipmentAsync(), Cron.Minutely);
}