using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
    }
}
