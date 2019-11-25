namespace AppCore.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }

        public int LocationId { get; set; }
        public bool Barista { get; set; }
    }
}