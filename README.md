CooperativaApp

Este repositório contém o código-fonte para o CooperativaApp, uma aplicação web para gerenciamento de informações de cooperativas e seus cooperados. 
O sistema permite o cadastro, atualização e consulta de dados de cooperativas, cooperados e seus contatos favoritos, incluindo informações de chave PIX.

Funcionalidades:

Gerenciamento de Cooperativas: Cadastro, atualização, exclusão e consulta de informações de cooperativas, incluindo nome e status de atividade.

Gerenciamento de Cooperados: Cadastro, atualização, exclusão e consulta de informações de cooperados, como nome completo, dados bancários e relação com uma cooperativa específica. Busca por nome e número de conta.

Contatos Favoritos: Cadastro, atualização e exclusão de contatos favoritos para cada cooperado, incluindo nome do contato e chave PIX (CPF, CNPJ, email, telefone ou aleatória).

Autenticação: Utiliza um sistema de autenticação para controlar o acesso às funcionalidades do sistema (implementação específica a ser detalhada).

API REST: A aplicação expõe uma API RESTful para facilitar a integração com outros sistemas.

Tecnologias Utilizadas:

Backend: ASP.NET Core (.NET 8), Entity Framework Core, SQL Server, Docker

Banco de dados: 

SQL Server

Testes:

xUnit, FluentAssertions, Microsoft.AspNetCore.Mvc.Testing, Bogus

Outras Bibliotecas: [Serilog,AutoMapper,DataAnnotation]

Arquitetura:

A aplicação segue uma arquitetura em camadas, separando a lógica de negócio (Domain), acesso a dados (Infraestrutura), aplicação (Application) e apresentação (Presentation - API).

Principios:

Domain Driven Desing (DDD) - Clean Architecture - SOLID

Instalação e Execução:

Clone o repositório:
git clone [(https://github.com/MarcoAntonioMoraDev/Axis)]

Navegue: cd [Axis/CooperativaApp]
Execute: CooperativaApp.sln

Inicialize o DockerDesktop

Execute a Migration do Banco de Dados

Navegue: cd [Axis/CooperativaApp]

Abra o Terminal Power Shell, execute: dockercompose up -d

no Visual Studio inicialize a conexão com o Banco de Dados : ID=sa; Password=Axis2025!

Execute a API

Para geração de Token em Usuarios, endpoint POST api/usuarios/login

email: admin@cooperativa.com

senha: SenhaSegura123

Autorize o uso da API clicando no botão Authorization e cole o Token gerado.
