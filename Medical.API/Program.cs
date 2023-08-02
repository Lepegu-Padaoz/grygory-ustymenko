using Medical.API;
using Medical.API.Extensions;
using Medical.API.Middlewares;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Medical API", Version = "v1" });
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// registration of own services for DAL/BL layers
builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddBusinessLayer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();