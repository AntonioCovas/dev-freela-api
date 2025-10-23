# DevFreela.API 🚀

Projeto desenvolvido com o objetivo de aprimorar conhecimentos em **arquitetura limpa** e **boas práticas de desenvolvimento de APIs** com ASP.NET Core.

> ⚠️ Este projeto está em desenvolvimento e é utilizado para fins de estudo.  

---

## 📖 Sobre o Projeto

O **DevFreela** é uma API que simula um marketplace de freelancers e projetos.  
O projeto está sendo desenvolvido com intuito de colocar em prática e fixar conceitos importantes.

## ✅ Funcionalidades já implementadas

- CRUD de **projetos** e **usuários**
- Adição de **comentários** a projetos
- Controle de status do projeto (**Iniciar**, **Cancelar**, **Finalizar**)
- Listagem de **skills**
- Mapeamento de entidades com **Fluent API** do EF Core
- Uso de **CQRS** para separação de comandos e queries
- **MediatR** como mediador entre Controllers e Handlers, desacoplando a lógica de envio de comandos e queries da execução
- **Padrão repository** para abstrair acessos ao banco, centralizando consultas e persistência em classes dedicadas
- **Abstrações** e **injeção de dependência** para separar responsabilidades, facilitando **testes**, **manutenção** e **evolução do código**
- Validações com **FluentValidation**
- **Filter** para interceptar pipeline de execução, centralizar e padronizar retorno de erros, possibilitando limpar controllers

### 🔜 Próximos passos

- Autenticação e autorização com JWT
- Testes unitários com xUnit

---

## 🔧 Tecnologias Utilizadas

- [ASP.NET Core (.NET 9)](https://dotnet.microsoft.com/)
- [SQL Server](https://www.microsoft.com/sql-server)
- [Entity Framework Core](https://learn.microsoft.com/ef/core/)
- [Dapper](https://github.com/DapperLib/Dapper)
- [MediatR](https://github.com/jbogard/MediatR)
- [Swagger / OpenAPI](https://swagger.io/)

---

## 🏗️ Arquitetura

O projeto segue a **Clean Architecture** com as seguintes camadas:

- **DevFreela.API** → Interface/Apresentação (Program, Controllers)
- **DevFreela.Application** → Aplicação/Casos de uso (CQRS, DTOs)
- **DevFreela.Core** → Domínio (Entities, Enums, Exceptions, Interfaces)
- **DevFreela.Infrastructure** → Infraestrutura (Entity mapping, Repositories, Migrations)

---

## 📌 Endpoints principais

### **Projects**
- `GET /api/projects` → Lista todos os projetos  
- `POST /api/projects` → Cria um novo projeto  
- `GET /api/projects/{id}` → Busca projeto por ID  
- `PUT /api/projects/{id}` → Atualiza projeto  
- `DELETE /api/projects/{id}` → Remove projeto  
- `POST /api/projects/{id}/comments` → Adiciona comentário  
- `PUT /api/projects/{id}/cancel` → Cancela projeto  
- `PUT /api/projects/{id}/start` → Inicia projeto  
- `PUT /api/projects/{id}/finish` → Finaliza projeto  

### **Users**
- `POST /api/users` → Cria usuário  
- `GET /api/users/{id}` → Busca usuário por ID  
- `PUT /api/users/{id}` → Atualiza usuário  
- `DELETE /api/users/{id}` → Remove usuário  

### **Skills**
- `GET /api/skills` → Lista skills disponíveis  

---

## ▶️ Como rodar o projeto

1. Clone o repositório:

```bash
git clone https://github.com/AntonioCovas/DevFreela.API.git
```

2. Configure a string de conexão em ambiente de desenvolvimento usando dotnet user-secrets no diretório do .csproj (DevFreela.API):

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "YOUR_CONNECTION_STRING"
```

3. Restaure pacotes e execute a aplicação:

```bash
dotnet restore
dotnet run --project DevFreela.API
```

## 📝 Observações

Este repositório é ativo e está em evolução. Novas funcionalidades serão adicionadas aos poucos, incluindo validações, autenticação e autorização, mensageria e testes.
O histórico de commits reflete a evolução do projeto, funcionando também como portfólio de estudo e aprendizado.