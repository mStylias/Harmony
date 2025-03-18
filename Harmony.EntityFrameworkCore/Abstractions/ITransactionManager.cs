using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Harmony.EntityFrameworkCore.Abstractions;

public interface ITransactionManager
{
    /// <summary>
    ///     Gets the current <see cref="IDbContextTransaction" /> being used by the context, or null
    ///     if no transaction is in use.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This property is null unless one of <see cref="BeginTransaction" />,
    ///         <see cref="M:Microsoft.EntityFrameworkCore.RelationalDatabaseFacadeExtensions.BeginTransaction" />, or
    ///         <see cref="O:Microsoft.EntityFrameworkCore.RelationalDatabaseFacadeExtensions.UseTransaction" />
    ///         has been called.
    ///         No attempt is made to obtain a transaction from the current DbConnection or similar.
    ///     </para>
    ///     <para>
    ///         For relational databases, the underlying <see cref="DbTransaction" /> can be obtained using
    ///         <see cref="M:Microsoft.EntityFrameworkCore.Storage.DbContextTransactionExtensions.GetDbTransaction" />
    ///         on the returned <see cref="IDbContextTransaction" />.
    ///     </para>
    ///     <para>
    ///         See <see href="https://aka.ms/efcore-docs-transactions">Transactions in EF Core</see> for more information and examples.
    ///     </para>
    /// </remarks>
    IDbContextTransaction? CurrentTransaction { get; }
    
    /// <summary>
    ///     Gets or sets a value indicating whether or not a transaction will be created automatically by
    ///     <see cref="DbContext.SaveChanges()" /> if neither 'BeginTransaction' nor 'UseTransaction' has been called.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         The default setting is <see cref="AutoTransactionBehavior.WhenNeeded" />.
    ///     </para>
    ///     <para>
    ///         Setting this to <see cref="AutoTransactionBehavior.Never" /> with caution, since the database could be left in
    ///         an inconsistent state if failure occurs.
    ///     </para>
    ///     <para>
    ///         See <see href="https://aka.ms/efcore-docs-transactions">Transactions in EF Core</see> for more information and examples.
    ///     </para>
    /// </remarks>
    AutoTransactionBehavior AutoTransactionBehavior { get; set; }
    
    /// <summary>
    ///     Whether a transaction savepoint will be created automatically by <see cref="DbContext.SaveChanges()" /> if it is called
    ///     after a transaction has been manually started with <see cref="BeginTransaction" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         The default value is <see langword="true" />, meaning that <see cref="DbContext.SaveChanges()" /> will create a
    ///         transaction savepoint within a manually-started transaction. Regardless of this property, savepoints are only created
    ///         if the data provider supports them; see <see cref="IDbContextTransaction.SupportsSavepoints" />.
    ///     </para>
    ///     <para>
    ///         Setting this value to <see langword="false" /> should only be done with caution since the database could be left in a
    ///         corrupted state if <see cref="DbContext.SaveChanges()" /> fails.
    ///     </para>
    ///     <para>
    ///         See <see href="https://aka.ms/efcore-docs-transactions">Transactions in EF Core</see> for more information and examples.
    ///     </para>
    /// </remarks>
    bool AutoSavepointsEnabled { get; set; }
    
    /// <summary>
    ///     Starts a new transaction.
    /// </summary>
    /// <remarks>
    ///     See <see href="https://aka.ms/efcore-docs-transactions">Transactions in EF Core</see> for more information and examples.
    /// </remarks>
    /// <returns>
    ///     A <see cref="IDbContextTransaction" /> that represents the started transaction.
    /// </returns>
    IDbContextTransaction BeginTransaction();
    
    /// <summary>
    ///     Asynchronously starts a new transaction.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Entity Framework Core does not support multiple parallel operations being run on the same DbContext instance. This
    ///         includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
    ///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
    ///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see>
    ///         for more information and examples.
    ///     </para>
    ///     <para>
    ///         See <see href="https://aka.ms/efcore-docs-transactions">Transactions in EF Core</see> for more information and examples.
    ///     </para>
    /// </remarks>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    ///     A task that represents the asynchronous transaction initialization. The task result contains a <see cref="IDbContextTransaction" />
    ///     that represents the started transaction.
    /// </returns>
    /// <exception cref="OperationCanceledException">If the <see cref="CancellationToken" /> is canceled.</exception>
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    ///     Applies the outstanding operations in the current transaction to the database.
    /// </summary>
    void CommitTransaction();
    
    /// <summary>
    ///     Applies the outstanding operations in the current transaction to the database.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Entity Framework Core does not support multiple parallel operations being run on the same DbContext instance. This
    ///         includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
    ///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
    ///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see>
    ///         for more information and examples.
    ///     </para>
    ///     <para>
    ///         See <see href="https://aka.ms/efcore-docs-transactions">Transactions in EF Core</see> for more information and examples.
    ///     </para>
    /// </remarks>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    /// <exception cref="OperationCanceledException">If the <see cref="CancellationToken" /> is canceled.</exception>
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    ///     Discards the outstanding operations in the current transaction.
    /// </summary>
    /// <remarks>
    ///     See <see href="https://aka.ms/efcore-docs-transactions">Transactions in EF Core</see> for more information and examples.
    /// </remarks>
    void RollbackTransaction();
    
    /// <summary>
    ///     Discards the outstanding operations in the current transaction.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         See <see href="https://aka.ms/efcore-docs-transactions">Transactions in EF Core</see> for more information and examples.
    ///     </para>
    ///     <para>
    ///         Entity Framework Core does not support multiple parallel operations being run on the same DbContext instance. This
    ///         includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
    ///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
    ///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see>
    ///         for more information and examples.
    ///     </para>
    /// </remarks>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    /// <exception cref="OperationCanceledException">If the <see cref="CancellationToken" /> is canceled.</exception>
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    ///     Creates an instance of the configured <see cref="IExecutionStrategy" />.
    /// </summary>
    /// <remarks>
    ///     See <see href="https://aka.ms/efcore-docs-connection-resiliency">Connection resiliency and database retries</see>
    ///     for more information and examples.
    /// </remarks>
    /// <returns>An <see cref="IExecutionStrategy" /> instance.</returns>
    IExecutionStrategy CreateExecutionStrategy();
}