namespace StudentsDB.Models.StudentModels
{
    public class SortStudentsViewModel
    {
        public StudentsSortState NameSort { get; }      // значение для сортировки по имени
        public StudentsSortState SurnameSort { get; }   // значение для сортировки по фамилии
        public StudentsSortState AgeSort { get; }       // значение для сортировки по возрасту
        public StudentsSortState Current { get; }       // текущее значение сортировки

        public SortStudentsViewModel(StudentsSortState sortOrder)
        {
            NameSort = sortOrder == StudentsSortState.NameAsc ? StudentsSortState.NameDesc : StudentsSortState.NameAsc;
            SurnameSort = sortOrder == StudentsSortState.SurnameAsc ? StudentsSortState.SurnameDesc : StudentsSortState.SurnameAsc;
            AgeSort = sortOrder == StudentsSortState.AgeAsc ? StudentsSortState.AgeDesc : StudentsSortState.AgeAsc;
            Current = sortOrder;
        }
    }
}
