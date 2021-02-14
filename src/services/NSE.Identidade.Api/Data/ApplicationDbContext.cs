using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NSE.Identidade.Api.Data
{
    //Install-Package Microsoft.EntityFrameworkCore.Tools -version 3.1.3
    //Add-Migration Initial
    //Update-Database
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
