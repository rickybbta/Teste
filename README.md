# Projeto TodoList

Este repositório contém um sistema de gerenciamento de tarefas com funcionalidades de autenticação, cadastro e gerenciamento de tarefas de usuários. O projeto é composto por três partes principais: o **front-end** em Angular, a **API RESTful** em .NET Core e um **exercício em SQL**. Abaixo está uma breve descrição de cada diretório.

## Estrutura do Repositório

### 1. **ProjetoTarefaWeb**
Este diretório contém o código do front-end da aplicação, desenvolvido em Angular 14. Aqui estão as funcionalidades principais:

- **Login e Cadastro de Usuários**: O usuário pode se cadastrar, logar e gerenciar suas informações.
- **Gerenciamento de Tarefas**: Criação, edição e remoção de tarefas.
- **Atualização de Perfil**: Permite que o usuário edite seus dados pessoais, como nome, e-mail e senha.
  
Para mais detalhes, consulte o arquivo [README.md](ProjetoTarefaWeb/README.md) no diretório `ProjetoTarefaWeb`.

### 2. **ProjetoTarefaAPI**
Aqui está o código do back-end da aplicação, desenvolvido em .NET Core com Entity Framework. A API é responsável por gerenciar os usuários e suas tarefas, além de autenticar as requisições via JWT.

Principais funcionalidades:

- **Autenticação JWT**: Rotas protegidas para operações de usuários autenticados.
- **CRUD de Usuários e Tarefas**: Operações de criação, leitura, atualização e exclusão de usuários e suas respectivas tarefas.

Para mais detalhes sobre a API e as rotas disponíveis, veja o arquivo [README.md](ProjetoTarefaAPI/README.md) no diretório `ProjetoTarefaAPI`.

### 3. **SQL**
Este diretório contém um exercício SQL proposto, relacionado ao banco de dados da aplicação. Nele você encontrará:

- **Scripts SQL**: Scripts para criação de tabelas, inserção de dados e consultas.
- **Exercícios Práticos**: Questões de manipulação de dados, incluindo seleções, junções, agregações e outros comandos SQL relevantes para o contexto de gerenciamento de tarefas.

## Como Rodar o Projeto

1. **ProjetoTarefaWeb**: Siga as instruções no [README.md](ProjetoTarefaWeb/README.md) do diretório para instalar as dependências e rodar a aplicação Angular.
   
2. **ProjetoTarefaAPI**: Consulte o [README.md](ProjetoTarefaAPI/README.md) para configurar a API e rodá-la localmente utilizando o .NET Core.


## Autor

### Gabriel Guerra.
