using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

namespace ProjetoTarefaApi.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Verifica se o usuário está autenticado e tem um token
            if (context.User.Identity.IsAuthenticated)
            {
                // Obtém o ID do usuário
                var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var email = context.User.FindFirst(ClaimTypes.Email)?.Value;
                var nome = context.User.FindFirst(ClaimTypes.Name)?.Value; // Supondo que você está adicionando "name" como claim
                var telefone = context.User.FindFirst(ClaimTypes.MobilePhone)?.Value; // Usando um tipo de claim padrão para telefone

                // Log no console
                Console.WriteLine($"Usuário Logado: ID = {userId}, Email = {email}, Nome = {nome}, Telefone = {telefone}");
            }

            await _next(context);
        }
    }
}
