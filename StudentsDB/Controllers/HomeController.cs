using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsDB.Models;
using StudentsDB.Models.GroupModels;

namespace StudentsDB.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudentsContext _context;

        public HomeController(StudentsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string name, string year, int page = 1, GroupsSortState sortOrder = GroupsSortState.NameAsc)
        {
            int pageSize = 5;

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
                    groups = groups.OrderByDescending(s => s.Name);
                    break;
                case GroupsSortState.YearAsc:
                    groups = groups.OrderBy(s => s.Year);
                    break;
                case GroupsSortState.YearDesc:
                    groups = groups.OrderByDescending(s => s.Year);
                    break;
                default:
                    groups = groups.OrderBy(s => s.Name);
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
                    _context.Groups.Remove(group);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Group? group = await _context.Groups.FirstOrDefaultAsync(p => p.Id == id);
                if (group != null) return View(group);
            }

            return NotFound();
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
