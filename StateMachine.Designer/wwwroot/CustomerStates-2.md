```mermaid 
stateDiagram-v2
    [*] --> NonCustomer
    NonCustomer --> Customer: AccountAssociation
    Applicant --> Customer: AccountAssociation
    Customer --> Customer: AccountAssociation
    FormerCustomer --> Customer : AccountAssociation
    Customer --> FormerCustomer : AccountStatusUpdate [evt.Status == \"closed\" && subject.Accounts == 0]
    Customer --> Customer : AccountStatusUpdate [evt.Status == \"closed\" && subject.Accounts >= 1]
    Customer --> Deceased : LifeEventUpdate [evt.Status == \"deceased\"]
    Deceased --> Customer : LifeEventUpdate [evt.Status == \"alive\"]
    FormerCustomer --> Deceased : LifeEventUpdate [evt.Status == \"deceased\"]
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
        "Suspect",
        "Applicant",
        "Known",
        "Former",
        "Forgotten",
        "Investigated",
        "BlackListed",
        "Archvied",
        "Verified",
        "Customer",
        "ReportedDeceased",
        "Deceased",

      ]
    },
    {
      "Name": "Test",
      "StateTypeName": "ExampleProject.Models.CustomerState",
      "States": [
        "NonCustomer",
        "Applicant"
      ]
    }
  ]
}
```