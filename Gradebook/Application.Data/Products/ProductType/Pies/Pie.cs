namespace AppCore.Data
{
    public class Pie : Product
    {
        public int PieId { get; set; }
        public string AllergyInformation { get; set; }
        public bool IsPieOfTheWeek { get; set; }
    }
}
