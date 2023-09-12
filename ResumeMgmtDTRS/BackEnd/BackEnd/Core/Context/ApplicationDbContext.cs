using BackEnd.Core.Entites;
using Microsoft.EntityFrameworkCore;
using Npgsql.Replication.PgOutput.Messages;

namespace BackEnd.Core.Context
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}
        public DbSet<Company> Companies { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Candidate> Candidates { get; set; }


		//Defining relationship between entities with foreign key
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Job>()
				.HasOne(job => job.Company)
				.WithMany(company => company.Jobs)
				.HasForeignKey(job => job.CompanyId);

			modelBuilder.Entity<Candidate>()
				.HasOne(candidate => candidate.Job)
				.WithMany(job => job.Candidates)
				.HasForeignKey(candidate => candidate.JobId);


			/*Changing data type of Company Size and Job Level'int' to string as enum is converted to string*/

			modelBuilder.Entity<Company>()
				.Property(company => company.Size)
				.HasConversion<string>();

			modelBuilder.Entity<Job>()
				.Property(job => job.Level)
				.HasConversion<string>();


		}
	}
}
