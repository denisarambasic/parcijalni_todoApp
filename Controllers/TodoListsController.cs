using Ispit.Todo.Data;
using Ispit.Todo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ispit.Todo.Controllers;

[Authorize]
public class TodoListsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public TodoListsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: TodoLists
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        var lists = await _context.TodoLists
            .Where(t => t.UserId == user.Id)
            .ToListAsync();
        return View(lists);
    }

    // GET: TodoLists/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: TodoLists/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TodoList list)
    {
        var user = await _userManager.GetUserAsync(User);
        list.UserId = user.Id;
        _context.Add(list);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: TodoLists/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var list = await _context.TodoLists
            .Include(t => t.Tasks)
            .FirstOrDefaultAsync(t => t.Id == id);


        if (list == null)
            return NotFound();

        // filtriramo samo one taskove koji nisu završeni
        list.Tasks = list.Tasks.Where(t => !t.Status).ToList();

        return View(list);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var list = await _context.TodoLists
            .Include(l => l.Tasks)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (list == null)
            return NotFound();

        // obriši sve zadatke vezane uz listu
        _context.TodoTasks.RemoveRange(list.Tasks);

        // obriši listu
        _context.TodoLists.Remove(list);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

}
