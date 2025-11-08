# 🧩 API CRUD Base — .NET 8  

![.NET](https://img.shields.io/badge/.NET-8.0-purple?logo=dotnet)
![License](https://img.shields.io/badge/license-MIT-green)
![Language](https://img.shields.io/badge/language-C%23-blue)
![Platform](https://img.shields.io/badge/platform-ASP.NET%20Core-lightgrey)

---

## 🧠 Sobre o Projeto

Este projeto é um **CRUD simples em .NET 8**, criado como **base de estudos para autenticação de APIs e mensageria**.  
A branch **`ApiCrud`** contém uma estrutura limpa e enxuta, com uma única controller (`ProductController`) e um modelo (`Product`), utilizando **armazenamento em memória** (sem banco de dados).

> 🔍 Ideal para testar conceitos de API, versionamento e preparar terreno para recursos mais avançados como autenticação, mensageria e integração em nuvem.

---

## 🚀 Objetivo

Fornecer uma base sólida para:

- 🔐 Implementar autenticação e autorização (JWT, Identity, Cognito)
- 💬 Integrar mensageria (Kafka, SQS, RabbitMQ)
- ☁️ Explorar deploys serverless (AWS Lambda)
- 🧱 Estudar boas práticas de arquitetura em APIs .NET

---

## 🧰 Tecnologias Utilizadas

| Categoria | Tecnologia |
|------------|-------------|
| Framework | .NET 8 |
| Linguagem | C# |
| API | ASP.NET Core Web API |
| Persistência | In-memory list (sem banco de dados) |

---

## 🏗️ Estrutura do Projeto

```bash
/src
├── ApiCrud
│ ├── Controllers
│ │ └── ProductController.cs
│ ├── Models
│ │ └── Product.cs
│ ├── Program.cs
│ └── appsettings.json
```

- **ProductController.cs** → expõe os endpoints CRUD (Get, Post, Put, Delete)  
- **Product.cs** → representa o modelo de produto com propriedades básicas (Id, Name, Price, etc.)  
- **Program.cs** → configura e executa a API  

---

## ⚙️ Como Executar o Projeto

### 📋 Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Git](https://git-scm.com/)

### 📦 Clonar o repositório
```bash
git clone <seu-repo-url>
cd <nome-do-repo>
git checkout ApiCrud
```
### ▶️ Executar a aplicação
```bash
dotnet run
```
A API será iniciada em:
👉 https://localhost:5001
ou
👉 http://localhost:5000
