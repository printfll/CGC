namespace Microsoft.CGC.DataAccess
{
    using System;
    using System.IO;
    using System.Linq;

    using Microsoft.CGC.DataAccess.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Data context for entities
    /// </summary>
    public class EntityContext : EntityFrameworkCore.DbContext
    {
        /*
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityContext"/> class.
        /// </summary>
        /// <param name="options">DbContextOptions to use</param>
        public EntityContext(DbContextOptions<EntityContext> options)
            : base(options)
        {
        }*/

        static EntityContext()
        {
            string assemblyDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var builder = new ConfigurationBuilder()
                .SetBasePath(assemblyDir)
                .AddJsonFile("config.json");
            var configuration = builder.Build();

            string activeDb = configuration["database:active"];
            ConnectionString = configuration[$"database:{activeDb}:connectionString"];
            if (string.IsNullOrWhiteSpace(ConnectionString))
            {
                throw new Exception($"wrong db connection string \"{ConnectionString}\"");
            }
            Console.WriteLine($"db connection string \"{ConnectionString}\"");
        }

        public static string ConnectionString;

        /// <summary>
        /// Gets or sets data set of tasks
        /// </summary>
        public DbSet<Task> Tasks { get; set; }

        /// <summary>
        /// Gets or sets data set of modules
        /// </summary>
        public DbSet<Module> Modules { get; set; }

        /// <summary>
        /// Gets or sets data set of jobs
        /// </summary>
        public DbSet<Job> Jobs { get; set; }

        #region User & Permission Management
        /// <summary>
        /// Gets or sets accounts data set
        /// </summary>
        public DbSet<Account> Accounts { get; set; }

        /// <summary>
        /// Gets or sets users data set
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets roles data set
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Gets or sets rights data set
        /// </summary>
        public DbSet<Permission> Permissions { get; set; }

        /// <summary>
        /// Gets or sets group data set
        /// </summary>
        public DbSet<Group> Groups { get; set; }

        /// <summary>
        /// Gets or sets UserGroupMappings
        /// </summary>
        public DbSet<GroupUserMapping> GroupUserMappings { get; set; }

        /// <summary>
        /// Gets or sets RoleRightMappings
        /// </summary>
        public DbSet<RolePermissionMapping> RolePermissionMappings { get; set; }

        /// <summary>
        /// Gets or sets ResourcePermissions
        /// </summary>
        public DbSet<ResourcePermission> ResourcePermissions { get; set; }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {         
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        /// <inheritdoc />
        public override int SaveChanges()
        {
            this.ChangeTracker.DetectChanges();

            if (this.ChangeTracker.HasChanges())
            {
                var addEntities = this.ChangeTracker.Entries<Entity>().Where(e => e.State == EntityState.Added);

                foreach (var e in addEntities)
                {
                    e.Entity.CreatedOn = DateTime.Now;
                }

                var updatedEntities = this.ChangeTracker.Entries<Entity>().Where(e => e.State == EntityState.Modified);

                foreach (var e in updatedEntities)
                {
                    e.Entity.ModifiedOn = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // set up indexes here
            modelBuilder.Entity<Account>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<Permission>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<Role>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<Job>()
                .HasIndex(x => x.CreatedBy);

            modelBuilder.Entity<GroupUserMapping>()
                .HasIndex(x => x.IsOwner);

            modelBuilder.Entity<GroupUserMapping>()
                .HasOne(x => x.Group)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<GroupUserMapping>()
                .HasOne(x => x.Member)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull);

            /*
            // These collections are used in EE standard mapping
            // EE core cannot recognize these navigation properties
            modelBuilder.Entity<Group>(
                entity =>
                    {
                        entity.Ignore(e => e.ParentGroups);
                        entity.Ignore(e => e.ChildGroups);
                        entity.Ignore(e => e.Users);
                        entity.Ignore(e => e.Groups);
                    });

            modelBuilder.Entity<Role>(
                entity => { entity.Ignore(e => e.Rights); });

            modelBuilder.Entity<Permission>(
                entity => { entity.Ignore(e => e.Roles); });

            // Add mappings for EE core models
            modelBuilder.Entity<GroupMapping>(entity =>
            {
                entity.HasKey(e => new { e.ParentGroupId, e.ChildGroupId });

                entity.HasIndex(e => e.ChildGroupId)
                    .HasName("IX_ChildGroupId");

                entity.HasIndex(e => e.ParentGroupId)
                    .HasName("IX_ParentGroupId");

                entity.HasOne(d => d.ChildGroup)
                    .WithMany(p => p.GroupMappingsChildGroup)
                    .HasForeignKey(d => d.ChildGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.GroupMappings_dbo.Groups_ChildGroupId");

                entity.HasOne(d => d.ParentGroup)
                    .WithMany(p => p.GroupMappingsParentGroup)
                    .HasForeignKey(d => d.ParentGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.GroupMappings_dbo.Groups_ParentGroupId");
            });

            modelBuilder.Entity<ResourcePermission>(entity =>
            {
                entity.HasIndex(e => e.GroupId)
                    .HasName("IX_GroupId");

                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_RoleId");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserId");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.ResourcePermissions)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_dbo.ResourcePermissions_dbo.Groups_GroupId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.ResourcePermissions)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_dbo.ResourcePermissions_dbo.Roles_RoleId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ResourcePermissions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.ResourcePermissions_dbo.Users_UserId");
            });

            modelBuilder.Entity<RolePermissionMapping>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.RightId });

                entity.HasIndex(e => e.RightId)
                    .HasName("IX_RightId");

                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_RoleId");

                entity.HasOne(d => d.Right)
                    .WithMany(p => p.RoleRightMappings)
                    .HasForeignKey(d => d.RightId)
                    .HasConstraintName("FK_dbo.RoleRightMappings_dbo.Rights_RightId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleRightMappings)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_dbo.RoleRightMappings_dbo.Roles_RoleId");
            });

            modelBuilder.Entity<GroupUserMapping>(entity =>
            {
                entity.HasKey(e => new { e.GroupId, e.UserId });

                entity.HasIndex(e => e.GroupId)
                    .HasName("IX_GroupId");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserId");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.UserGroupMappings)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_dbo.UserGroupMappings_dbo.Groups_GroupId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserGroupMappings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.UserGroupMappings_dbo.Users_UserId");
            });
            */
        }
    }
}
