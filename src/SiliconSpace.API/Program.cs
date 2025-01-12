using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;
using SiliconSpace.API.Extensions;
using SiliconSpace.API.MidlleWares;
using SiliconSpace.Data.DbContexts;
using SiliconSpace.Service.Helpers;
using SiliconSpace.Shared.Extensions;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
     .AddNewtonsoftJson(options =>
     {
         options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
     });
builder.Services.AddHttpContextAccessor();
builder.Services.AddJwtService(builder.Configuration);
builder.Services.AddSwaggerService();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddCustomService();
builder.Services.AddRateLimiterService(builder.Configuration);
WebEnvironmentHost.WebRootPath = Path.GetFullPath("wwwroot");

builder.Services.Configure<FormOptions>(options =>
{
    options.ValueLengthLimit = int.MaxValue;
    options.MultipartBodyLengthLimit = long.MaxValue; // In case of multipart
    options.MultipartHeadersLengthLimit = int.MaxValue; // Set the multipart headers length limit
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = null; // Unlimited request body size
});

// Logger

var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(option
=> option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.InitAccessor();
app.UseIpRateLimiting();
app.UseMiddleware<ExceptionHandlerMiddleWare>();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
