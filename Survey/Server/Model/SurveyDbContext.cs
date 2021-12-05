using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Survey.Shared.Model;
using Survey.Shared.Model.Comment;
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
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //modelBuilder
        //    //    .Entity<BoardModel>()
        //    //    .HasOne(e => e.Replies)
        //    //    .OnDelete(DeleteBehavior.ClientCascade);
        //    //modelBuilder.Entity<CardModel>()
        //    //    .HasMany(x => x.Replies)
        //    //    .WithOne()
        //    //    .OnDelete(DeleteBehavior.ClientCascade);

        //    //modelBuilder.Entity<BoardModel>()
        //    //    .HasMany(x => x.Cards)
        //    //    .WithOne();
        //    //.OnDelete(DeleteBehavior.Cascade);

        //    modelBuilder.Entity<CardModel>()
        //        .HasMany(x => x.Replies)
        //        .WithOne()
        //        .OnDelete(DeleteBehavior.Cascade);

        //    //modelBuilder.Entity<Reply>()
        //    //    .HasMany(x => x.Replies)
        //    //    .WithOne()
        //    //.OnDelete(DeleteBehavior.Cascade);

        //    //modelBuilder.Entity<Reply>()
        //    //    .HasMany(x => x.Replies)
        //    //    .WithOne()
        //    //    .OnDelete(DeleteBehavior.Cascade);

        //    base.OnModelCreating(modelBuilder);
        //}
        public DbSet<CardModel> CardModel => Set<CardModel>();
        public DbSet<BoardModel> BoardModel => Set<BoardModel>();
        public DbSet<BoardFiller> BoardFillers => Set<BoardFiller>();

    }


}
