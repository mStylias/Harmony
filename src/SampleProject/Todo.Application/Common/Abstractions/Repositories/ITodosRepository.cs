﻿using Todo.Domain.Entities.Todos;

namespace Todo.Application.Common.Abstractions.Repositories;

public interface ITodosRepository
{
    Task<TodoList?> GetTodoListById(int todoListId, CancellationToken cancellationToken = default);
    Task<IEnumerable<TodoList>> GetTodoListsOfUserAsync(string userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<TodoItem>> GetTodoListItemsAsync(int todoListId, CancellationToken cancellationToken = default);
    Task<TodoItem?> GetTodoItemById(int todoItemId, CancellationToken cancellationToken = default);
    Task<IEnumerable<TodoItem>> GetTodoItemsOfMultipleListsAsync(IEnumerable<int> todoListIds,
        CancellationToken cancellationToken = default);
    Task<TodoList> CreateTodoListAsync(TodoList todoList);
    Task<TodoItem> CreateTodoItemAsync(TodoItem todoItem);
    Task<int> DeleteTodoList(int todoListId);
    Task<int> DeleteTodoItem(int todoItemId);
    Task<bool> TodoListExistsAsync(string name, string userId, CancellationToken cancellationToken = default);
    Task<bool> TodoListExistsAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> TodoItemExistsAsync(string name, int todoListId, CancellationToken cancellationToken = default);
    Task<bool> UserOwnsListAsync(int listId, string userId, CancellationToken cancellationToken = default);
}