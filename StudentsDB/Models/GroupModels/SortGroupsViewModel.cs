namespace StudentsDB.Models.GroupModels
{
    public class SortGroupsViewModel
    {
        public GroupsSortState NameSort { get; } // значение для сортировки по названию
        public GroupsSortState YearSort { get; }    // значение для сортировки по году
        public GroupsSortState Current { get; }     // текущее значение сортировки

        public SortGroupsViewModel(GroupsSortState sortOrder)
        {
            NameSort = sortOrder == GroupsSortState.NameAsc ? GroupsSortState.NameDesc : GroupsSortState.NameAsc;
            YearSort = sortOrder == GroupsSortState.YearAsc ? GroupsSortState.YearDesc : GroupsSortState.YearAsc;
            Current = sortOrder;
        }
    }
}
