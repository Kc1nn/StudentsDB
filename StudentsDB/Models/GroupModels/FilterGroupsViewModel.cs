namespace StudentsDB.Models.GroupModels
{
    public class FilterGroupsViewModel
    {
        public FilterGroupsViewModel(string name, string year)
        {
            Name = name;
            Year = year;
        }

        public string Name { get; } // введенное название группы
        public string Year { get; } // введенный год создания группы
    }
}
