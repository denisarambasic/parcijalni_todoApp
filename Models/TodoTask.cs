using System.ComponentModel.DataAnnotations;

namespace Ispit.Todo.Models;

public class TodoTask
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public bool Status { get; set; } = false; //Po defaultu će uvijek biti false

    [Required]
    public int TodoListId { get; set; }

    public virtual TodoList TodoList { get; set; }
}