
# Projeto TodoList Web

Este é um projeto front-end da aplicação TodoList construído em Angular 14. A aplicação consome uma API RESTful para gerenciar usuários e tarefas. O objetivo da aplicação é permitir que usuários se cadastrem, façam login, gerenciem suas tarefas (adicionar, editar e remover), além de permitir a edição dos dados pessoais dos usuários.

## Funcionalidades

- **Login e Cadastro de Usuários**: O usuário pode se cadastrar e logar na aplicação.
- **Gestão de Tarefas**: Adição, remoção e atualização de tarefas.
- **Atualização de Perfil**: O usuário pode alterar suas informações pessoais, como nome, e-mail e senha.
- **Consumo da API**: A aplicação consome uma API externa para gerenciar dados de usuários e tarefas. As rotas consumidas estão descritas abaixo.

## Requisitos

Antes de rodar o projeto, você precisará ter instalado:

- [Node.js](https://nodejs.org) (versão 16.x ou superior)
- [Angular CLI](https://angular.io/cli) (versão 14.x)

### Verificando as versões instaladas

Para verificar se o `Node.js` e o `Angular CLI` estão instalados corretamente, você pode usar os seguintes comandos no terminal:

```bash
node -v
# Deve retornar a versão 16.x ou superior

npm -v
# Deve retornar a versão do npm que acompanha o Node.js

ng version
# Deve retornar a versão do Angular CLI (14.x.x)
```

## Instalação

### 1. Instalando Node.js

Você pode baixar o Node.js diretamente do [site oficial](https://nodejs.org/). Certifique-se de instalar a versão LTS.

Após a instalação, abra o terminal e verifique se o Node.js foi instalado corretamente com o comando:

```bash
node -v
```

### 2. Instalando o Angular CLI

Depois de ter o Node.js instalado, você pode instalar o Angular CLI globalmente usando o npm:

```bash
npm install -g @angular/cli@14.2.13

npm install @types/node@latest
```

Verifique a instalação do Angular CLI com o comando:

```bash
ng version
```


### 3. Instalando dependências

Entre no diretório do projeto e execute o comando para instalar todas as dependências do projeto:

```bash
cd ProjetoTarefaWeb
npm install
```

Isso irá instalar todas as dependências listadas no arquivo `package.json`.

## Como rodar o projeto

Após instalar todas as dependências, você pode rodar o projeto localmente com o comando:

```bash
ng serve
```

A aplicação estará disponível no endereço:

```bash
http://localhost:4200
```

### Alterando a porta

Se desejar rodar o projeto em uma porta diferente, você pode usar o seguinte comando:

```bash
ng serve --port 4300
```

Isso fará com que a aplicação rode na porta `4300`.

## Descrição da Aplicação

A aplicação web é um gerenciador de tarefas. O usuário pode se cadastrar, fazer login e gerenciar suas tarefas de forma prática. As funcionalidades principais são:

- **Cadastro de Usuário**: Permite que novos usuários se cadastrem com nome, e-mail, telefone e senha.
- **Login**: Permite que os usuários façam login com e-mail e senha.
- **Gerenciamento de Tarefas**: O usuário pode criar novas tarefas, editar ou marcar como concluídas, além de remover tarefas da lista.
- **Atualização de Dados Pessoais**: O usuário pode atualizar seu nome, e-mail, telefone e senha.

## Estrutura do Projeto

Aqui está a estrutura básica do projeto:

```
/src
  /app
    /login              # Componente para tela de login e cadastro
    /todo-list          # Componente para lista de tarefas
    /edit-user          # Componente para edição de dados do usuário
  /assets               # Arquivos estáticos (imagens, ícones, etc.)
  /environments         # Arquivos de configuração de ambiente
```

## Autor

### Gabriel Guerra.