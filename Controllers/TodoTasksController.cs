using Ispit.Todo.Data;
using Ispit.Todo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ispit.Todo.Controllers;

[Authorize]
public class TodoTasksController : Controller
{
    private readonly ApplicationDbContext _context;

    public TodoTasksController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: TodoTasks/Create?listId=5
    public IActionResult Create(int listId)
    {
        ViewBag.ListId = listId;
        return View();
    }

    // POST: TodoTasks/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(int listId, TodoTask task)
    {
        task.TodoListId = listId;
        task.Status = false;
        _context.Add(task);
        await _context.SaveChangesAsync();
        return RedirectToAction("Details", "TodoLists", new { id = listId });
    }

    // POST: TodoTasks/MarkDone/5
    [HttpPost]
    public async Task<IActionResult> MarkDone(int id)
    {
        var task = await _context.TodoTasks.FindAsync(id);
        if (task == null)
            return NotFound();

        task.Status = true;
        _context.Update(task);
        await _context.SaveChangesAsync();

        return RedirectToAction("Details", "TodoLists", new { id = task.TodoListId });
    }

    // POST: TodoTasks/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var task = await _context.TodoTasks.FindAsync(id);
        _context.TodoTasks.Remove(task);
        await _context.SaveChangesAsync();
        return RedirectToAction("Details", "TodoLists", new { id = task.TodoListId });
    }
}
