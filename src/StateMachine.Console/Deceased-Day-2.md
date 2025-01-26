```mermaid 
stateDiagram-v2

state when_AccountClosed <<choice>>
state when_InvestigationCompleted <<choice>>

[*] --> Suspect
	Suspect --> Applicant: DetailsProvided
	Applicant --> Verified: RiskCheckPassed
	Applicant --> Known: SimplfiedCheckDone
	Applicant --> Blacklisted: ApplicationFraudDetected
	Verified --> Customer: AccountOpened
	Customer --> Customer: AccountOpened
	Customer --> when_AccountClosed:	AccountClosed
		when_AccountClosed --> Former:		if subject.AccountsHeld == 0
		when_AccountClosed --> Customer:	if subject.AccountsHeld != 0
	Customer --> UnderInvestigation: InvestigationStarted
	UnderInvestigation --> when_InvestigationCompleted: InvestigationCompleted
		when_InvestigationCompleted --> Customer: if evt.Outcome == true
		when_InvestigationCompleted --> Blacklisted: if evt.Outcome == false

	Customer --> ReportedDeceased: DeathReported
	ReportedDeceased --> Deceased: DeathConfirmed
	ReportedDeceased --> Customer: DeathReportedFalse
	Former --> Customer: AccountOpened
	Former --> Forgotten: RightToBeForgotten
```