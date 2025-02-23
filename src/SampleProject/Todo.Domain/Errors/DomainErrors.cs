using System.Text.Json;

namespace Todo.Domain.Errors;

/// <summary>
/// Contains all the known errors of our application. This class is segmented in different partial classes,
/// one for each logical group. E.g. Errors.Auth, Errors.Todos, etc.
/// </summary>
public partial class DomainErrors
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
    };
}