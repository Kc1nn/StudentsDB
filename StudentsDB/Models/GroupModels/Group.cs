using StudentsDB.Models.StudentModels;

namespace StudentsDB.Models.GroupModels
{
    public class Group
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Year { get; set; }

        public List<Student> Students { get; set; } = new();
    }
}
