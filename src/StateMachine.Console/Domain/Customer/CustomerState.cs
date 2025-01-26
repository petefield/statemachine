namespace CustomerStateManagement.Domain.Customer;

public enum CustomerState
{
    Suspect,
    Applicant,
    Known,
    Former,
    Forgotten,
    UnderInvestigation,
    Blacklisted,
    Archived,
    Verified,
    Customer,
    ReportedDeceased,
    Deceased
}
