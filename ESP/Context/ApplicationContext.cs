using ESP.Models;
using Microsoft.EntityFrameworkCore;

namespace ESP.Context
{
    public class ApplicationContext : DbContext
    {

        public DbSet<Block> Blocks { get; set; } = null!;
        public DbSet<SubjectType> SubjectTypes { get; set; } = null!;
        public DbSet<CheckBlock> CheckBlocks { get; set; } = null!;
        public DbSet<CheckCode> CheckCodes { get; set; } = null!;
        public DbSet<ProhibitionCode> ProhibitionCodes { get; set; } = null!;
        public DbSet<Process> Processes { get; set; } = null!;
        public DbSet<ClientType> ClientTypes { get; set; } = null!;
        public DbSet<SystemType> SystemTypes { get; set; } = null!;
        public DbSet<SystemBlock> SystemBlocks { get; set; } = null!;  
        public DbSet<Models.Route> Routes { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CheckBlock>(entity =>
            {

                entity.HasMany(x => x.SubjectTypes)
                      .WithMany(x => x.CheckBlocks)
                      .UsingEntity(x => x.ToTable("CheckBlocksAndSubjectTypes"));

                entity.HasMany(x => x.CheckCodes)
                      .WithMany(x => x.CheckBlocks)
                      .UsingEntity(x => x.ToTable("CheckBlocksAndCheckCodes"));

                entity.HasOne(x => x.Block)
                      .WithMany(x => x.CheckBlocks)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Process>(entity =>
            {
                
                entity.HasMany(x => x.CheckBlocks)
                      .WithMany(x => x.Processes)
                      .UsingEntity(x => x.ToTable("ProcessesAndCheckBlocks"));

                entity.HasMany(x => x.CheckCodes)
                      .WithMany(x => x.Processes)
                      .UsingEntity(x => x.ToTable("ProcessesAndCheckCodes"));

                entity.HasMany(x => x.ProhibitionCodes)
                      .WithMany(x => x.Processes)
                      .UsingEntity(x => x.ToTable("ProcessesAndProhibitionCodes"));
                
                entity.HasMany(x => x.SubjectTypes)
                     .WithMany(x => x.Processes)
                     .UsingEntity(x => x.ToTable("ProcessesAndSubjectTypes"));

            });


            modelBuilder.Entity<SubjectType>(entity =>
            {
                entity.HasMany(x => x.ClientTypes)
                      .WithMany(x => x.SubjectTypes)
                      .UsingEntity(x => x.ToTable("SubjectTypesAndClientTypes"));
            });

            modelBuilder.Entity<Models.Route>(entity =>
            {
               
                entity.HasMany(x => x.CheckCodes)
                      .WithMany(x => x.Routes)
                      .UsingEntity(x => x.ToTable("RoutesAndCheckCodes"));

                entity.HasMany(x => x.ProhibitionCodes)
                      .WithMany(x => x.Routes)
                      .UsingEntity(x => x.ToTable("RoutesAndProhibitionCodes"));
            });

        }
    }
}
