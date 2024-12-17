//using DP424.Application.Repo.Abstract;
using DP424.Application.Repo.Implementation;
using DP424.Application.Services;

using DP424.Infrastructure;
using DP424.Web.Command;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var constr = builder.Configuration.GetConnectionString("constr");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(op => op.UseMySql(constr, ServerVersion.AutoDetect(constr)));

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<CommandHandler>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
