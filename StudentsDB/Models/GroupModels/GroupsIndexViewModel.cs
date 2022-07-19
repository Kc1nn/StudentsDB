namespace StudentsDB.Models.GroupModels
{
    public class GroupsIndexViewModel
    {
        public IEnumerable<Group> Groups { get; }
        public PageViewModel PageViewModel { get; }
        public FilterGroupsViewModel FilterGroupsViewModel { get; }
        public SortGroupsViewModel SortGroupsViewModel { get; }
        public GroupsIndexViewModel(IEnumerable<Group> groups, PageViewModel pageViewModel,
            FilterGroupsViewModel filterGroupsViewModel, SortGroupsViewModel sortGroupsViewModel)
        {
            Groups = groups;
            PageViewModel = pageViewModel;
            FilterGroupsViewModel = filterGroupsViewModel;
            SortGroupsViewModel = sortGroupsViewModel;
        }
    }
}
