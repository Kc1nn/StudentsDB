using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsDB.Models;
using StudentsDB.Models.GroupModels;
using StudentsDB.Models.StudentModels;

namespace StudentsDB.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentsContext _context;

        public StudentController(StudentsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id, string name, string surname, string age, int page = 1, int pageSize = 5, StudentsSortState sortOrder = StudentsSortState.NameAsc)
        {
            if (id != null)
            {
                //фильтрация
                IQueryable<Student> students = _context.Students.Where(p => p.GroupId == id);

                if (!string.IsNullOrEmpty(name))
                {
                    students = students.Where(p => p.Name!.Contains(name));
                }
                if (!string.IsNullOrEmpty(surname))
                {
                    students = students.Where(p => p.Surname!.Contains(surname));
                }
                if (!string.IsNullOrEmpty(age))
                {
                    students = students.Where(p => p.Age!.ToString().Contains(age));
                }

                // сортировка
                switch (sortOrder)
                {
                    case StudentsSortState.NameDesc:
                        students = students.OrderByDescending(s => s.Name);
                        break;
                    case StudentsSortState.SurnameAsc:
                        students = students.OrderBy(s => s.Surname);
                        break;
                    case StudentsSortState.SurnameDesc:
                        students = students.OrderByDescending(s => s.Surname);
                        break;
                    case StudentsSortState.AgeAsc:
                        students = students.OrderBy(s => s.Age);
                        break;
                    case StudentsSortState.AgeDesc:
                        students = students.OrderByDescending(s => s.Age);
                        break;
                    default:
                        students = students.OrderBy(s => s.Name);
                        break;
                }

                // пагинация
                var count = await students.CountAsync();
                var items = await students.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

                // формируем модель представления
                StudentsIndexViewModel viewModel = new StudentsIndexViewModel(
                    _context.Groups.FirstOrDefault(p => p.Id == id)!,
                    items,
                    new PageViewModel(count, page, pageSize),
                    new FilterStudentsViewModel(name, surname, age),
                    new SortStudentsViewModel(sortOrder)
                );
                if (viewModel.Group != null && viewModel.Students != null)
                    return View(viewModel);
            }
            return NotFound("Группа, список студентов которой вы пытаетесь получить, не найдена");
        }

        public IActionResult Create(int id)
        {
            return View(new Student() { GroupId = id });
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            student.Id = 0;
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { id = student.GroupId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Student? student = await _context.Students.FirstOrDefaultAsync(p => p.Id == id);
                if (student != null)
                {
                    _context.Students.Remove(student);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index), new { id = student.GroupId });
                }
            }

            return NotFound("Студент, которого вы пытаетесь удалить, не найден");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Student? student = await _context.Students.FirstOrDefaultAsync(p => p.Id == id);
                if (student != null) 
                    return View(student);
            }

            return NotFound("Студент, которого вы пытаетесь изменить, не найден");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { id = student.GroupId });
        }
    }
}
