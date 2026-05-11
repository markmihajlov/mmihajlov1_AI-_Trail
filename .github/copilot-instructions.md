# Copilot Instructions — mmihajlov1_AI-_Trail

> **Trust these instructions first.** Only search the codebase if the information here is incomplete or found to be in error.

## Repository Summary

This is the **AI Trail** repository (`mmihajlov1_AI-_Trail`). It is a full-stack web application for tracking engineer progress through the AI Trail program — a structured badge-based framework for AI adoption in the SDLC.

- **Repository URL:** https://github.com/markmihajlov/mmihajlov1_AI-_Trail
- **Primary language(s):** C# (.NET 10), TypeScript (React 19)
- **Frameworks / runtimes:** ASP.NET Core 10 Web API, React + Vite + MUI
- **Project type:** Full-stack web application (REST API + SPA)

## Build & Validation

### Bootstrap / Install

```bash
# Backend
cd src/backend/AiTrailTracker.Api
dotnet restore

# Frontend
cd src/frontend
npm install
```

### Build

```bash
# Backend
dotnet build src/backend/AiTrailTracker.Api

# Frontend
cd src/frontend
npm run build
```

### Test

```bash
# Backend (52+ unit tests)
dotnet test src/backend/AiTrailTracker.Api.Tests

# Frontend
cd src/frontend
npm test
```

### Lint

```bash
cd src/frontend
npm run lint
```

### Run

```bash
# Backend (starts on http://localhost:5000)
cd src/backend/AiTrailTracker.Api
dotnet run

# Frontend (starts on http://localhost:5173, proxies /api to backend)
cd src/frontend
npm run dev
```

### Known Issues / Workarounds

- If the backend process is already running, `dotnet test` may fail with file lock errors. Stop the running process first.
- In development, auth is bypassed via `DevBypassAuthHandler`. Configure `DevAuth` in `appsettings.Development.json`.

## Project Layout

```
mmihajlov1_AI-_Trail/
├── .github/
│   ├── copilot-instructions.md
│   └── instructions/
│       └── ai-attribution.instructions.md
├── .agent-os/specs/                    # Spec-driven workflow specs
├── docs/
│   ├── ai-trail-plan.md               # Program plan document
│   └── training-links.txt
├── src/
│   ├── backend/
│   │   ├── AiTrailTracker.Api/        # .NET 10 Web API
│   │   └── AiTrailTracker.Api.Tests/  # xUnit test project
│   └── frontend/                      # React + Vite + MUI SPA
└── mmihajlov1_AI-_Trail.sln
```

### Key Directories

| Directory | Purpose |
|-----------|---------|
| `src/backend/AiTrailTracker.Api/Controllers/` | REST API controllers |
| `src/backend/AiTrailTracker.Api/Services/` | Business logic services |
| `src/backend/AiTrailTracker.Api/Storage/` | Data access (Azure Table + InMemory) |
| `src/backend/AiTrailTracker.Api/Auth/` | Authentication handlers |
| `src/backend/AiTrailTracker.Api/Validation/` | URL validation & input sanitization |
| `src/frontend/src/pages/` | React page components |
| `src/frontend/src/components/` | Reusable UI components |
| `src/frontend/src/api/` | API client hooks (React Query + Axios) |

### CI / GitHub Actions

_No workflows have been added yet._

Pre-check-in validation:
1. `dotnet test src/backend/AiTrailTracker.Api.Tests`
2. `cd src/frontend && npm run lint && npm test`

## Coding Conventions

- Follow the existing code style and formatting of the project.
- Do not introduce new dependencies without justification.
- Keep changes minimal and focused on the task at hand.
- Always run the full test suite before considering a change complete.
- When adding new files, follow the existing directory structure and naming conventions.
- All mutating API endpoints are protected by anti-forgery tokens.
- URL evidence fields must be validated (http/https only) on both backend and frontend.
- Free-text fields are sanitized to prevent XSS.

## Environment Setup

- .NET 10 SDK
- Node.js >= 20, npm >= 10
- No Azure infrastructure required for local development (InMemoryStorage fallback)
