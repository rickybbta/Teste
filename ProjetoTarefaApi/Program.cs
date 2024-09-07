using Microsoft.EntityFrameworkCore;
using ProjetoTarefaApi.Data; // Ajuste o namespace conforme necessário
using ProjetoTarefaApi.Configurations; // Adicione este namespace se for necessário para JwtSettings
using ProjetoTarefaApi.Services; // Adicione este namespace se for necessário para TokenService

var builder = WebApplication.CreateBuilder(args);

// Configurar o DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), 
        new MySqlServerVersion(new Version(8, 0, 25)))); // Ajuste a versão do MySQL conforme necessário

// Configurar JWT settings
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddSingleton<TokenService>();

// Add services to the container.
builder.Services.AddControllers();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Adiciona log de inicialização
var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("API iniciada com sucesso!");
logger.LogInformation("Caminho da documentação Swagger: http://localhost:5223/swagger/index.html");

app.Run();
