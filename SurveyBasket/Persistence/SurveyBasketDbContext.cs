using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SurveyBasket.Entities;
using SurveyBasket.Persistence.EntitiesConfigurations;

namespace SurveyBasket.Persistence;

public class SurveyBasketDbContext(DbContextOptions<SurveyBasketDbContext> options)
        : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Poll> Polls { get; set; }

    /// <summary>
    /// Automatically applies all entity configurations found in the current assembly.
    /// </summary>
    /// <remarks>
    /// Instead of manually applying each configuration like:
    /// <code>
    /// modelBuilder.ApplyConfiguration(new PollConfiguration());
    /// </code>
    /// This approach scans the current assembly and applies all classes implementing
    /// <c>IEntityTypeConfiguration&lt;T&gt;</c> to the model.
    /// </remarks>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    /*
     * Instead of registering each entity configuration manually like:
     *     modelBuilder.ApplyConfiguration(new PollConfiguration());
     *
     * We use the following line to automatically scan the current assembly,
     * find all classes that implement IEntityTypeConfiguration<T>,
     * and apply their configurations to the ModelBuilder.
     */
}