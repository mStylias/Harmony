using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace Harmony.EntityFrameworkCore.Abstractions;

public interface IDatabaseManager
{
    /// <summary>
    ///     Returns the name of the database provider currently in use.
    ///     The name is typically the name of the provider assembly.
    ///     It is usually easier to use a sugar method such as
    ///     <see cref="M:Microsoft.EntityFrameworkCore.SqlServerDatabaseFacadeExtensions.IsSqlServer" />
    ///     instead of calling this method directly.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This method can only be used after the <see cref="DbContext" /> has been configured because
    ///         it is only then that the provider is known. This means that this method cannot be used
    ///         in <see cref="DbContext.OnConfiguring" /> because this is where application code sets the
    ///         provider to use as part of configuring the context.
    ///     </para>
    ///     <para>
    ///         See <see href="https://aka.ms/efcore-docs-dbcontext">DbContext lifetime, configuration, and initialization</see>
    ///         for more information and examples.
    ///     </para>
    /// </remarks>
    string? ProviderName { get; }
    
    /// <summary>
    ///     Provides access to change tracking information and operations for entity instances the context is tracking.
    ///     Instances of this class are typically obtained from <see cref="DbContext.ChangeTracker" /> and it is not designed
    ///     to be directly constructed in your application code.
    /// </summary>
    /// <remarks>
    ///     See <see href="https://aka.ms/efcore-docs-change-tracking">EF Core change tracking</see> for more information and examples.
    /// </remarks>
    ChangeTracker ChangeTracker { get; }
    
    /// <summary>
    ///     Metadata about the shape of entities, the relationships between them, and how they map to
    ///     the database. A model is typically created by overriding the
    ///     <see cref="DbContext.OnModelCreating(ModelBuilder)" /> method on a derived
    ///     <see cref="DbContext" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         The service lifetime is <see cref="ServiceLifetime.Scoped" />. This means that each
    ///         <see cref="DbContext" /> instance will use its own instance of this service.
    ///         The implementation may depend on other services registered with any lifetime.
    ///         The implementation does not need to be thread-safe.
    ///     </para>
    ///     <para>
    ///         See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> for more information and
    ///         examples.
    ///     </para>
    /// </remarks>
    IModel Model { get; }
    
    /// <summary>
    ///     A unique identifier for the context instance and pool lease, if any.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This identifier is primarily intended as a correlation ID for logging and debugging such
    ///         that it is easy to identify that multiple events are using the same or different context instances.
    ///     </para>
    ///     <para>
    ///         See <see href="https://aka.ms/efcore-docs-dbcontext">DbContext lifetime, configuration, and initialization</see>
    ///         for more information and examples.
    ///     </para>
    /// </remarks>
    DbContextId ContextId { get; }
    
    /// <summary>
    ///     Ensures that the database for the context exists.
    /// </summary>
    /// <remarks>
    ///     <list type="bullet">
    ///         <item>
    ///             <description>
    ///                 If the database exists and has any tables, then no action is taken. Nothing is done to ensure
    ///                 the database schema is compatible with the Entity Framework model.
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <description>
    ///                 If the database exists but does not have any tables, then the Entity Framework model is used to
    ///                 create the database schema.
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <description>
    ///                 If the database does not exist, then the database is created and the Entity Framework model is used to
    ///                 create the database schema.
    ///             </description>
    ///         </item>
    ///     </list>
    ///     <para>
    ///         It is common to use <see cref="EnsureCreated" /> immediately following <see cref="EnsureDeleted" /> when
    ///         testing or prototyping using Entity Framework. This ensures that the database is in a clean state before each
    ///         execution of the test/prototype. Note, however, that data in the database is not preserved.
    ///     </para>
    ///     <para>
    ///         Note that this API does **not** use migrations to create the database. In addition, the database that is
    ///         created cannot be later updated using migrations. If you are targeting a relational database and using migrations,
    ///         then you can use <see cref="M:Microsoft.EntityFrameworkCore.RelationalDatabaseFacadeExtensions.Migrate" />
    ///         to ensure the database is created using migrations and that all migrations have been applied.
    ///     </para>
    ///     <para>
    ///         See <see href="https://aka.ms/efcore-docs-manage-schemas">Managing database schemas with EF Core</see>
    ///         and <see href="https://aka.ms/efcore-docs-ensure-created">Database creation APIs</see> for more information and examples.
    ///     </para>
    /// </remarks>
    /// <returns><see langword="true" /> if the database is created, <see langword="false" /> if it already existed.</returns>
    bool EnsureCreated();

    /// <summary>
    ///     Ensures that the database for the context exists.
    /// </summary>
    /// <remarks>
    ///     <list type="bullet">
    ///         <item>
    ///             <description>
    ///                 If the database exists and has any tables, then no action is taken. Nothing is done to ensure
    ///                 the database schema is compatible with the Entity Framework model.
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <description>
    ///                 If the database exists but does not have any tables, then the Entity Framework model is used to
    ///                 create the database schema.
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <description>
    ///                 If the database does not exist, then the database is created and the Entity Framework model is used to
    ///                 create the database schema.
    ///             </description>
    ///         </item>
    ///     </list>
    ///     <para>
    ///         It is common to use <see cref="EnsureCreatedAsync" /> immediately following <see cref="EnsureDeletedAsync" /> when
    ///         testing or prototyping using Entity Framework. This ensures that the database is in a clean state before each
    ///         execution of the test/prototype. Note, however, that data in the database is not preserved.
    ///     </para>
    ///     <para>
    ///         Note that this API does **not** use migrations to create the database. In addition, the database that is
    ///         created cannot be later updated using migrations. If you are targeting a relational database and using migrations,
    ///         then you can use <see cref="M:Microsoft.EntityFrameworkCore.RelationalDatabaseFacadeExtensions.MigrateAsync" />
    ///         to ensure the database is created using migrations and that all migrations have been applied.
    ///     </para>
    ///     <para>
    ///         Entity Framework Core does not support multiple parallel operations being run on the same DbContext instance. This
    ///         includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
    ///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
    ///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see>
    ///         for more information and examples.
    ///     </para>
    ///     <para>
    ///         See <see href="https://aka.ms/efcore-docs-manage-schemas">Managing database schemas with EF Core</see>
    ///         and <see href="https://aka.ms/efcore-docs-ensure-created">Database creation APIs</see> for more information and examples.
    ///     </para>
    /// </remarks>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    ///     A task that represents the asynchronous save operation. The task result contains <see langword="true" /> if the database is created,
    ///     <see langword="false" /> if it already existed.
    /// </returns>
    /// <exception cref="OperationCanceledException">If the <see cref="CancellationToken" /> is canceled.</exception>
    Task<bool> EnsureCreatedAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     <para>
    ///         Ensures that the database for the context does not exist. If it does not exist, no action is taken. If it does
    ///         exist then the database is deleted.
    ///     </para>
    ///     <para>
    ///         Warning: The entire database is deleted, and no effort is made to remove just the database objects that are used by
    ///         the model for this context.
    ///     </para>
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         It is common to use <see cref="EnsureCreated" /> immediately following <see cref="EnsureDeleted" /> when
    ///         testing or prototyping using Entity Framework. This ensures that the database is in a clean state before each
    ///         execution of the test/prototype. Note, however, that data in the database is not preserved.
    ///     </para>
    ///     <para>
    ///         See <see href="https://aka.ms/efcore-docs-manage-schemas">Managing database schemas with EF Core</see>
    ///         and <see href="https://aka.ms/efcore-docs-ensure-created">Database creation APIs</see> for more information and examples.
    ///     </para>
    /// </remarks>
    /// <returns><see langword="true" /> if the database is deleted, <see langword="false" /> if it did not exist.</returns>
    bool EnsureDeleted();

    /// <summary>
    ///     <para>
    ///         Asynchronously ensures that the database for the context does not exist. If it does not exist, no action is taken. If it does
    ///         exist then the database is deleted.
    ///     </para>
    ///     <para>
    ///         Warning: The entire database is deleted, and no effort is made to remove just the database objects that are used by
    ///         the model for this context.
    ///     </para>
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         It is common to use <see cref="EnsureCreatedAsync" /> immediately following <see cref="EnsureDeletedAsync" /> when
    ///         testing or prototyping using Entity Framework. This ensures that the database is in a clean state before each
    ///         execution of the test/prototype. Note, however, that data in the database is not preserved.
    ///     </para>
    ///     <para>
    ///         Entity Framework Core does not support multiple parallel operations being run on the same DbContext instance. This
    ///         includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
    ///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
    ///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see>
    ///         for more information and examples.
    ///     </para>
    ///     <para>
    ///         See <see href="https://aka.ms/efcore-docs-manage-schemas">Managing database schemas with EF Core</see>
    ///         and <see href="https://aka.ms/efcore-docs-ensure-created">Database creation APIs</see> for more information and examples.
    ///     </para>
    /// </remarks>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    ///     A task that represents the asynchronous save operation. The task result contains <see langword="true" /> if the database is deleted,
    ///     <see langword="false" /> if it did not exist.
    /// </returns>
    /// <exception cref="OperationCanceledException">If the <see cref="CancellationToken" /> is canceled.</exception>
    Task<bool> EnsureDeletedAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Determines whether or not the database is available and can be connected to.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Any exceptions thrown when attempting to connect are caught and not propagated to the application.
    ///     </para>
    ///     <para>
    ///         The configured connection string is used to create the connection in the normal way, so all
    ///         configured options such as timeouts are honored.
    ///     </para>
    ///     <para>
    ///         Note that being able to connect to the database does not mean that it is
    ///         up-to-date with regard to schema creation, etc.
    ///     </para>
    ///     <para>
    ///         See <see href="https://aka.ms/efcore-docs-connections">Database connections in EF Core</see> for more information and examples.
    ///     </para>
    /// </remarks>
    /// <returns><see langword="true" /> if the database is available; <see langword="false" /> otherwise.</returns>
    bool CanConnect();

    /// <summary>
    ///     Determines whether or not the database is available and can be connected to.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Any exceptions thrown when attempting to connect are caught and not propagated to the application.
    ///     </para>
    ///     <para>
    ///         The configured connection string is used to create the connection in the normal way, so all
    ///         configured options such as timeouts are honored.
    ///     </para>
    ///     <para>
    ///         Note that being able to connect to the database does not mean that it is
    ///         up-to-date with regard to schema creation, etc.
    ///     </para>
    ///     <para>
    ///         Entity Framework Core does not support multiple parallel operations being run on the same DbContext instance. This
    ///         includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
    ///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
    ///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see>
    ///         for more information and examples.
    ///     </para>
    ///     <para>
    ///         See <see href="https://aka.ms/efcore-docs-connections">Database connections in EF Core</see> for more information and examples.
    ///     </para>
    /// </remarks>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns><see langword="true" /> if the database is available; <see langword="false" /> otherwise.</returns>
    /// <exception cref="OperationCanceledException">If the <see cref="CancellationToken" /> is canceled.</exception>
    Task<bool> CanConnectAsync(CancellationToken cancellationToken = default);
}