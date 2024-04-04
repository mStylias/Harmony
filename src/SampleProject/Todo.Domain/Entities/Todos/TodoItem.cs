using Todo.Domain.Enums;

namespace Todo.Domain.Entities.Todos;

/// <summary>
/// This entity represents an individual todo that can be added to a <see cref="TodoList"/>>
/// </summary>
public class TodoItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public TodoStatus Status { get; set; }

    public int TodoListId { get; set; }
    
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public TodoItem() { /* Empty constructor needed for dapper */ }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}