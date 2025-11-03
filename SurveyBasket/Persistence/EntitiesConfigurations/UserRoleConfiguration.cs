using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyBasket.Abstractions.Consts;

namespace SurveyBasket.Persistence.EntitiesConfigurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        //Default Data
        builder.HasData(new IdentityUserRole<string>
        {
            UserId = DefaultUsers.AdminId,
            RoleId = DefaultRoles.AdminRoleId
        });
    }
}