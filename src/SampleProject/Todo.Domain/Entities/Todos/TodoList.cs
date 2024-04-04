namespace Todo.Domain.Entities.Todos;

/// <summary>
/// This entity represents a todo list, that can contain multiple <see cref="TodoItem"/>
/// </summary>
public class TodoList
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int UserId { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public TodoList() { /* Empty constructor needed for dapper */ }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}