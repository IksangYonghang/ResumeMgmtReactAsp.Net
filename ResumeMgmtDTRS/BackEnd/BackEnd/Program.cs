using BackEnd.Core.AutoMapperConfig;
using BackEnd.Core.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//DB Configuration
builder.Services.AddDbContext<ApplicationDbContext>(options => 
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")
));

//AutoMapper Configuration
builder.Services.AddAutoMapper(typeof(AutoMapperConfigProfile));

// Add services to the container.
//Converting enums to string format
builder.Services
	.AddControllers()
	.AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
	});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}


//Configuring Cross-Origin Resource Sharing (CORS) policies for the application.

app.UseCors(options =>
{
	options
	.AllowAnyOrigin()
	.AllowAnyMethod()
	.AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
