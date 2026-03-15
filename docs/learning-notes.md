# Learning Notes - Investment Portfolio Aggregator

> Explicacoes escritas em portugues para facilitar o aprendizado.

---

## 7. Git Strategy — GitHub Flow, Conventional Commits e PRs

### GitHub Flow — por que nao GitFlow?

GitFlow tem branches como `develop`, `release`, `hotfix`. E robusto, mas complexo.
Para a maioria das empresas hoje (especialmente startups e times ageis), o **GitHub Flow** e suficiente e mais rapido:

- `main` sempre esta pronta para deploy
- Voce cria uma branch para cada feature/task
- Abre um PR, revisao, merge — pronto

**O que cai em entrevista:**
- 'Qual estrategia de branching voce usa?' — explique GitHub Flow vs GitFlow e quando usar cada um
- 'O que e trunk-based development?' — alternativa onde todo mundo commita direto na main com feature flags

### Conventional Commits — por que padronizar mensagens?

Mensagens de commit sao a documentacao do historico do projeto. Compare:

Ruim:
  git commit -m 'arrumei o bug'
  git commit -m 'adicionei coisa nova'

Bom (Conventional Commits):
  git commit -m 'fix(domain): prevent negative Money amount creation'
  git commit -m 'feat(domain): add Money arithmetic operators'
  git commit -m 'test(domain): add Money value object TDD cycle'

Com Conventional Commits voce consegue:
- Gerar CHANGELOG automatico
- Calcular versao semantica automaticamente (semver)
- Entender o historico sem abrir o codigo

Formato: type(scope): descricao
Types: feat, fix, test, refactor, docs, chore, perf, ci

**O que cai em entrevista:**
- 'Como voce escreve mensagens de commit?' — cite Conventional Commits
- 'O que e Semantic Versioning (semver)?' — MAJOR.MINOR.PATCH ligado a breaking changes, features e fixes

### Squash Merge — por que nao Merge Commit ou Rebase?

| Estrategia | Resultado no main | Quando usar |
|---|---|---|
| Merge Commit | Preserva todos os commits da branch | Quer historico completo |
| Squash Merge | Todos os commits viram 1 no main | Historico limpo por feature |
| Rebase | Reescreve historico linear | Times avancados |

Squash Merge e ideal para aprendizado: cada task vira 1 commit limpo no main,
mas voce ainda tem liberdade de fazer quantos commits quiser durante o desenvolvimento.

**O que cai em entrevista:**
- 'Qual a diferenca entre merge, squash merge e rebase?'
- 'O que e um conflito de merge e como resolver?'


A Clean Architecture garante isso colocando as regras de negocio no centro (Domain) e os detalhes tecnicos na borda (Infrastructure, Api).

**Regra de dependencia:** as setas sempre apontam para dentro. Domain nao conhece nada de fora.

``nApi -> Application -> Domain
Infrastructure -> Application -> Domain
`  

**O que cai em entrevista:**
- 'Por que Domain nao pode referenciar Infrastructure?'
- 'O que e Dependency Inversion Principle?' (repositorios sao interfaces no Domain/Application)

---

## 2. CQRS — Separar leitura de escrita

CQRS = Command Query Responsibility Segregation.

A ideia e simples: **escrever** e **ler** sao operacoes completamente diferentes.

- **Command:** muda estado. Ex: ImportarTransacao. Nao retorna dados, apenas confirma.
- **Query:** le estado. Ex: BuscarResumodoPortfolio. Nao muda nada, so retorna dados.

Por que separar? Porque a query de 'resumo do portfolio' pode precisar de joins complexos, agregacoes, etc. Se misturar com o modelo de escrita, voce complica os dois.

**ReadModel:** um modelo de dados otimizado especificamente para leitura. Pode ser uma view, uma tabela desnormalizada, um cache. Nao precisa seguir as regras do Domain.

**O que cai em entrevista:**
- 'O que e CQRS e quando usar?'
- 'Qual a diferenca entre Command e Query?'
- 'O que e eventual consistency?'

---

## 3. Domain Events — O que aconteceu no sistema

Domain Events representam **fatos que ja aconteceram** no dominio. Sao imutaveis.

Exemplo: quando uma transacao e importada, o sistema publica um evento TransactionImported. Qualquer parte do sistema interessada pode reagir a esse evento sem que o Domain saiba quem sao os interessados.

Isso e o principio Open/Closed: o Domain esta fechado para modificacao, mas aberto para extensao via eventos.

**O que cai em entrevista:**
- 'O que sao Domain Events?'
- 'Qual a diferenca entre Domain Event e Integration Event?'
- 'Como voce garante que um evento nao seja perdido?' (Outbox Pattern)

---

## 4. EventStore — Guardar a historia completa

Em vez de guardar so o estado atual ('posicao atual: 100 acoes de PETR4'), o EventStore guarda todos os eventos que levaram a esse estado:

1. CompradoAsset: PETR4, 50 acoes, R$ 30,00
2. CompradoAsset: PETR4, 80 acoes, R$ 28,00
3. VendidoAsset: PETR4, 30 acoes, R$ 35,00
-> Estado atual: 100 acoes, preco medio: R$ 28,80

Isso permite **replay**: se uma regra de negocio mudar, voce pode recalcular tudo a partir dos eventos originais.

**O que cai em entrevista:**
- 'O que e Event Sourcing?'
- 'Qual a diferenca entre Event Sourcing e Event-Driven Architecture?'

---

## 5. TDD — Testar primeiro, codificar depois

TDD = Test-Driven Development. O ciclo e:

1. **Red:** escreve um teste que falha (o codigo ainda nao existe)
2. **Green:** escreve o minimo de codigo para o teste passar
3. **Refactor:** melhora o codigo sem quebrar os testes

Por que comecar pelos Value Objects?
Value Objects sao pequenos, imutaveis e sem dependencias. Sao o lugar perfeito para aprender TDD antes de partir para entidades mais complexas.

Exemplo de ciclo TDD para o Value Object Money:

`csharp
// RED: teste falha pois Money nao existe ainda
[Fact]
public void Money_WhenCreatedWithNegativeAmount_ShouldThrowException()
{
    var act = () => new Money(-10, 'BRL');
    act.Should().Throw<DomainException>();
}

// GREEN: cria Money com validacao minima
public record Money
{
    public Money(decimal amount, string currency)
    {
        if (amount < 0) throw new DomainException('Amount cannot be negative');
        Amount = amount;
        Currency = currency;
    }
    public decimal Amount { get; }
    public string Currency { get; }
}

// REFACTOR: melhora validacoes, adiciona operacoes aritmeticas
`  

**O que cai em entrevista:**
- 'Voce usa TDD? Explique o ciclo Red-Green-Refactor.'
- 'Qual a diferenca entre unit test, integration test e e2e test?'
- 'O que e test coverage e por que nao deve ser o unico objetivo?'

---

## 6. Value Objects vs Entidades — A diferenca fundamental

| | Value Object | Entity |
|---|---|---|
| Identidade | Pelo valor | Por ID unico |
| Mutabilidade | Imutavel | Mutavel |
| Exemplo | Money(100, BRL) | Transaction(id: 42) |
| Igualdade | Money(100,BRL) == Money(100,BRL) | Entity so e igual se mesmo ID |

Dois Money(100, 'BRL') sao **iguais** — tanto faz qual nota de R voce tem.
Duas Transactions com mesmo valor mas IDs diferentes sao **diferentes** — sao transacoes distintas.

**O que cai em entrevista:**
- 'O que e um Value Object em DDD?'
- 'Por que Value Objects devem ser imutaveis?'
- 'Como implementar igualdade por valor em C#?' (record ou override Equals/GetHashCode)
