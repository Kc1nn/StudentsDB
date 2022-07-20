using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsDB.Models;
using StudentsDB.Models.GroupModels;
using StudentsDB.Models.StudentModels;

namespace StudentsDB.Controllers
{
    public class HomeController : Controller
    {
        const int pageSize = 5;
        private readonly StudentsContext _context;

        public HomeController(StudentsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string name, string year, int page = 1, GroupsSortState sortOrder = GroupsSortState.NameAsc)
        {
            //фильтрация
            IQueryable<Group> groups = _context.Groups;

            if (!string.IsNullOrEmpty(name))
            {
                groups = groups.Where(p => p.Name!.Contains(name));
            }
            if (!string.IsNullOrEmpty(year))
            {
                groups = groups.Where(p => p.Year!.ToString().Contains(year));
            }

            // сортировка
            switch (sortOrder)
            {
                case GroupsSortState.NameDesc:
                    groups = groups.OrderByDescending(s => s.Name).ThenByDescending(s => s.Year);
                    break;
                default:
                    groups = groups.OrderBy(s => s.Name).ThenBy(s => s.Year);
                    break;
            }

            // пагинация
            var count = await groups.CountAsync();
            var items = await groups.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // формируем модель представления
            GroupsIndexViewModel viewModel = new GroupsIndexViewModel(
                items,
                new PageViewModel(count, page, pageSize),
                new FilterGroupsViewModel(name, year),
                new SortGroupsViewModel(sortOrder)
            );

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Group group)
        {
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Group? group = await _context.Groups.FirstOrDefaultAsync(p => p.Id == id);
                if (group != null)
                {
                    //Если в группе есть студенты - не выполняем запрос
                    List<Student> students = await _context.Students.Where(p => p.GroupId == id).ToListAsync();
                    if(students.Any())
                    {
                        return StatusCode(412, "Ошибка! Нельзя удалить группу, в которой есть студенты");
                    }

                    _context.Groups.Remove(group);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
            }
            return NotFound("Группа, которую вы пытаетесь удалить, не найдена");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                //Если в группе есть студенты - не выполняем запрос
                List<Student> students = await _context.Students.Where(p => p.GroupId == id).ToListAsync();
                if (students.Any())
                {
                    return StatusCode(412, "Ошибка! Нельзя изменить группу, в которой есть студенты");
                }

                Group? group = await _context.Groups.FirstOrDefaultAsync(p => p.Id == id);
                if (group != null) return View(group);
            }

            return NotFound("Группа, которую вы пытаетесь изменить, не найдена");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Group group)
        {
            _context.Groups.Update(group);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
