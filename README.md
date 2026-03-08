 ACT Hotelaria
Gestão de agendamentos e consumos no setor hoteleiro, com foco em arquiteturas modernas e escalabilidade. Este projeto implementa funcionalidades principais de reservas e controle de consumos, utilizando ferramentas como MediatR (CQRS), EF Core, Docker, e aplicando padrões como DDD e Clean Architecture.
---
## 🚀 Tecnologias Utilizadas
- **Linguagem e Framework**: .NET 8.
- **Banco de Dados**: SQL Server (ORM: Entity Framework Core).
- **Cache**: Redis.
- **Containerização**: Docker e Docker Compose.
- **Padrões de Arquitetura**:
  - **CQRS**: Separação entre comandos (escrita) e consultas (leitura).
  - **DDD**: Design alinhado ao domínio.
  - **Clean Architecture**: Manutenção fácil e separação clara de camadas.
---
## 🛠️ Pré-requisitos
Certifique-se de possuir os seguintes componentes instalados na sua máquina:
1. [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
2. [Docker e Docker Compose](https://docs.docker.com/get-docker/)
3. Ambiente compatível com SQL Server ou acesso a um servidor configurado.
---
## 🏗️ Instalação e Execução
Siga os passos abaixo para executar o projeto localmente:
1. **Clone o repositório**
   ```bash
   git clone https://github.com/seu-usuario/ACT-Hotelaria.git
   cd ACT-Hotelaria
2. Configure as variáveis de ambiente
   Certifique-se de editar o arquivo appsettings.json ou docker-compose.override.yml caso haja necessidade de personalização.
3. Suba os serviços usando Docker Compose
      docker-compose up -d
   
4. Execute as migrações
   Certifique-se de aplicar as migrações utilizando o dotnet ef:
      dotnet ef database update
   
5. Acesse o sistema em http://localhost:5000 (http://localhost:5000).
---
<img width="398" height="371" alt="image" src="https://github.com/user-attachments/assets/0f4ddc49-bd96-46cc-9956-e9eaed9200b6" />
