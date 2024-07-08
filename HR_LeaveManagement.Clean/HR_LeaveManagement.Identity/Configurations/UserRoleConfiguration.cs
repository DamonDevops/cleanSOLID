using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_LeaveManagement.Identity.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                RoleId = "5a826ee2-b320-47f2-84f6-bc985e037bd6",
                UserId = "d33faa83-1813-4c33-8230-49e284091b43"
            },
            new IdentityUserRole<string>
            {
                RoleId = "ab651914-7c7a-4568-8c41-b9bd1e00f7f6",
                UserId = "8fc98501-ff86-4156-b938-275ae7518667"
            }
            );
    }
}
