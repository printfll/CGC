namespace Microsoft.CGC.DataAccess.DotNetStd.DataContext
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using Microsoft.CGC.DataAccess.Models;
    using Microsoft.CGC.DataAccess.DotNetStd.Migrations;

    public class EntityContext : DbContext
    {
        public EntityContext() : base("CGCProd")
        {
        }

        public EntityContext(string connectionString) : 
            base(connectionString)
        {
        }

        public DbSet<Experiment> Experiments { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<ResourcePermission> ResourcePermissions { get; set; }
        public DbSet<Right> Rights { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }    
        
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


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>()
              .HasMany(e => e.ParentGroups)
              .WithMany(e => e.ChildGroups)
              .Map(m => m.ToTable("GroupMappings").MapLeftKey("ChildGroupId").MapRightKey("ParentGroupId"));

            modelBuilder.Entity<Group>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Groups)
                .Map(m => m.ToTable("UserGroupMappings").MapLeftKey("GroupId").MapRightKey("UserId"));

            modelBuilder.Entity<Right>()
                .HasMany(e => e.Roles)
                .WithMany(e => e.Rights)
                .Map(m => m.ToTable("RoleRightMappings").MapLeftKey("RightId").MapRightKey("RoleId"));
        }
    }
}
