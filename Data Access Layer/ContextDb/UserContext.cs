using Db_Builder.Models.User_Manager;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.ContextDb
{
    public class UserContext : IdentityDbContext<AppUser>
    {
        public UserContext()
        {
                    
        }

        public UserContext( DbContextOptions<UserContext> options) :base(options)
        {
                
        }

        public DbSet<DegreeState> DegreeStates { set;get; }

        
    }
}
