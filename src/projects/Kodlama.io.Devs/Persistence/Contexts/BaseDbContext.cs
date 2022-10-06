using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Auths.Helpers;
using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<GithubSocial> GithubSocials { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                base.OnConfiguring(
                    optionsBuilder.UseSqlServer(Configuration.GetConnectionString("AdvancedCampConnectionString")));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProgrammingLanguage>(a =>
            {
                a.ToTable("ProgrammingLanguages").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
                a.HasMany(p => p.Technologies);
            });

            modelBuilder.Entity<Technology>(a =>
            {
                a.ToTable("Technologies").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
                a.Property(p => p.ProgrammingLanguageId).HasColumnName("ProgrammingLanguageId");
                a.HasOne(p => p.ProgrammingLanguage);
            });

            modelBuilder.Entity<GithubSocial>(a =>
            {
                a.ToTable("GithubSocials").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.GithubUrl).HasColumnName("GithubUrl");
                a.Property(p => p.UserId).HasColumnName("UserId");
                a.HasOne(p => p.User);
            });

            ProgrammingLanguage[] programmingLanguageEntitySeeds =
            {
                new(1, "C#"),
                new(2, "Java")
            };
            modelBuilder.Entity<ProgrammingLanguage>().HasData(programmingLanguageEntitySeeds);

            Technology[] technologyEntitySeeds =
            {
                new(1, "WPF", 1, true),
                new(2, "ASP.NET", 1, true),
                new(3, "JSP", 2, true),
            };
            modelBuilder.Entity<Technology>().HasData(technologyEntitySeeds);

            OperationClaim[] operationClaimEntitySeeds =
            {
                new(1, RoleTypes.Admin.ToString()),
                new(2, RoleTypes.Moderator.ToString()),
                new(3, RoleTypes.User.ToString()),
                
            };
            modelBuilder.Entity<OperationClaim>().HasData(operationClaimEntitySeeds);
        }
    }
}
