using StudentsDB.Models.GroupModels;

namespace StudentsDB.Models.StudentModels
{
    public class StudentsIndexViewModel
    {
        public Group Group { get; }
        public IEnumerable<Student> Students { get; }
        public PageViewModel PageViewModel { get; }
        public FilterStudentsViewModel FilterStudentsViewModel { get; }
        public SortStudentsViewModel SortStudentsViewModel { get; }
        public StudentsIndexViewModel(Group group, IEnumerable<Student> students, PageViewModel pageViewModel, FilterStudentsViewModel filterStudentsViewModel, SortStudentsViewModel sortStudentsViewModel)
        {
            Group = group;
            Students = students;
            PageViewModel = pageViewModel;
            FilterStudentsViewModel = filterStudentsViewModel;
            SortStudentsViewModel = sortStudentsViewModel;
        }
    }
}
