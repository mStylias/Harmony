namespace Todo.Domain.Rules;

/// <summary>
/// Represents the domain rules of our application. Like Errors this class is segmented in different partial classes,
/// one for each logical group. E.g. DomainRules.Auth, DomainRules.Todos, etc.
/// </summary>
public static partial class DomainRules
{
    public static class Todos
    {
        public const int MaximumListNameCharacters = 100;
        public const int MaximumTodoItemNameCharacters = 100;
    }
}