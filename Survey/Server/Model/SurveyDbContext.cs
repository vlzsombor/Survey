using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Survey.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Survey.Server.Model 
{
    public class SurveyDbContext : IdentityDbContext
    {
        public SurveyDbContext(DbContextOptions<SurveyDbContext> options) : base(options)
        {
            // Database.EnsureDeleted();
            // Database.EnsureCreated();
            // caused There is already an object named 'AspNetRoles' in the database
        }

        public DbSet<CardModel> CardModel => Set<CardModel>();
        public DbSet<BoardModel> BoardModel => Set<BoardModel>();
        public DbSet<BoardFiller> BoardFillers => Set<BoardFiller>();

    }


}
