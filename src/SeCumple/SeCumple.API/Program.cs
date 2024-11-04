using SeCumple.API.Middlewares;
using SeCumple.Application;
using SeCumple.CrossCutting.Options;
using SeCumple.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularLocalhost",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Cambia el puerto si es diferente
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});


// Add services to the container.
builder.Services.Configure<SettingOptions>(builder.Configuration.GetSection("Settings"));

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers()
    ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationDependencies();
builder.Services.AddInfrastructureDependencies(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
// }

app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection();

app.UseCors("AllowAngularLocalhost");

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();