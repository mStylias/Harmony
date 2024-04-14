using System.Text.Json;

namespace Todo.Domain.Errors;

public partial class Errors
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true
    };
}