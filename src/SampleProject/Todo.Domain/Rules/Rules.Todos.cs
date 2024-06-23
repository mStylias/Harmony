namespace Todo.Domain.Rules;

/// <summary>
/// Represents the domain rules of our application. Like Errors this class is segmented in different partial classes,
/// one for each logical group. E.g. Rules.Auth, Rules.Todos, etc.
/// </summary>
public static partial class Rules
{
    public static class Todos
    {
        public const int MaximumListNameCharacters = 50;
        
    }
}