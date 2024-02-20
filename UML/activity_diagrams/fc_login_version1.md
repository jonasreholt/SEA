
```mermaid
flowchart TD
    A[Start program] --> B(Login screen)
    B --> C{Select user type}
    C -->|Exprimentee| D[Select experiment]
    C -->|Experimenter| E[Menu for Experimenter]
    C -->|Experiments admin| F[Menu for Admin]
```