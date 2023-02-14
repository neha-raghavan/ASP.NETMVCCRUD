namespace ASP.NETMVCCRUD.Models
{
    public class AddEmployeeView
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public long Salary { get; set; }
        public DateTime DOB { get; set; }
        public string Department { get; set; }
    }
}
