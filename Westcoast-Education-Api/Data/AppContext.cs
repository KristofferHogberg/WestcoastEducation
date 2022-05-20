using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Westcoast_Education_Api.Data
{
    public class AppContext : IdentityDbContext
    {
        public AppContext(DbContextOptions options) : base(options)
        {
        }
    }
}