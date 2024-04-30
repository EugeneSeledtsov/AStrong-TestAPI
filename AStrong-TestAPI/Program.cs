using AStrong_TestAPI.ConfigurationExtensions;
using DataAccess;
using DataAccess.Interfaces;
using Exceptions;
using Services.Implementations;
using Services.Interfaces;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAppDbContext(builder.Configuration);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("Handlers")));
builder.Services.AddScoped<IDataContext, DataContext>();
builder.Services.AddSingleton<IFileService>(t =>
{
    return new FileService(builder.Configuration.GetValue<string>("FileCorePath") ?? throw new ArgumentException("Couldn't found FileCorePath in appConfig file"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
