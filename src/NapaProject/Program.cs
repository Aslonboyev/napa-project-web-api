using Microsoft.EntityFrameworkCore;
using NapaProject.AppDbContexts;
using NapaProject.Services.AutoMappers;
using NapaProject.Services.Extensions;
using NapaProject.Services.Helpers;
using NapaProject.Services.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//-> Database
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgressDb"));
});

//-> Services
builder.Services.AddServices();
builder.Services.AddSwaggerAuthorization();
builder.Services.AddJwtService(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(MappingProfile));

//Serilog
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Services.GetService<IHttpContextAccessor>() != null)
    HttpContextHelper.Accessor = app.Services.GetRequiredService<IHttpContextAccessor>();

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHadlerMiddleware>();

app.UseAuthentication();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
