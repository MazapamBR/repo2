// JaveragesLibrary/Program.cs

using JaveragesLibrary.Services.Features.Figuras; // Nuevo using para el servicio de Figuras

var builder = WebApplication.CreateBuilder(args);

// REGISTRO DEL SERVICIO DE FIGURAS
// Usamos Singleton para mantener los datos en memoria durante toda la ejecución
builder.Services.AddSingleton<FiguraService>();

builder.Services.AddControllers();

// Swagger/OpenAPI para documentación automática
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "GeoFormulas API",
        Description = "Una API para calcular área, perímetro y volumen de figuras geométricas",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Equipo GeoFormulas",
            Url = new Uri("https://tuwebsite.com")
        }
    });
});

var app = builder.Build();

// Middleware para desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "GeoFormulas API V1");
        options.RoutePrefix = string.Empty; // Swagger en la raíz
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers(); // Habilita los endpoints

app.Run();
