namespace AppCore.Entities
{
    public class Speaker
    {
        public int SpeakerId { get; set; }
        public int ConferenceId { get; set; }
        public Conference Conference { get; private set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Company { get; set; }
        public string CompanyUrl { get; set; }
        public string BlogUrl { get; set; }
        public string Twitter { get; set; }
        public string GitHub { get; set; }
    }
}