using Microsoft.EntityFrameworkCore;
using ProjetoTarefaApi.Models; // Certifique-se de ajustar conforme seu namespace real

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar o DbContext para usar MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 25)))); // Ajuste a versão conforme necessário

// Adicionar suporte a controladores
builder.Services.AddControllers();

var app = builder.Build();

// Configurar o pipeline de solicitação HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Mapeia os controladores para suas rotas
app.MapControllers();

app.Run();
