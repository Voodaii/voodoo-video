using Microsoft.AspNetCore.Hosting;
using Voodoo.Video.Areas.Identity;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]
namespace Voodoo.Video.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}