```mermaid 
stateDiagram-v2
    [*] --> NonCustomer 
    NonCustomer --> Customer: Events.AccountAssociation
    Applicant --> Customer: Events.AccountAssociation
    Customer --> Customer: Events.AccountAssociation
    FormerCustomer --> Customer : Events.AccountAssociation
    Customer --> FormerCustomer : Events.AccountStatusUpdate [evt.Status == \"closed\" && subject.Accounts == 0]
    Customer --> Customer : Events.AccountStatusUpdate [evt.Status == \"closed\" && subject.Accounts >= 1]
    Customer --> Deceased : Events.LifeEventUpdate [evt.Status == \"deceased\"]
    Deceased --> Customer : Events.LifeEventUpdate [evt.Status == \"alive\"]
    FormerCustomer --> Deceased : Events.LifeEventUpdate [evt.Status == \"deceased\"]
```
# Schema


```json
{
  "Name": "ExampleProject",
  "Events": [
    {
      "Name": "AccountAssociation",
      "Properties": []
    },
    {
      "Name": "AccountStatusUpdate",
      "Properties": [
        {
          "Name": "Status",
          "Type": {
            "Name": "ExampleProject.Events.AccountStatus",
            "Values": [
              "Open",
              "Closed"
            ]
          }
        }
      ]
    },
    {
      "Name": "LifeEventUpdate",
      "Properties": [
        {
          "Name": "Status",
          "Type": {
            "Name": "ExampleProject.Events.LifeEventStatus",
            "Values": [
              "Alive",
              "Dead",
              "Pending"
            ]
          }
        }
      ]
    }
  ],
  "Entities": [
    {
      "Name": "CustomerView",
      "StateTypeName": "ExampleProject.Models.CustomerState",
      "States": [
        "NonCustomer",
        "Applicant",
        "Customer",
        "FormerCustomer",
        "Deceased"
      ]
    }
  ]
}
```