# API de Gerenciamento de Tarefas

API REST desenvolvida em **ASP.NET Core** para gerenciamento de tarefas com autenticação JWT e testes automatizados.

## Tecnologias

* ASP.NET Core (.NET 8)
* Entity Framework Core
* JWT Authentication
* xUnit
* Moq
* FluentAssertions
* Coverlet (Code Coverage)

## Funcionalidades

* Registro de usuário
* Login com autenticação JWT
* CRUD de tarefas
* Proteção de rotas com autenticação
* Testes unitários e de integração

## Arquitetura

O projeto segue uma estrutura em camadas:

```
Controllers
Services
Repositories
Models
DTOs
```

Essa separação permite:

* melhor testabilidade
* desacoplamento entre camadas
* manutenção mais simples

## Como executar o projeto

1. Clonar o repositório

```
git clone https://github.com/NathanWMartins/ControleTarefas
```

2. Restaurar dependências

```
dotnet restore
```

3. Executar a API

```
dotnet run
```
## 🔐 Autenticação

A autenticação é feita utilizando **JWT (JSON Web Token)**.

Fluxo:

1. Registrar usuário
2. Fazer login
3. Receber token
4. Enviar token no header:

```
Authorization: Bearer SEU_TOKEN
```

## 🧪 Executar testes

Para rodar todos os testes:

```
dotnet test
```

## 📊 Cobertura de testes

Gerar cobertura:

```
dotnet test --collect:"XPlat Code Coverage"
```

Gerar relatório visual:

```
reportgenerator -reports:TestResults/**/coverage.cobertura.xml -targetdir:coverage
```

Depois abra:

```
coverage/index.html
```

## 📌 Estrutura do projeto

```
ApiEstudo
│
├── TarefasApi
│   ├── Controllers
│   ├── Services
│   ├── Repositories
│   ├── Models
│   ├── DTOs
│   └── TarefasApi.Tests
│       ├── Controllers
│       └── Services
│   
```

## Objetivo do projeto

Este projeto foi desenvolvido para estudo de:

* arquitetura de APIs em .NET
* autenticação com JWT
* boas práticas de testes
* organização de código em camadas

## Autor

Desenvolvido por **Nathan Will Martins**
