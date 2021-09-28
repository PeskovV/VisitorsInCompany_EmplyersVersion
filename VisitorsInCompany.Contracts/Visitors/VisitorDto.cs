namespace VisitorsInCompany.Contracts.Visitors
{
    public class VisitorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Organization { get; set; }
        public string VisitGoal { get; set; }
        public string Attendant { get; set; }
        public string Note { get; set; }
        public string EntryTime { get; set; }
        public string ExitTime { get; set; }
    }
}