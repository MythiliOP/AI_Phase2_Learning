---
applyTo: "**/Controllers/**/*.cs,**/Services/**/*.cs"
---

# API Development Instructions

- Keep controllers thin.
- Business logic must be in services.
- Use dependency injection.
- Use async/await for I/O operations.
- Follow the existing repository patterns.
- Use DTOs for API request and response models.

## Test Case Generation

Ask Copilot to generate:

- Happy path scenarios
- Negative scenarios
- Edge cases
- Boundary conditions

## Defect Analysis

Ask Copilot to:

- Suggest root causes
- Recommend regression tests
- Identify risk areas