using SeCumple.API.Middlewares;
using SeCumple.Application;
using SeCumple.CrossCutting.Options;
using SeCumple.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<SettingOptions>(builder.Configuration.GetSection("Settings"));

// Configura CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.AllowAnyOrigin() // Temporalmente permite todos los orígenes para depurar
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Agrega servicios al contenedor
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationDependencies();
builder.Services.AddInfrastructureDependencies(builder.Configuration);

var app = builder.Build();

app.Use(async (context, next) =>
{
    Console.WriteLine($"Solicitud entrante: Método: {context.Request.Method}, Ruta: {context.Request.Path}");
    await next.Invoke();
});

app.UseCors("AllowSpecificOrigins");

app.Use(async (context, next) =>
{
    if (context.Request.Method == HttpMethods.Options)
    {
        context.Response.StatusCode = StatusCodes.Status204NoContent;
        return;
    }
    await next();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
