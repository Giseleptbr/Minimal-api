# Minimal API com .NET 9, EF Core e SQLite

Este projeto foi desenvolvido como parte do **Desafio DIO** para construir uma **Minimal API** utilizando:

- .NET 9
- Entity Framework Core com SQLite
- Swagger para documentação
- Injeção de dependências e arquitetura em camadas

---

## Sobre o projeto

Este projeto consiste em uma **Minimal API** desenvolvida em **.NET 9** como parte do desafio da **DIO**.  
O objetivo é **simular um sistema de gestão de administradores e veículos**, demonstrando:

- **Boas práticas de desenvolvimento de APIs**
- **Estrutura em camadas** (Domínio, Infraestrutura e API)
- **Integração com banco de dados** via **Entity Framework Core**
- **Documentação interativa com Swagger**

O projeto permite **realizar operações CRUD** para **Administradores** e **Veículos**, além de fornecer **um endpoint de login** para validação de credenciais.

### Funcionalidades

- **Cadastro e gerenciamento de administradores**
  - Criar, listar, atualizar e remover administradores
  - Login com validação de e-mail e senha
- **Cadastro e gerenciamento de veículos**
  - Criar, listar, atualizar e remover veículos
- **Retorno em JSON**
  - API pronta para consumo por qualquer cliente HTTP
- **Swagger integrado**
  - Documentação e testes diretamente pelo navegador

### 💡 O que o projeto busca demonstrar

Este projeto demonstra como **criar uma API enxuta e escalável**, aplicando conceitos como:

- **Minimal APIs** para endpoints simples e performáticos
- **Injeção de dependência** para separar responsabilidades
- **Persistência de dados** com SQLite, incluindo Migrations do EF Core
- **Padrões de organização de projeto** que facilitam manutenção e evolução

---

## Tecnologias utilizadas

- **.NET 9** (Minimal API)
- **Entity Framework Core** (Code First)
- **SQLite** (banco de dados local)
- **Swagger / Swashbuckle** (documentação interativa)

## Prints de Funcionamento

### 1️⃣ Teste de Login via Postman
![Postman Test](postman%20post.png)

### 2️⃣ Endpoints Documentados no Swagger
![Swagger Minimal API](Swagger%20Minimal%20API.png)