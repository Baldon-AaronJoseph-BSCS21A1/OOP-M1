namespace WinFormsApp1.Repositories
{
    public class Student
    {
        public int ID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Name => $"{FirstName} {LastName}";
        public string Section { get; set; } = string.Empty;
        public string Course { get; set; } = string.Empty;
    }
}