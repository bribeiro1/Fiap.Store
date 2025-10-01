# Fiap.Store – Microserviço de Pedidos

## Contexto de Negócio
Este microserviço faz parte de um cenário de **e-commerce/loja**.  
Ele é responsável por **gerenciar pedidos** (Orders) e aplicar **políticas de desconto**:

- Criar novos pedidos com itens (SKU, quantidade, preço unitário).
- Calcular o **valor final com desconto**, considerando:
  - 5% para pedidos com subtotal ≥ 500
  - 10% para pedidos com subtotal ≥ 1000
  - Cupom promocional 'OFF20' com 20%
  - Sempre prevalece o **melhor desconto**

---

## Arquitetura e Camadas

O projeto segue **DDD + Clean Architecture**:

- **Fiap.Store.Domain**  
  - Entidades ('Order', 'OrderItem')  
  - Value Objects  
  - Serviços de Domínio ('OrderService')  
  - Contratos ('IOrderRepository')  

- **Fiap.Store.Application**  
  - Casos de uso (Commands/Handlers com MediatR)  
  - Validações com FluentValidation  
  - DTOs para transporte  

- **Fiap.Store.Infra**  
  - Repositório InMemory ('OrderRepository','IOrderService')  
  - Implementa as interfaces do domínio  

- **Fiap.Store.Api**  
  - API minimal em ASP.NET Core 8  
  - Endpoints '/orders' e '/orders/{id}/price'  
  - Swagger para documentação  

- **Fiap.Store.Tests**  
  - Testes unitários com xUnit + FluentAssertions + Moq  
  - Cobrem cenários de sucesso e erro  

---

## Executando localmente

### Pré-requisitos
- .NET 8 SDK
- Git

### Comandos
'''bash
# restaurar pacotes
dotnet restore

# rodar testes com cobertura
dotnet test --collect:"XPlat Code Coverage"

# executar a API
dotnet run --project src/Fiap.Store.Api
