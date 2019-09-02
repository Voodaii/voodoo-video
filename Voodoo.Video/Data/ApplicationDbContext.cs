using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Voodoo.Video.Models.Application;
using Voodoo.Video.Models.Identity;

namespace Voodoo.Video.Data
{
    // interface IApplicationDbContext : IDataProtectionKeyContext
    // {
    //     int SaveChanges();
    //     Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    // }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IDataProtectionKeyContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
            MigrateDatabase();
        }

        private void MigrateDatabase()
        {
            if (Database.GetPendingMigrations().Any())
            {
                Database.Migrate();
            }
        }

        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
        public DbSet<CameraLayout> CameraLayouts {get;set;}
        public DbSet<Camera> Cameras {get;set;}
        public DbSet<CameraTile> CameraTiles {get;set;}
    }
}
