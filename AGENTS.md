# AGENTS.md

## Project orientation

This repository contains the BeThere event software:

- `backend/BeThere/` — ASP.NET Core/.NET backend targeting .NET 10.
- `frontend/` — React, JavaScript, Vite, and plain CSS frontend.
- `BeThere.slnx` — .NET solution.
- `frontend/PROJECT_CONTEXT.md` — authoritative source for product decisions, scope, milestones, and learning goals.

Read the relevant sections of `PROJECT_CONTEXT.md` before planning work. This file complements that document; it must not duplicate its roadmap or decision log. Current explicit user decisions take precedence over older documentation, historical notes, or assumptions found in code.

Distinguish among:

- **Implemented behavior:** supported by current source and configuration.
- **Approved plan:** explicitly accepted for future work.
- **Proposal:** under discussion and not authorized for implementation.
- **Deferred feature:** intentionally outside the current scope.
- **Exploratory artifact:** mockup, sample data, experiment, or proof of concept.
- **Historical decision:** useful background that may no longer be current.

The .NET API is the chosen backend boundary. PostgreSQL is the approved persistence provider for future real persistence. The current backend uses EF Core InMemory and the frontend still uses mock data; neither fact should be mistaken for the final architecture.

## Efficient context gathering

Start with this file, the relevant project-context sections, and the current task. Inspect only the source, configuration, dependency, and documentation files needed for the task.

Use targeted searches such as `rg`, focused directory listings, and direct file inspection. Provide concise progress updates for substantial work. Avoid loading every document, repeating unchanged context, launching multiple agents by default, or creating unnecessary documentation.

Do not install dependencies or launch services unless the approved task explicitly requires it.

## Grounding and uncertainty

Verify factual claims against actual code and configuration. Cite repository paths when useful. Clearly label unknowns and assumptions.

Treat documentation as guidance that must be checked against code. Never invent capabilities, approvals, successful checks, test results, implementation status, API routes, schemas, or deployment details. Code is evidence of current behavior, but does not prove that behavior remains desired.

Follow the current scope and decisions in
`frontend/PROJECT_CONTEXT.md`. Do not implement
deferred or proposed features unless the user explicitly re-approves them.
When this file and older documentation conflict, follow the user's latest
explicit decision.

## Approval-based development workflow

Before implementation:

1. Understand the request and identify the relevant code.
2. Explain a manageable plan covering intended behavior, likely files, acceptance criteria, risks, and verification.
3. Wait for the user’s explicit approval.

If approval includes requested changes, incorporate them and confirm the revised plan before implementation. After approval, perform routine implementation and verification within that scope without repeatedly asking. Ask again only for a material change to scope, architecture, data model, API contract, permissions, deployment, or external systems.

After implementation, review the diff, run the approved checks, record the results, and explain important choices and tradeoffs so the user can understand and defend the change.

## Task scope and continuity

For substantive work, use a concise task record with a stable ID, scope, acceptance criteria, status, and verification results. If no existing system serves this purpose, recommend `tasks/T-NNN.md`.

Do not require task paperwork for ordinary questions or trivial corrections. Keep task records brief. Never mark work complete while required checks, review, or delivery steps remain unresolved.

## Contracts, persistence, and verification

Before changing behavior, identify affected API routes, request and response shapes, data structures, configuration, database relationships, and user-visible behavior.

PostgreSQL is approved as the persistence provider. Do not substitute Supabase, SQLite, or another provider without explicit approval. The React frontend must communicate with PostgreSQL through the .NET API and must never connect directly to the database.

Treat database schema, migrations, API routes, payloads, persistence behavior, and connection configuration as deliberate decisions requiring approval. Do not invent a schema, migration strategy, API contract, connection configuration, or deployment approach. Preserve compatibility unless a breaking change is explicitly approved.

Update relevant existing documentation when behavior changes. Recommend `docs/contracts.md` only if it would clarify an active contract; do not claim it exists or require a large documentation bootstrap.

Commands identified in the current repository are:

- Backend build, from the repository root: `dotnet build BeThere.slnx`
- Frontend build, from `frontend/`: `npm run build`
- Frontend lint, from `frontend/`: `npm run lint`
- Combined development launcher, from the repository root: `./scripts/start-dev.sh`

Record exactly what ran, what passed, what failed, and what remains unverified. Build or lint success is not behavioral test coverage. No automated test command or test project is currently defined; do not invent one or claim tests passed.

## Boundaries and delivery

Use a dedicated task branch for substantive code changes. Preserve unrelated work and existing user changes, especially in a dirty working tree.

Never commit PostgreSQL credentials, connection strings, secrets, local database files, or private environment configuration. Require explicit authorization before destructive actions, deployment, pushing, merging, or changes affecting external systems.

Surface new dependencies and major architecture changes in the plan for approval. Prefer small, understandable changes over speculative infrastructure.

## Learning and future server use

Explain important choices and tradeoffs without overwhelming the user. Preserve the user’s ownership of the project.

Keep task definitions and verification steps clear enough for a future server task runner, but do not introduce a queue, worker service, remote execution, or server configuration now. Unattended execution requires a separately approved permission policy; the default remains interactive plan approval.
