# Tasks - Investment Portfolio Aggregator

## Status Legend
- [ ] To Do
- [~] In Progress
- [x] Done

---

## TASK-001 - Setup da estrutura da solucao
**Status:** [x] Done
**Goal:** Criar todos os projetos da solucao seguindo Clean Architecture + CQRS + Event-Driven.

Projects:
- InvestmentPortfolio.Domain (Class Library)
- InvestmentPortfolio.Application (Class Library)
- InvestmentPortfolio.Infrastructure (Class Library)
- InvestmentPortfolio.Api (Web API)
- InvestmentPortfolio.Workers (Worker Service)
- InvestmentPortfolio.ReadModel (Class Library)
- InvestmentPortfolio.EventStore (Class Library)
- InvestmentPortfolio.Domain.Tests (xUnit)
- InvestmentPortfolio.Application.Tests (xUnit)

Dependency rules:
- Api -> Application -> Domain
- Infrastructure -> Application -> Domain
- Workers -> Application -> Domain
- ReadModel -> Domain
- EventStore -> Domain
- Tests -> target project only

---

## TASK-002 - Criar Value Objects do dominio com TDD
**Status:** [ ] To Do
**Goal:** Modelar conceitos imutaveis do dominio como Value Objects.

Value Objects:
- Money (amount + currency, operacoes aritmeticas)
- Ticker (symbol validado — ex: AAPL, BTC, PETR4)
- Quantity (decimal nao-negativo)
- AssetType (Stock, Crypto, ETF, Fund)
- TransactionType (Buy, Sell, Dividend)

Ciclo TDD (Red -> Green -> Refactor) para cada Value Object.

---

## TASK-003 - Criar Entidades do dominio com TDD
**Status:** [ ] To Do
**Goal:** Implementar as entidades core do dominio.

Entities:
- Asset (Ticker, name, AssetType)
- BrokerAccount (name, userId)
- Transaction (Asset, Quantity, Money price, date, TransactionType)
- Position (Asset, Quantity, Money averagePrice) + calculos
- Portfolio (BrokerAccount, Positions) + calculo de valor total

---

## TASK-004 - Criar Domain Events
**Status:** [ ] To Do
**Goal:** Modelar eventos para suportar arquitetura event-driven e auditoria.

Events:
- TransactionImported
- AssetPurchased
- AssetSold
- DividendReceived
- PortfolioSnapshotGenerated

---

## TASK-005 - Criar interfaces de repositorio (Application Layer)
**Status:** [ ] To Do
**Goal:** Definir contratos de persistencia na camada de Application (Dependency Inversion).

Interfaces:
- IAssetRepository
- ITransactionRepository
- IPortfolioRepository
- IBrokerAccountRepository
- IUnitOfWork

---

## TASK-006 - Implementar Use Cases com TDD (Application Layer)
**Status:** [ ] To Do
**Goal:** Criar Commands e Queries usando CQRS com MediatR.

Commands:
- ImportTransactionCommand
- CreateBrokerAccountCommand

Queries:
- GetPortfolioSummaryQuery
- GetPositionsByAccountQuery

---

## TASK-007 - Implementar Infrastructure (EF Core + Repositories)
**Status:** [ ] To Do
**Goal:** Implementar persistencia com Entity Framework Core.

- DbContext + Migrations
- Implementacao dos repositorios
- Configuracoes de mapeamento (Fluent API)

---

## TASK-008 - Implementar EventStore
**Status:** [ ] To Do
**Goal:** Persistir e recuperar domain events para auditoria e replay.

- IEventStore interface (Application)
- EventStore implementacao (Infrastructure/EventStore)
- Modelo de persistencia de eventos

---

## TASK-009 - Implementar ReadModel e Projections
**Status:** [ ] To Do
**Goal:** Criar modelos de leitura otimizados (lado Query do CQRS).

- PortfolioSummaryReadModel
- PositionReadModel
- Projections a partir dos domain events

---

## TASK-010 - Implementar Workers (Background Services)
**Status:** [ ] To Do
**Goal:** Processamento assincrono de importacao e calculo de portfolio.

- TransactionImportWorker
- PortfolioSnapshotWorker

---

## TASK-011 - Implementar API (Minimal API)
**Status:** [ ] To Do
**Goal:** Expor endpoints REST com autenticacao JWT.

Endpoints:
- POST /broker-accounts
- POST /transactions/import
- GET /portfolio/summary
- GET /portfolio/positions
