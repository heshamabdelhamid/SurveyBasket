using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyBasket.Abstractions.Consts;
using SurveyBasket.Entities;

namespace SurveyBasket.Persistence.EntitiesConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.OwnsMany(x => x.RefreshTokens)
            .ToTable("RefreshTokens") // this to change the table name, you can remove this line if you want to use the default table name
            .WithOwner()
            .HasForeignKey("UserId"); // this to change the foreign key name, you can remove this line if you want to use the default foreign key name

        builder.Property(x => x.FirstName)
            .HasMaxLength(100);
        
        builder.Property(x => x.LastName)
            .HasMaxLength(100);

        builder.HasData(new ApplicationUser
        {
            Id = DefaultUsers.AdminId,
            FirstName = "Survey Basket",
            LastName = "Admin",
            UserName = DefaultUsers.AdminEmail,
            NormalizedUserName = DefaultUsers.AdminEmail.ToUpper(),
            Email = DefaultUsers.AdminEmail,
            NormalizedEmail = DefaultUsers.AdminEmail.ToUpper(),
            SecurityStamp = DefaultUsers.AdminSecurityStamp,
            ConcurrencyStamp = DefaultUsers.AdminConcurrencyStamp,
            EmailConfirmed = true,
            PasswordHash = DefaultUsers.AdminPassword
        });
    }
}
