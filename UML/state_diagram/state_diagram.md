```mermaid 
stateDiagram


[*]                 --> MainMenu
MainMenu            --> SuperUserMenu
MainMenu            --> ExperimenterMenu
SuperUserMenu       --> MainMenu
SuperUserMenu       --> ModifySurvey
ModifySurvey        --> SuperUserMenu
ExperimenterMenu    --> MainMenu
ExperimenterMenu    --> RunExperiment
RunExperiment       --> LockScreen
LockScreen          --> ExperimenterMenu
MainMenu            --> [*]

```