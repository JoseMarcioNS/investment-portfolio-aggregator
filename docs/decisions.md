# Architecture Decision Records (ADR)

---

## ADR-007 - Git Strategy: GitHub Flow + Conventional Commits
**Data:** 2026-03-15
**Status:** Accepted

### Contexto
O projeto precisa de uma estrategia de versionamento que reflita praticas profissionais
e sirva de aprendizado para entrevistas.

### Decisao
- GitHub Flow: main protegida + branch por task (task/XXX-descricao)
- Conventional Commits: feat/fix/test/refactor/docs/chore com scope
- Um PR por task com template de checklist
- Squash merge para manter historico de main limpo

### Consequencias
+ Historico de commits legivel e profissional
+ PR template garante qualidade e documentacao consistente
+ Conventional Commits permitem geracao automatica de changelog
+ Padrao amplamente usado no mercado (relevante para entrevistas)
- Requer disciplina para seguir o fluxo em projeto solo

---

## ADR-001 - Clean Architecture como base
**Data:** 2026-03-15
**Status:** Accepted

### Contexto
O sistema precisa ser testavel, evolutivo e com separacao clara de responsabilidades.

### Decisao
Adotar Clean Architecture com 4 camadas: Domain, Application, Infrastructure, Api.

### Consequencias
+ Alta testabilidade (dominio sem dependencias externas)
+ Facil trocar frameworks e banco de dados
- Mais arquivos e estrutura inicial mais complexa

---

## ADR-002 - CQRS com ReadModel separado
**Data:** 2026-03-15
**Status:** Accepted

### Contexto
Operacoes de leitura (portfolio summary) tem requisitos diferentes das escritas (importar transacoes).

### Decisao
Separar Commands (escrita) de Queries (leitura) usando CQRS. ReadModel com projecoes otimizadas.

### Consequencias
+ Queries otimizadas independentemente
+ Escalabilidade independente de leitura e escrita
- Eventual consistency entre write model e read model

---

## ADR-003 - EventStore para auditoria e event-driven
**Data:** 2026-03-15
**Status:** Accepted

### Contexto
O sistema precisa de historico completo para auditoria e suportar arquitetura event-driven.

### Decisao
Criar projeto EventStore para persistir Domain Events. Permite replay de eventos e auditoria completa.

### Consequencias
+ Trilha de auditoria completa
+ Possibilidade de reconstruir estado a partir de eventos
+ Base para integracao com message brokers (RabbitMQ, Kafka)
- Complexidade adicional

---

## ADR-004 - Workers para processamento assincrono
**Data:** 2026-03-15
**Status:** Accepted

### Contexto
Importacao de transacoes e calculo de snapshots sao operacoes pesadas e nao devem bloquear a API.

### Decisao
Criar projeto InvestmentPortfolio.Workers com .NET Worker Services para processamento em background.

### Consequencias
+ API responsiva
+ Processamento escalavel independentemente
- Deploy e monitoramento adicional

---

## ADR-005 - TDD como pratica de desenvolvimento
**Data:** 2026-03-15
**Status:** Accepted

### Contexto
O projeto tem objetivo de aprendizado e preparacao para entrevistas. TDD garante qualidade e ensina design.

### Decisao
Adotar TDD (Red -> Green -> Refactor) em todas as camadas. Comecar pelos Value Objects e Entidades do dominio.

### Consequencias
+ Codigo mais confiavel e com design melhor
+ Facilita refactoring
+ Muito valorizado em entrevistas
- Curva de aprendizado inicial

---

## ADR-006 - MediatR para CQRS na camada Application
**Data:** 2026-03-15
**Status:** Accepted

### Contexto
Necessidade de desacoplar Commands/Queries dos handlers na camada Application.

### Decisao
Usar MediatR como mediator pattern para Commands, Queries e Domain Events.

### Consequencias
+ Handlers desacoplados
+ Pipeline behaviors (validacao, logging, transactions)
+ Padrao muito usado no mercado (relevante para entrevistas)
- Dependencia de biblioteca externa na Application
