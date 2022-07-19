namespace StudentsDB.Models.StudentModels
{
    public class FilterStudentsViewModel
    {
        public FilterStudentsViewModel(string name, string surname, string age)
        {
            Name = name;
            Surname = surname;
            Age = age;
        }

        public string Name { get; }     // введенное имя студента
        public string Surname { get; }  // введенное фамилия студента
        public string Age { get; }      // введенное возраст студента
    }
}
