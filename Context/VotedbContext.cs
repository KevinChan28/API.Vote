using API.UsersVote.Models;
using Microsoft.EntityFrameworkCore;

namespace API.UsersVote.Context;

public partial class VotedbContext : DbContext
{
	public VotedbContext()
	{
	}

	public VotedbContext(DbContextOptions<VotedbContext> options)
		: base(options)
	{
	}

	public virtual DbSet<PoliticParty> PoliticParties { get; set; }

	public virtual DbSet<User> Users { get; set; }

	public virtual DbSet<Vote> Votes { get; set; }
	public virtual DbSet<DocumentAdmin> DocumentAdmins { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<PoliticParty>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("PRIMARY");

			entity.ToTable("politic_party");

			entity.HasIndex(e => e.Applicant, "idx_Applicant");

			entity.Property(e => e.Applicant).HasMaxLength(400);
			entity.Property(e => e.Name).HasMaxLength(300);
		});

		modelBuilder.Entity<User>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("PRIMARY");

			entity.ToTable("user");

			entity.HasIndex(e => e.Curp, "CURP").IsUnique();

			entity.Property(e => e.Curp)
				.HasMaxLength(100)
				.HasColumnName("CURP");
			entity.Property(e => e.Rol).HasMaxLength(300);
			entity.Property(e => e.Email).HasMaxLength(300);
		});

		modelBuilder.Entity<Vote>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("PRIMARY");

			entity.ToTable("vote");

			entity.HasIndex(e => e.Applicant, "idx_Applicant");

			entity.Property(e => e.Applicant).HasMaxLength(400);
			entity.Property(e => e.CreatedDate)
				.HasColumnType("datetime")
				.HasColumnName("Created_Date");
			entity.Property(e => e.VoteLocation).HasColumnName("Vote_Location");
		});

		modelBuilder.Entity<DocumentAdmin>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("PRIMARY");

			entity.ToTable("document_admin");

			entity.HasIndex(e => e.UserId, "idx_userId");
			entity.HasIndex(e => e.Section, "idx_SECTION");

			entity.Property(e => e.Section).HasMaxLength(400);
		});

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
