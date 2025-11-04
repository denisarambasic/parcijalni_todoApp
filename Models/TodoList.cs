using System.ComponentModel.DataAnnotations;

namespace Ispit.Todo.Models;

public class TodoList
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string UserId { get; set; }

    public virtual ApplicationUser User { get; set; }

    public virtual ICollection<TodoTask> Tasks { get; set; }
}
