namespace Ayllu.Infrastructure.Persistence.Data;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>(options)
{
    public DbSet<Antithesis> Antitheses => Set<Antithesis>();
    public DbSet<Dialectic> Dialectics => Set<Dialectic>();
    public DbSet<ApplicationGroup> Groups => Set<ApplicationGroup>();
    public DbSet<ApplicationGroupDialectic> GroupDialectics => Set<ApplicationGroupDialectic>();
    public DbSet<Synthesis> Syntheses => Set<Synthesis>();
    public DbSet<Thesis> Theses => Set<Thesis>();
    public DbSet<ApplicationUserFriendship> UserFriendships => Set<ApplicationUserFriendship>();
    public DbSet<ApplicationUserGroup> UserGroups => Set<ApplicationUserGroup>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // 🔒 Global filter: ignora soft-deleted
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (typeof(ISoftDeleteEntity).IsAssignableFrom(entityType.ClrType))
            {
                var filter = CreateIsDeletedFilter(entityType.ClrType);
                builder.Entity(entityType.ClrType).HasQueryFilter(filter);
            }
        }

        builder.Entity<ApplicationGroup>(entity =>
        {
            entity.ToTable("Groups");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Description).IsRequired(false);
            entity.HasOne(g => g.Creator)
                .WithMany(u => u.CreatedGroups)
                .HasForeignKey(g => g.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasMany(g => g.Dialectics)
                .WithOne(gd => gd.Group)
                .HasForeignKey(gd => gd.GroupId);
        });

        builder.Entity<ApplicationGroupDialectic>(entity =>
        {
            entity.HasKey(gd => gd.Id);
            entity.HasOne(gd => gd.Dialectic)
                .WithOne(d => d.GroupContext)
                .HasForeignKey<ApplicationGroupDialectic>(gd => gd.DialecticId);
            entity.HasOne(gd => gd.CreatedBy)
                .WithMany(u => u.GroupDialecticsCreated)
                .HasForeignKey(gd => gd.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Antithesis>(entity =>
        {
            entity.ToTable("Antitheses");
            entity.HasKey(a => a.Id);

            entity.HasOne<ApplicationUser>()
                .WithMany(u => u.AuthoredAntitheses)
                .HasForeignKey(a => a.AuthorUserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Dialectic>(entity =>
        {
            entity.ToTable("Dialectics");
            entity.HasKey(d => d.Id);
            entity.HasIndex(d => new { d.OwnerUserId, d.Title }).IsUnique(true).HasDatabaseName("UIX_OUI_T");

            entity.HasOne(d => d.OwnerUser)
                  .WithMany(u => u.OwnedDialectics)
                  .HasForeignKey(d => d.OwnerUserId)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.Thesis)
                .WithOne(t => t.Dialectic)
                .HasForeignKey<Thesis>(t => t.DialecticId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(d => d.Antitheses)
                .WithOne(a => a.Dialectic)
                .HasForeignKey(a => a.DialecticId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(d => d.Syntheses)
                .WithOne(s => s.Dialectic)
                .HasForeignKey(s => s.DialecticId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Synthesis>(entity =>
        {
            entity.ToTable("Syntheses");
            entity.HasKey(d => d.Id);
            entity.HasIndex(d => new { d.DialecticId, d.AuthorUserId })
                .IsUnique(true)
                .HasDatabaseName("UIX_DI_AUI");

            entity.HasOne(s => s.AuthorUser)
                .WithMany(u => u.AuthoredSyntheses)
                .HasForeignKey(s => s.AuthorUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(s => s.Dialectic)
                .WithMany(d => d.Syntheses)
                .HasForeignKey(s => s.DialecticId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(s => s.Antithesis)
                .WithMany()
                .HasForeignKey(s => s.AntithesisId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(s => s.Thesis)
                .WithMany()
                .HasForeignKey(s => s.ThesisId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Thesis>(entity =>
        {
            entity.ToTable("Theses");
            entity.HasKey(t => t.Id);

            entity.HasOne<ApplicationUser>()
                .WithMany(u => u.AuthoredTheses)
                .HasForeignKey(t => t.AuthorUserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<IdentityUserClaim<string>>(entity =>
        {
            entity.ToTable("UserClaims");
        });

        builder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.ToTable("UserLogins");
        });

        builder.Entity<IdentityRoleClaim<string>>(entity =>
        {
            entity.ToTable("RoleClaims");
        });

        builder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.ToTable("UserTokens");
        });

        builder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable("Users");

            entity.HasKey(e => e.Id);

            entity.HasMany(e => e.FriendshipsInitiated)
                .WithOne(f => f.UserA)
                .HasForeignKey(f => f.UserAId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(e => e.FriendshipsReceived)
                .WithOne(f => f.UserB)
                .HasForeignKey(f => f.UserBId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<ApplicationRole>(entity =>
        {
            entity.ToTable("Roles");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).IsRequired();
            entity.Property(e => e.Name).IsRequired();
            entity.HasIndex(e => e.Name).IsUnique().HasDatabaseName("IX_ROLENAME");
        });

        builder.Entity<ApplicationUserRole>(entity =>
        {
            entity.ToTable("UserRoles");
            entity.HasKey(x => new { x.UserId, x.RoleId });
            entity.HasIndex(x => new { x.RoleId, x.UserId }).IsUnique().HasDatabaseName("IX_RID_UID");
            entity.HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne<ApplicationRole>()
                .WithMany()
                .HasForeignKey(x => x.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<ApplicationUserFriendship>(entity =>
        {
            entity.ToTable("Friendships");

            entity.HasKey(e => new { e.UserAId, e.UserBId });

            entity.HasIndex(e => new { e.UserAId, e.UserBId })
                  .IsUnique()
                  .HasDatabaseName("IX_UA_UB");

            entity.Property(e => e.Status)
                  .IsRequired()
                  .HasDefaultValue(UserFriendshipStatus.PENDING)
                  .HasConversion<int>(); // Garante que será salvo como INT no banco

            entity.HasOne(e => e.UserA)
                .WithMany(u => u.FriendshipsInitiated)
                .HasForeignKey(e => e.UserAId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.UserB)
                .WithMany(u => u.FriendshipsReceived)
                .HasForeignKey(e => e.UserBId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<ApplicationUserGroup>(entity =>
        {
            entity.ToTable("UserGroups");

            entity.HasKey(x => new { x.UserId, x.GroupId });

            entity.HasOne(x => x.User)
                .WithMany(u => u.Groups)
                .HasForeignKey(x => x.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.Group)
                .WithMany(g => g.Members)
                .HasForeignKey(x => x.GroupId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<ApplicationUserGroupAdmin>(entity =>
        {
            entity.ToTable("AdminUserGroups");

            entity.HasKey(x => new { x.UserId, x.GroupId });

            entity.HasOne<ApplicationUser>()
                .WithMany(u => u.AdminGroups)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne<ApplicationGroup>()
                .WithMany(g => g.Admins)
                .HasForeignKey(x => x.GroupId)
                .OnDelete(DeleteBehavior.Cascade);
        });


    }

    private void ApplySoftDeleteAndAuditRules()
    {
        var now = DateTimeOffset.UtcNow;

        foreach (var entry in ChangeTracker.Entries())
        {
            // 🔹 AUDIT (Create / Update / Concurrency)
            if (entry.Entity is IEntity<string> auditable)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        auditable.CreatedAt = now;
                        auditable.UpdatedAt = null;
                        auditable.ConcurrencyTimestamp = Guid.NewGuid();
                        break;

                    case EntityState.Modified:
                        auditable.UpdatedAt = now;
                        auditable.ConcurrencyTimestamp = Guid.NewGuid();
                        break;
                }
            }

            // 🔹 SOFT DELETE
            if (entry.State == EntityState.Deleted &&
                entry.Entity is ISoftDeleteEntity softDelete)
            {
                entry.State = EntityState.Modified;

                softDelete.DeletedAt = now;

                if (entry.Entity is IEntity<string> auditableOnDelete)
                {
                    auditableOnDelete.UpdatedAt = now;
                    auditableOnDelete.ConcurrencyTimestamp = Guid.NewGuid();
                }
            }
        }
    }

    public override int SaveChanges()
    {
        ApplySoftDeleteAndAuditRules();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        ApplySoftDeleteAndAuditRules();
        return base.SaveChangesAsync(cancellationToken);
    }

    private static LambdaExpression CreateIsDeletedFilter(Type entityType)
    {
        var parameter = Expression.Parameter(entityType, "e");
        var property = Expression.Property(parameter, nameof(ISoftDeleteEntity.IsDeleted));
        var condition = Expression.Equal(property, Expression.Constant(false));
        return Expression.Lambda(condition, parameter);
    }
}