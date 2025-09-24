// Copyright (c) 2023 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using AuthPermissions.BaseCode.DataLayer.Classes;
using AuthPermissions.BaseCode.DataLayer.Classes.SupportTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AuthPermissions.BaseCode.DataLayer.EfCode
{
    /// <summary>
    /// This forms the AuthP's EF Core database
    /// </summary>
    public class AuthPermissionsDbContext : DbContext
    {
        private readonly ICustomConfiguration _customConfiguration;

        /// <summary>
        /// The list of central customer accounts
        /// </summary>
        public DbSet<CustomerAccount> CustomerAccounts { get; set; }

        /// <summary>
        /// Links customer accounts to tenants
        /// </summary>
        public DbSet<CustomerTenantLink> CustomerTenantLinks { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="options"></param>
        /// <param name="eventSetups">OPTIONAL: If provided, then a method will be run within the ctor</param>
        /// <param name="customConfiguration">OPTIONAL: This allows to provide a custom configuration to the DbContext</param>
        public AuthPermissionsDbContext(DbContextOptions<AuthPermissionsDbContext> options,
            IEnumerable<IDatabaseStateChangeEvent> eventSetups = null,
            ICustomConfiguration customConfiguration = null)
            : base(options)
        {
            foreach (var eventSetup in eventSetups ?? Array.Empty<IDatabaseStateChangeEvent>())
            {
                eventSetup.RegisterEventHandlers(this);
            }
            ProviderName = Database.ProviderName;
            _customConfiguration = customConfiguration;
        }

        /// <summary>
        /// This is needed for EF Core 9 and above  when building a multi-tenant application.
        /// This allows you to add more than one migration on this database 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(x => x.Ignore(RelationalEventId.PendingModelChangesWarning));
            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// This overcomes the exception if the class used in the tests which uses the <see cref="IModelCacheKeyFactory"/>
        /// to allow testing of an DbContext that works with SqlServer and PostgreSQL 
        /// </summary>
        public string ProviderName { get; }

        /// <summary>
        /// The list of AuthUsers defining what roles and tenant that user has
        /// </summary>
        public DbSet<AuthUser> AuthUsers { get; set; }

        /// <summary>
        /// A list of all the AuthP's Roles, each with the permissions in each Role
        /// </summary>
        public DbSet<RoleToPermissions> RoleToPermissions { get; set; }

        /// <summary>
        /// When using AuthP's multi-tenant feature these define each tenant and the DataKey to access data in that tenant
        /// </summary>
        public DbSet<Tenant> Tenants { get; set; }

        /// <summary>
        /// This links AuthP's Roles to a AuthUser
        /// </summary>
        public DbSet<UserToRole> UserToRoles { get; set; }

        /// <summary>
        /// If you use AuthP's JWT refresh token, then the tokens are held in this entity
        /// </summary>
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        /// <summary>
        /// This holds the backup set of <see cref="ShardingEntry"/>'s held in the FileStore cache
        /// </summary>
        public DbSet<ShardingEntry> ShardingEntryBackup { get; set; }

        public DbSet<Plan> Plans { get; set; }

        public DbSet<TenantPlan> TenantPlans { get; set; }

        /// <summary>
        /// Set up AuthP's setup
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("authp");

            //Add concurrency token to every entity 
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (Database.IsSqlServer())
                {
                    entityType.AddProperty("ConcurrencyToken", typeof(byte[]))
                        .SetColumnType("ROWVERSION");
                    entityType.FindProperty("ConcurrencyToken")
                        .ValueGenerated = ValueGenerated.OnAddOrUpdate;
                    entityType.FindProperty("ConcurrencyToken")
                        .IsConcurrencyToken = true;
                }
                else if (Database.IsNpgsql())
                {
                    entityType.AddProperty("xmin", typeof(uint))
                        .SetColumnType("xid");
                    entityType.FindProperty("xmin")
                        .ValueGenerated = ValueGenerated.OnAddOrUpdate;
                    entityType.FindProperty("xmin")
                        .IsConcurrencyToken = true;
                }
                //NOTE: Sqlite doesn't support concurrency support, but if needed it can be added
                //see https://www.bricelam.net/2020/08/07/sqlite-and-efcore-concurrency-tokens.html
            }

            //This allows a developer to add a custom configuration to this DbContext
            //Typical use is to set up the concurrency tokens parts when using a custom database type  
            _customConfiguration?.ApplyCustomConfiguration(modelBuilder);

            modelBuilder.Entity<AuthUser>()
                .HasIndex(x => x.Email)
                .IsUnique();
            modelBuilder.Entity<AuthUser>()
                .HasIndex(x => x.UserName)
                .IsUnique();

            modelBuilder.Entity<AuthUser>()
                .HasMany(x => x.UserRoles)
                .WithOne()
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<RoleToPermissions>()
                .HasIndex(x => x.RoleType);

            modelBuilder.Entity<RoleToPermissions>()
                .HasKey(x => x.RoleId);

            // Who created the role (optional FK to Tenant)
            modelBuilder.Entity<RoleToPermissions>()
                .HasOne(x => x.CreatedByTenant)
                .WithMany()
                .HasForeignKey(x => x.CreatedByTenantId)
                .OnDelete(DeleteBehavior.NoAction);

            // Unique role name per creator-tenant (global roles have CreatedByTenantId = null)
            modelBuilder.Entity<RoleToPermissions>()
                .HasIndex(x => new { x.RoleName, x.CreatedByTenantId })
                .IsUnique();

            modelBuilder.Entity<UserToRole>()
                .HasKey(x => new { x.UserId, x.RoleId });

            modelBuilder.Entity<Tenant>().HasKey(x => x.TenantId);
            modelBuilder.Entity<Tenant>()
                .HasIndex(x => x.TenantFullName)
                .IsUnique();
            modelBuilder.Entity<Tenant>()
                .Property(x => x.ParentDataKey)
                .IsUnicode(false);
            modelBuilder.Entity<Tenant>()
                .HasIndex(x => x.ParentDataKey);

            modelBuilder.Entity<Tenant>()
                .HasMany(x => x.TenantRoles)
                .WithMany(x => x.Tenants);

            modelBuilder.Entity<RefreshToken>()
                .Property(x => x.TokenValue)
                .IsUnicode(false)
                .HasMaxLength(AuthDbConstants.RefreshTokenValueSize)
                .IsRequired();

            modelBuilder.Entity<RefreshToken>()
                .HasKey(x => x.TokenValue);

            modelBuilder.Entity<RefreshToken>()
                .HasIndex(x => x.AddedDateUtc)
                .IsUnique();

            modelBuilder.Entity<ShardingEntry>()
                .HasKey(x => x.Name);

            modelBuilder.Entity<ShardingEntry>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<Plan>()
                .HasIndex(x => x.Name)
                .IsUnique();
            modelBuilder.Entity<Plan>()
                .HasKey(x => x.Id);


            modelBuilder.Entity<TenantPlan>()
                .HasIndex(x => new { x.TenentId, x.IsActive });
            modelBuilder.Entity<TenantPlan>()
                .HasKey(x => x.Id);


            // Plan <-> RoleToPermissions (many-to-many via join table authp.PlanToRoles)
            modelBuilder.Entity<Plan>()
                .HasMany(p => p.Roles)
                .WithMany() // no navigation on RoleToPermissions needed
                .UsingEntity<Dictionary<string, object>>(
                    "PlanToRoles",
                    j => j
                        .HasOne<RoleToPermissions>()
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .HasPrincipalKey(r => r.RoleId)
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<Plan>()
                        .WithMany()
                        .HasForeignKey("PlanId")
                        .HasPrincipalKey(p => p.Id)
                        .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.ToTable("PlanToRoles", "authp");
                        j.HasKey("PlanId", "RoleId");
                        j.HasIndex("RoleId");
                    });

            // TenantPlans (many plans can be assigned to one tenant; only one active at a time)
            modelBuilder.Entity<TenantPlan>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<TenantPlan>()
                .HasOne(tp => tp.Tenant)
                .WithMany() // optional: add ICollection<TenantPlan> to Tenant if you need reverse nav
                .HasForeignKey(tp => tp.TenentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TenantPlan>()
                .HasOne(tp => tp.Plan)
                .WithMany(p => p.TenantPlans)
                .HasForeignKey(tp => tp.PlanId)
                .OnDelete(DeleteBehavior.Cascade);

            // Helpful non-unique indexes
            modelBuilder.Entity<TenantPlan>()
                .HasIndex(tp => tp.TenentId);
            modelBuilder.Entity<TenantPlan>()
                .HasIndex(tp => tp.PlanId);

            // Unique filtered index: only one active plan per tenant
            if (Database.IsSqlServer())
            {
                modelBuilder.Entity<TenantPlan>()
                    .HasIndex(tp => tp.TenentId)
                    .IsUnique()
                    .HasFilter("[IsActive] = 1");
            }
            else if (Database.IsNpgsql())
            {
                modelBuilder.Entity<TenantPlan>()
                    .HasIndex(tp => tp.TenentId)
                    .IsUnique()
                    .HasFilter("\"IsActive\" = true");
            }
            else
            {
                // Callback (no filtering support): keep existing composite index for performance
                modelBuilder.Entity<TenantPlan>()
                    .HasIndex(x => new { x.TenentId, x.IsActive });
            }

            // TenantPlan <-> RoleToPermissions (many-to-many via join table authp.TenantPlanRoles)
            modelBuilder.Entity<TenantPlan>()
                .HasMany(tp => tp.Roles)
                .WithMany() // no navigation on RoleToPermissions needed
                .UsingEntity<Dictionary<string, object>>(
                    "TenantPlanRoles",
                    j => j
                        .HasOne<RoleToPermissions>()
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .HasPrincipalKey(r => r.RoleId)
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<TenantPlan>()
                        .WithMany()
                        .HasForeignKey("TenantPlanId")
                        .HasPrincipalKey(tp => tp.Id)
                        .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.ToTable("TenantPlanRoles", "authp");
                        j.HasKey("TenantPlanId", "RoleId");
                        j.HasIndex("RoleId");
                    });

            // Customers
            modelBuilder.Entity<CustomerAccount>()
                .HasKey(x => x.GlobalCustomerId);
            modelBuilder.Entity<CustomerAccount>()
                .HasIndex(x => x.GlobalUserId)
                .IsUnique();
            modelBuilder.Entity<CustomerAccount>()
                .HasIndex(x => x.PhoneNumber)
                .IsUnique();

            modelBuilder.Entity<CustomerTenantLink>()
                .HasKey(x => x.CustomerTenantLinkId);
            modelBuilder.Entity<CustomerTenantLink>()
                .HasIndex(x => new { x.GlobalCustomerId, x.TenantId })
                .IsUnique();
            modelBuilder.Entity<CustomerTenantLink>()
                .HasIndex(x => x.TenantId);
            modelBuilder.Entity<CustomerTenantLink>()
                .HasOne(x => x.Customer)
                .WithMany(x => x.TenantLinks)
                .HasForeignKey(x => x.GlobalCustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
