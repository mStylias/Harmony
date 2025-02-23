using System.Globalization;
using Harmony.Cqrs.Validators;
using Harmony.MinimalApis.Errors;
using Harmony.Results;
using Harmony.Results.ErrorTypes.InnerErrorTypes;
using Microsoft.Extensions.Logging;
using Todo.Application.Common.Abstractions.Repositories;
using Todo.Domain.Errors;
using Todo.Domain.Errors.Inner;
using Todo.Domain.Rules;

namespace Todo.Application.Todos.Lists.Commands.CreateTodoList;

public class CreateTodoListValidator : IOperationValidator<CreateTodoListCommand, Result<HttpError>>
{
    private readonly ILogger<CreateTodoListValidator> _logger;
    private readonly IUsersRepository _usersRepository;
    private readonly ITodosRepository _todosRepository;

    public CreateTodoListValidator(
        ILogger<CreateTodoListValidator> logger,
        IUsersRepository usersRepository, 
        ITodosRepository todosRepository)
    {
        _logger = logger;
        _usersRepository = usersRepository;
        _todosRepository = todosRepository;
    }
    
    // Here we don't need to await anything, but we need the async to be able 
    // to use the implicit operators of the Result class
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<Result<HttpError>> ValidateAsync(
        CreateTodoListCommand command,
        CancellationToken cancellationToken = default)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        var createTodoListInput = command.Input;
        if (createTodoListInput is null)
        {
            return DomainErrors.General.NullReferenceError(_logger, nameof(createTodoListInput));
        }

        var validationErrors = new List<ValidationInnerError>();
        
        // Name
        if (string.IsNullOrWhiteSpace(createTodoListInput.Name))
        {
            validationErrors.Add(new(
                InnerErrorCodes.Validation.RequiredPropertyNotProvided, 
                "Name is required", 
                nameof(createTodoListInput.Name).ToUpperInvariant()));
        }

        if (createTodoListInput.Name.Length > DomainRules.Todos.MaximumListNameCharacters)
        {
            validationErrors.Add(new(
                InnerErrorCodes.Validation.MaximumCharactersExceeded,
                $"Name exceeds maximum character limit: '{DomainRules.Todos.MaximumListNameCharacters}'",
                nameof(createTodoListInput.Name).ToUpperInvariant()));
        }
        
        // Description
        if (string.IsNullOrWhiteSpace(createTodoListInput.Description))
        {
            validationErrors.Add(new(
                InnerErrorCodes.Validation.RequiredPropertyNotProvided, 
                "Description is required", 
                nameof(createTodoListInput.Description).ToUpperInvariant()));
        }
        
        // User Id
        var userExists = await _usersRepository.UserExistsAsync(createTodoListInput.UserId, cancellationToken)
            .ConfigureAwait(false);
        if (userExists == false)
        {
            validationErrors.Add(new(
                InnerErrorCodes.Validation.EntityDoesNotExist,
                $"User with id '{createTodoListInput.UserId}' does not exist", 
                nameof(createTodoListInput.UserId).ToUpperInvariant()));
        }

        // Already exists
        var todoListExists = await _todosRepository.TodoListExistsAsync(
            createTodoListInput.Name, 
            createTodoListInput.UserId, 
            cancellationToken).ConfigureAwait(false);
        
        if (todoListExists)
        {
            validationErrors.Add(new(
                InnerErrorCodes.Validation.EntityAlreadyExists,
                "Todo list with the same name already exists for this user",
                nameof(createTodoListInput.Name).ToUpperInvariant()));
        }
        
        if (validationErrors.Count > 0)
        {
            return DomainErrors.General.ValidationError(_logger, validationErrors);
        }
        
        return Result.Ok();
    }
}