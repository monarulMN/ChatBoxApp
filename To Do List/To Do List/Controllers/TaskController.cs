using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using To_Do_List.Data;
using To_Do_List.Models;

namespace To_Do_List.Controllers
{
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public TaskController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IActionResult> Index(string filter)
        {
            var tasks = _dbContext.Tasks.AsQueryable();

            if (filter == "active")
                tasks = tasks.Where(t => t.IsDone);
            else if(filter == "completed")
                tasks = tasks.Where(t =>t.IsDone);

            tasks = tasks.OrderByDescending(t => t.CreateAt);

            return View(await tasks.ToListAsync());
        }


        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskItem task)
        {
            if(ModelState.IsValid)
            {
                _dbContext.Add(task);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(task);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var task = await _dbContext.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            return View(task);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TaskItem task)
        {
            if (id != task.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _dbContext.Update(task);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _dbContext.Tasks.FindAsync(id);
            if (task != null)
            {
                _dbContext.Tasks.Remove(task);
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        public async Task<IActionResult> ToggleDone(int id)
        {
            var task = await _dbContext.Tasks.FindAsync(id);
            if (task != null)
            {
                task.IsDone = !task.IsDone;
                _dbContext.Update(task);
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
