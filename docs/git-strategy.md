# Git Strategy - Investment Portfolio Aggregator

---

## Branch Strategy — GitHub Flow

main (protected)
  branch per task

Rules:
- main is always deployable
- Never commit directly to main
- One branch per task
- Branch is deleted after PR merge

---

## Branch Naming

task/001-solution-setup
task/002-value-objects
task/003-domain-entities
task/004-domain-events
task/005-repository-interfaces
task/006-use-cases-cqrs
task/007-infrastructure-efcore
task/008-eventstore
task/009-readmodel-projections
task/010-workers
task/011-minimal-api

Pattern: task/{number}-{short-description-kebab-case}

---

## Commit Convention — Conventional Commits

Format:
  type(scope): short description

Types:
  feat     - new feature
  fix      - bug fix
  test     - adding or updating tests
  refactor - code change without new feature or bug fix
  docs     - documentation only
  chore    - build, config, dependencies

Scopes:
  domain, application, infrastructure, api, workers, readmodel, eventstore

Examples:
  feat(domain): add Money value object
  test(domain): add Money value object TDD tests (Red phase)
  refactor(domain): improve Money arithmetic operations
  docs: update TASK-002 status to done
  chore: add FluentAssertions package to Domain.Tests

---

## PR Strategy

- One PR per task
- PR title: [TASK-XXX] Short description
- Use PR template (.github/pull_request_template.md)
- Squash merge to keep main history clean
- Delete branch after merge

PR title examples:
  [TASK-002] Add Value Objects with TDD
  [TASK-003] Add Domain Entities with TDD

---

## Recommended commit flow per task

1. git checkout -b task/002-value-objects
2. Write failing test -> git commit -m 'test(domain): add Money creation test (Red)'
3. Write code to pass -> git commit -m 'feat(domain): add Money value object (Green)'
4. Refactor -> git commit -m 'refactor(domain): improve Money validation and operators'
5. Update docs -> git commit -m 'docs: update TASK-002 status to done'
6. Open PR -> [TASK-002] Add Value Objects with TDD
7. Merge (Squash) -> delete branch
