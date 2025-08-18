using Ayllu.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ayllu.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Organization> Organizations => Set<Organization>();
    public DbSet<AppUserOrganization> UserOrganizations => Set<AppUserOrganization>();
    public DbSet<Movement> Movements => Set<Movement>();
    public DbSet<Dialectic> Dialectics => Set<Dialectic>();
    public DbSet<DialecticStage> DialecticStages => Set<DialecticStage>();
    public DbSet<UserConnection> UserConnections => Set<UserConnection>();
    public DbSet<InvalidatedToken> InvalidatedTokens => Set<InvalidatedToken>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Identity renomeado (opcional) */
        builder.Entity<AppUser>().ToTable("Users");
        builder.Entity<AppRole>().ToTable("Roles");
        builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
        builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
        builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
        builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

        /* Organization */
        builder.Entity<Organization>(b =>
        {
            b.ToTable("Organizations");
            b.HasKey(x => x.Id);

            b.Property(x => x.Name).HasMaxLength(200).IsRequired();
            b.Property(x => x.Description).HasMaxLength(2000);
            b.Property(x => x.LogoUrl).HasMaxLength(1000);

            b.HasIndex(x => x.Name).IsUnique();
        });

        /* AppUserOrganization */
        builder.Entity<AppUserOrganization>(b =>
        {
            b.ToTable("UserOrganizations");
            b.HasKey(x => new { x.UserId, x.OrganizationId });

            b.Property(x => x.Role).HasConversion<int>().IsRequired();

            b.HasOne(x => x.User)
             .WithMany(u => u.OrganizationMemberships)
             .HasForeignKey(x => x.UserId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.Organization)
             .WithMany(o => o.Members)
             .HasForeignKey(x => x.OrganizationId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        /* Movement */
        builder.Entity<Movement>(b =>
        {
            b.ToTable("Movements");
            b.HasKey(x => x.Id);

            b.Property(x => x.Title).HasMaxLength(200).IsRequired();
            b.Property(x => x.Description).HasMaxLength(4000);

            b.HasOne(x => x.Organization)
             .WithMany(o => o.Movements)
             .HasForeignKey(x => x.OrganizationId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.CreatedBy)
             .WithMany(u => u.MovementsCreated)
             .HasForeignKey(x => x.CreatedById)
             .OnDelete(DeleteBehavior.Restrict);
        });

        /* Dialectic */
        builder.Entity<Dialectic>(b =>
        {
            b.ToTable("Dialectics");
            b.HasKey(x => x.Id);

            b.Property(x => x.Title).HasMaxLength(200).IsRequired();

            b.HasOne(x => x.Organization)
             .WithMany(o => o.Dialectics)
             .HasForeignKey(x => x.OrganizationId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.CreatedBy)
             .WithMany(u => u.DialecticsCreated)
             .HasForeignKey(x => x.CreatedById)
             .OnDelete(DeleteBehavior.Restrict);
        });

        /* DialecticStage */
        builder.Entity<DialecticStage>(b =>
        {
            b.ToTable("DialecticStages");
            b.HasKey(x => x.Id);

            b.Property(x => x.StageType).HasConversion<int>().IsRequired();
            b.Property(x => x.Content).IsRequired();

            b.HasOne(x => x.Dialectic)
             .WithMany(d => d.Stages)
             .HasForeignKey(x => x.DialecticId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.Author)
             .WithMany(u => u.DialecticStagesAuthored)
             .HasForeignKey(x => x.AuthorId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<UserConnection>(b =>
        {
            b.ToTable("UserConnections", t =>
            {
                t.HasCheckConstraint("CK_UserConnections_Order", "[User1Id] < [User2Id]");
            });
            // Chave composta (User1Id, User2Id)
            b.HasKey(x => new { x.User1Id, x.User2Id });

            b.Property(x => x.Status).HasConversion<int>().IsRequired();

            b.HasOne(x => x.User1)
             .WithMany()
             .HasForeignKey(x => x.User1Id)
             .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(x => x.User2)
             .WithMany()
             .HasForeignKey(x => x.User2Id)
             .OnDelete(DeleteBehavior.Restrict);
             
        });

        builder.Entity<InvalidatedToken>(b =>
        {
            b.ToTable("InvalidatedTokens");
            b.HasKey(x => x.Id);

            b.Property(x => x.Jwt).IsRequired();
            b.HasIndex(x => x.Jwt).IsUnique(); // garante que não duplica invalidação
        });
    }
}
