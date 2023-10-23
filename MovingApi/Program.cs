using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MovingApi.Data;
using MovingApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var Connectionstring = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(Connectionstring));
builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddTransient<IGenresServices, GenresServices>();
builder.Services.AddTransient<IMovieServices, MovieServices>();

builder.Services.AddAutoMapper(typeof(Program));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title= "TestApi",

    });
    
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(
    c=>c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()
    );
app.UseAuthorization();

app.MapControllers();

app.Run();
