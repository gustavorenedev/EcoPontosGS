# EcoPontos uma maneira de economizar a energia e deixa-lá mais sustentável

## Integrantes do Grupo
- RM551288 Gustavo René Dias Boamorte
- RM98442 João Costa Feitosa
- RM99495 Igor Miguel Silva
- RM98093 Pedro Felipe Barros
- RM551732 Kauê Matheus Santana

## Arquitetura da API

### Escolha da Arquitetura

Para este projeto, optamos por uma **arquitetura monolítica**. As razões para essa escolha são:

1. **Simplicidade e Coesão**: Uma abordagem monolítica permite um desenvolvimento mais coeso e simplificado, onde todos os componentes são implementados em uma única aplicação. Isso facilita o gerenciamento e a comunicação entre os componentes.

2. **Facilidade de Implementação**: Como a aplicação é pequena e a equipe é relativamente pequena, uma arquitetura monolítica reduz a complexidade inicial do projeto e permite um desenvolvimento mais rápido.

3. **Menor Sobrecarga Operacional**: Em um ambiente de produção, uma aplicação monolítica pode ser mais fácil de configurar e gerenciar, especialmente se não houver necessidade de escalabilidade horizontal complexa.

Este projeto é uma API para gerenciamento de tarefas, usuários e recompensas. Foi desenvolvido com foco em simplicidade e produtividade, utilizando uma **arquitetura em 3 camadas** para reduzir o tempo de implementação, dada a natureza curta do projeto.

### Princípios SOLID
- Single Responsibility Principle (SRP): Cada classe e método tem uma única responsabilidade. Isso é aplicado às camadas Controller, Service e Repository, separando a lógica de negócios e a persistência de dados.
- Dependency Injection (DI): Implementado para desacoplar dependências e facilitar testes. Utilizamos a injeção de dependências no Program.cs para gerenciar instâncias de serviços e repositórios.
- Interface Segregation Principle (ISP): As interfaces foram projetadas de forma coesa, evitando métodos desnecessários.

### Testes Unitários
Os testes unitários foram desenvolvidos para validar a lógica dos métodos principais nas camadas de Service e Repository. Utilizamos xUnit como framework de testes, assegurando que cada funcionalidade isolada opere conforme o esperado

- Validação de CRUD: Testes para criação, leitura, atualização e exclusão dos recursos de Usuário, Produto e Carrinho.
- Validação de cenário para os métodos da service.

#### Estrutura do Projeto
- **Controllers**: Responsáveis por gerenciar as requisições HTTP e retornar respostas adequadas.
- **Services**: Contêm a lógica de negócios e regras de aplicação.
- **Repositories**: Gerenciam a persistência de dados e a comunicação com o banco de dados.
- **DTOs**: Objetos de Transferência de Dados utilizados para enviar e receber dados entre o cliente e o servidor.

## 🚀 Tecnologias Utilizadas

- **ASP.NET Core 8.0**: Framework principal para construção da API.
- **Entity Framework Core**: ORM para acesso ao banco de dados.
- **Injeção de Dependência (DI)**: Para gerenciamento de serviços.
- **Swagger**: Para documentação automática da API.

---

## 📥 Como Clonar e Executar o Projeto

### Pré-requisitos

- .NET 8.0 ou superior instalado.
- Banco de dados configurado Oracle SQL.

### Passos

1. Clone o repositório:
   ```bash
   git clone https://github.com/gustavorenedev/EcoPontosGS.git
   ```
2. Navegue até a pasta do projeto:
   ```bash
   cd EcoPontosGS
   ```
3. Restaure as dependências:
   ```bash
   dotnet restore
   ```
4. Configure a conexão com o banco de dados no arquivo appsettings.json.

5. Execute as migrações do banco de dados:
   ```bash
   dotnet ef database update
   ```
6. Inicie o servidor:
   ```bash
   dotnet run
   ```
7. Acesse a documentação Swagger em http://localhost:5000/swagger.

## 📚 Endpoints da API

### Usuários (Users)
- **POST /api/User/loginUser**
  - **Descrição**: Faz o login de um usuário e retorna uma mensagem de sucesso.
  - **Corpo da Requisição**:
    ```json
    {
      "email": "john.doe@example.com",
      "password": "securepassword"
    }
    ```

- **POST /api/User/registerUser**
  - **Descrição**: Registra um novo usuário no sistema.
  - **Corpo da Requisição**:
    ```json
    {
      "name": "teste",
      "email": "teste@gmail.com",
      "password": "teste@123",
      "confirmPassword": "teste@123"
    }
    ```

### Tarefas (Task Work)

- **POST /api/TaskWork/CreateTaskWork**
  - **Descrição**: Registra um novo tarefa no sistema.
  - **Corpo da Requisição**:
    ```json
    {
      "type": "Placa Solar",
      "description": "Fazer utilização da placa solar para economia da energia por 1 hora"
    }
    ```

- **GET /api/TaskWork/GetTaskWorkAll**
  - **Descrição**: Retorna uma resposta Ok com uma lista de todas as tarefas.

- **GET /api/TaskWork/GetTaskById/{id}**
  - **Descrição**: Obtém uma tarefa específica pelo ID.
  - **Parâmetros**: `id` - Identificador da tarefa.

- **DELETE api/TaskWork/DeleteTaskWork/{id}**
  - **Descrição**: Exclui uma tarefa pelo ID.

### Registro de Tarefas (Task Register)

- **POST /api/TaskRegister/AssignTaskToUser?userId=&taskId=**
  - **Descrição**: Associa uma tarefa a um usuário.
  - **Parametros**:
      - userId: Id do usuário a ser associado a tarefa.
      - taskId: Id da tarefa a ser associada.

- **POST /api/TaskRegister/UnassignTaskFromUser?userId=&taskId=**
  - **Descrição**: Desassocia uma tarefa do usuário.
  - **Parametros**:
      - userId: Id do usuário a ser desassociado da tarefa.
      - taskId: Id da tarefa a ser desassociada.

- **GET /api/TaskRegister/GetTasksByUser/{userId}**
  - **Descrição**: Obtém todas as tarefas associadas a um usuário especificado.
  - **Parâmetros**: `userId` - Identificador da tarefa.

- **POST /api/TaskRegister/CompleteTask**
  - **Descrição**: Conclui uma tarefa para um usuário com uma duração especificada em segundos.
  - **Parametros**:
      - userId: Id do usuário a concluir a tarefa.
      - taskId: Id da tarefa a ser concluída.
      - duration: Total em segundos do tempo até a conclusão da tarefa.

### Recompensas (Reward)

- **POST /api/Reward/CreateReward**
  - **Descrição**: Cria uma nova recompensa.
  - **Corpo da Requisição**:
    ```json
    {
      "name": "Desconto de 5% na conta de luz",
      "pointsRequired": 1000
    }

- **GET /api/Reward/GetAllRewards**  
  - **Descrição**: Obtém todas as recompensas cadastradas.

- **GET /api/Reward/GetRewardById/{id}**  
  - **Descrição**: Obtém os detalhes de uma recompensa específica pelo ID.  
  - **Parâmetros**: `id` - Identificador da recompensa. 

- **PUT /api/Reward/UpdateReward/{id}**  
  - **Descrição**: Atualiza os detalhes de uma recompensa existente.  
  - **Parâmetros**: `id` - Identificador da recompensa.  
  - **Corpo da Requisição**:  
    ```json
    {
      "name": "Mini Placa Solar",
      "description": "Uma mini placa solar.",
      "pointsRequired": 10000
    }
    ```

- **DELETE /api/Reward/DeleteReward/{id}**  
  - **Descrição**: Exclui uma recompensa pelo ID.  
  - **Parâmetros**: `id` - Identificador da recompensa.  
