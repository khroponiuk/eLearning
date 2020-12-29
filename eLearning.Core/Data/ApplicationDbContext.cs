using eLearning.Core.Entities;
using eLearning.Core.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace eLearning.Core.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<Graph> Graphs { get; set; }
        public DbSet<GraphNode> GraphNodes { get; set; }
        public DbSet<GraphEdge> GraphEdges { get; set; }

        public DbSet<CourseTheme> CourseThemes { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Lab> Labs { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }


        //#public DbSet<GraphNodeConfiguration> GraphNodeConfigurations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Seed();

            ConfigureTableMapings(builder);
            ConfigureEntityRelations(builder);
        }

        protected void ConfigureTableMapings(ModelBuilder builder)
        {
            builder.Entity<Graph>().ToTable("Graphs");
            builder.Entity<GraphNode>().ToTable("GraphNodes");
            builder.Entity<GraphEdge>().ToTable("GraphEdges");

            builder.Entity<CourseTheme>().ToTable("CourseThemes");
            builder.Entity<Lecture>().ToTable("Lectures");
            builder.Entity<Lab>().ToTable("Labs");
            builder.Entity<Quiz>().ToTable("Quizzes");

            builder.Entity<GraphNodeConfiguration>().ToTable("GraphNodeConfigurations");

        }

        protected void ConfigureEntityRelations(ModelBuilder builder)
        {
            builder.Entity<Graph>()
                .HasMany(x => x.Nodes)
                .WithOne(x => x.Graph);

            builder.Entity<Graph>()
                .HasMany(x => x.Edges)
                .WithOne(x => x.Graph);

            builder.Entity<CourseTheme>()
               .HasOne(x => x.Lecture)
               .WithOne(x => x.CourseTheme)
               .HasForeignKey<Lecture>(x => x.CourseThemeId);

            builder.Entity<CourseTheme>()
                .HasOne(x => x.Lab)
                .WithOne(x => x.CourseTheme)
                .HasForeignKey<Lab>(x => x.CourseThemeId);

            builder.Entity<CourseTheme>()
                .HasOne(x => x.Quiz)
                .WithOne(x => x.CourseTheme)
                .HasForeignKey<Quiz>(x => x.CourseThemeId);
        }
    }
}
