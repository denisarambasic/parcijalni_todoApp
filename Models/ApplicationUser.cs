using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Ispit.Todo.Models;

public class ApplicationUser : IdentityUser
{
    [StringLength(50)]
    public string? FirstName { get; set; }

    [StringLength(50)]
    public string? LastName { get; set; }

    public virtual ICollection<TodoList> TodoLists { get; set; }
}