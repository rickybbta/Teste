# Projeto Tarefa API

Este projeto é uma API RESTful construída em **C#** com **.NET Core** e **Entity Framework Core**. O propósito do projeto é gerenciar usuários e suas tarefas associadas. Ele inclui funcionalidades de autenticação JWT, CRUD de usuários e CRUD de tarefas, com rotas protegidas para operações específicas de usuários autenticados.

## Dependências Necessárias

Para executar este projeto localmente, você precisará garantir que todas as dependências estejam instaladas. Siga os passos abaixo para configurar o ambiente corretamente.

### 1. Instalar o .NET SDK

Primeiro, você deve instalar o .NET SDK. Você pode baixá-lo diretamente do site oficial:

- [.NET SDK](https://dotnet.microsoft.com/download)

### 2. Instalando as Dependências do Projeto

Após clonar o repositório, navegue até a pasta do projeto `ProjetoTarefaApi` e execute o seguinte comando para restaurar as dependências:

```bash
dotnet restore
dotnet tool install --global dotnet-ef
```

# Nota importante Conexão com o Banco ! 
- Servidor: localhost
- Nome da Base de Dados: teste
- Usuário: root
- Senha: 123456
  
Certifique-se de que o MySQL esteja rodando localmente e que as credenciais acima estejam configuradas corretamente. Você pode alterar essas informações no arquivo de configuração appsettings.json dentro do diretório ProjetoTarefaAPI, caso utilize um ambiente diferente.
#

### 3. Pacotes Utilizados no Projeto
Abaixo estão os principais pacotes utilizados no projeto e suas funcionalidades:

Microsoft.EntityFrameworkCore - ORM usado para acessar o banco de dados.
Microsoft.EntityFrameworkCore.SqlServer - Provedor do Entity Framework Core para SQL Server.
Microsoft.EntityFrameworkCore.Tools - Ferramentas de CLI para o Entity Framework Core.
Microsoft.AspNetCore.Authentication.JwtBearer - Middleware de autenticação JWT para proteger rotas.
Swashbuckle.AspNetCore - Geração de documentação Swagger para a API.
### 4. Executar Migrations
Este projeto utiliza o Entity Framework Core para gerenciar o banco de dados. Para garantir que o banco de dados esteja atualizado com o último esquema, execute as migrações:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```
### 5. Executando a API
Para executar a API, use o seguinte comando na raiz do projeto:
```bash
dotnet run
```
A API estará disponível em http://localhost:5223.
##
### 6. Documentação Swagger
A documentação interativa da API pode ser acessada através do Swagger. Com a aplicação rodando, navegue até:
## http://localhost:5223/swagger/index.html

### Swagger UI
Rotas Disponíveis
Aqui estão as rotas disponíveis na API: 

### Usuário
- POST /api/usuario/cadastro - Cadastra um novo usuário.
- POST /api/usuario/login - Faz login de um usuário e retorna um token JWT.
- GET /api/usuario/{id} - Obtém informações de um usuário específico.
- PUT /api/usuario/{id} - Atualiza informações do usuário.
- DELETE /api/usuario/{id} - Remove um usuário.
### Tarefas
- POST /api/tarefa - Cria uma nova tarefa.
- GET /api/tarefa/usuario/{usuarioId} - Retorna todas as tarefas de um usuário.
- GET /api/tarefa/usuario/{usuarioId}/{id} - Retorna uma tarefa específica de um usuário.
- PUT /api/tarefa/usuario/{usuarioId}/{id} - Atualiza uma tarefa específica de um usuário.
- DELETE /api/tarefa/usuario/{usuarioId}/{id} - Deleta uma tarefa específica de um usuário.
### Ferramentas Utilizadas
- Entity Framework Core: Utilizado como ORM para gerenciar o banco de dados.
- JWT Authentication: Proteção das rotas com autenticação via tokens JWT.
- Swagger: Ferramenta para geração de documentação e teste das rotas da API.
- Conclusão
- Este projeto é uma API completa, gerenciamento de usuários e tarefas. Siga os passos descritos acima para configurar e executar o projeto localmente.
- <u>A autenticação JWT foi implementada porém por motivos de tempo ficara para futuras atualizações a autenticação para rotas após o login.<u>
  
## Autor

### Gabriel Guerra.