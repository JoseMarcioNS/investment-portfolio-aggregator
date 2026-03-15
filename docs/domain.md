# Domain Model

## Core Entities

User
BrokerAccount
Portfolio
Transaction
Position
Asset

---

## Relationships

User
↓
BrokerAccount
↓
Transactions
↓
Portfolio
↓
Positions

---

## Domain Events (initial ideas)

TransactionsImported
AssetPurchased
AssetSold
DividendReceived
PortfolioSnapshotGenerated
