using HR_LeaveManagement.API.Middleware;
using HR_LeaveManagement.Application;
using HR_LeaveManagement.Identity;
using HR_LeaveManagement.Infrastructure;
using HR_LeaveManagement.Persistence;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((context, loggerConfig) => 
loggerConfig.WriteTo.Console()
.ReadFrom.Configuration(context.Configuration)
);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);


builder.Services.AddControllers();

builder.Services.AddCors(options =>
options.AddPolicy("all", builder => 
builder.AllowAnyOrigin()
.AllowAnyHeader()
.AllowAnyMethod())
);
builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseCors("all");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();