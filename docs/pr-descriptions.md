# PR Descriptions — Investment Portfolio Aggregator

> Descriptions generated automatically by Copilot for each task PR.

---

## [TASK-003] Add Domain Entities with TDD

### Description
Added all domain entities following TDD (Red-Green-Refactor cycle).

**Created:**
- `Entity` base class with equality by Id
- `Asset` (Ticker, Name, AssetType)
- `BrokerAccount` (Name, UserId)  
- `Transaction` (Asset, Quantity, Price, Date, Type) + TotalValue calculation
- `Position` (Asset, Quantity, AveragePrice) + Apply(transaction) with buy/sell logic
- `Portfolio` (BrokerAccount, Positions) + TotalValue calculation + GetPositionByTicker
- `DomainException` for business rule violations

**Business Logic Implemented:**
- Average price calculation when buying assets (weighted average)
- Position update on buy/sell transactions
- Validation: cannot sell more than available quantity
- Validation: cannot add duplicate asset position to portfolio

**Test Coverage:** 65 tests passing (32 new entity tests + 33 previous value object tests)

### Related Task
TASK-003

### Type of change
- [x] feat — new feature
- [x] test — adding or updating tests

### TDD Checklist
- [x] Tests written first (Red phase)
- [x] Minimum code written to pass tests (Green phase)
- [x] Code refactored without breaking tests (Refactor phase)
- [x] All tests passing

### Quality Checklist
- [x] Build passing
- [x] No new compilation warnings
- [x] docs/tasks.md updated (task status)
- [ ] docs/decisions.md updated (no new architecture decision)
- [x] docs/learning-notes.md updated (local only, not in repo)
- [x] Dependency rules respected (Domain has no external dependencies)

---

## [TASK-002] Add Value Objects with TDD

### Description
Added Money, Ticker, Quantity, AssetType and TransactionType value objects to the Domain layer following TDD (Red-Green cycle).

**Created:**
- `Money` (amount + currency, arithmetic operators)
- `Ticker` (symbol validated, normalized to uppercase)
- `Quantity` (non-negative decimal + arithmetic operators)
- `AssetType` (enum: Stock, Crypto, Etf, Fund)
- `TransactionType` (enum: Buy, Sell, Dividend)

**Test Coverage:** 33 tests passing

### Related Task
TASK-002

### Type of change
- [x] feat — new feature
- [x] test — adding or updating tests
- [x] chore — added FluentAssertions package

### TDD Checklist
- [x] Tests written first (Red phase)
- [x] Minimum code written to pass tests (Green phase)
- [x] All tests passing

### Quality Checklist
- [x] Build passing
- [x] No new compilation warnings
- [x] docs/tasks.md updated
- [ ] docs/decisions.md updated (no new decisions)
- [x] docs/learning-notes.md updated (local only)
- [x] Dependency rules respected
