using StateDiagram.Parser;

namespace StateMachine.Parser.Tests
{
    public class ParserTests
    {
        [Fact]
        public void Parse_WhenGivenAValidStateDiagram_Returns_CorrectNumberOfTransitions()
        {
            var INPUT = @"``````mermaid
                        stateDiagram-v2

                        state when_AccountClosed <<choice>>

                        [*]			--> Suspect: CustomerCreated
                        Suspect		--> Applicant:			DetailsProvided
                        Applicant	--> Verified:			RiskCheckPassed
                        Applicant	--> Known:				SimplifiedCheckDone
                        Applicant	--> Blacklisted:		ApplicationFraudDetected
                        Verified	--> Customer:			AccountOpened
                        Customer	--> when_AccountClosed:	AccountClosed
                            when_AccountClosed --> Former:		if AccountsHeld == 0
                            when_AccountClosed --> Customer:	if AccountsHeld != 0
                        Customer --> UnderInvestigation:	InvestigationStarted
                        Customer --> ReportedDeceased:		DeathReported
                        ReportedDeceased --> Deceased:		DeathConfirmed
                        ReportedDeceased --> Customer:		DeathReportedFalse
                        Former --> Customer:				AccountOpened
                        Former	--> Forgotten:				RightToBeForgotten
                        UnderInvestigation --> Blacklisted:	InvestigationOutcomeTrue
                        UnderInvestigation --> Customer:	InvestigationOutcomeFalse
                        Blacklisted --> [*]
                        Deceased --> [*]

                        ```";
            var transitions = StateDiagram.Parser.Parser.Parse(INPUT).ToList();

            Assert.Equal(16, transitions.Count());
        }
    }
}