
```mermaid
flowchart TD
    exp_login(Experimenter login)
    exp_login --> exp_menu{Experimenter\n menu}
    exp_menu --> load_survey(Load survey from\n  external source)
    exp_menu --> create_survey(Create survey)
    exp_menu --> survey_statistics(Survey Statistics)
    
    
```