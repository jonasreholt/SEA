```mermaid
flowchart TD
    Start{Main menu} -->A1[Start survey]
    A1 --> A11{Select survey\n from list}
    A11 --> A12{Back to \n main menu}
    subgraph Surve A
    B1Start --> B1{Select \nversion}
    B1 --> B1.1[Version A]
    B1 --> B1.2[Version B]
    B1.1 --> B1Done[Store result]    
    B1.2 --> B1Done
    B1Done --> B1Final{Restart\n/End}    
    B1Final --> |Restart password?|B1
    end
    B1Final --> |Exit password?|A12
    A11 --> B2Start[Load survey]    
    subgraph Surve B
    B2Start --> B2{Select \nversion}
    B2 --> B2.1[Version A]
    B2 --> B2.2[Version B]
    B2.1 --> B2Done[Store result]    
    B2.2 --> B2Done
    B2Done --> B2Final{Restart\n/End}   
    B2Final --> |Restart password?|B2       
    end    
    A11 --> B1Start[Load survey]
    B2Final --> |Exit password?|A12
```