#region "Libraries"

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace MPIS.User.RepositoryModel.Config
{
    public class UserConfiguration
    {
        public UserConfiguration(EntityTypeBuilder<User.DomainModel.User> modelBuilder, string schema)
        {
            modelBuilder.ToTable("Users", schema);
            modelBuilder.HasIndex(x => x.Id);

            /*modelBuilder
               .HasMany(x => x.Records)
               .WithOne(x => x.User)
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Restrict);*/

        }
    }
}
