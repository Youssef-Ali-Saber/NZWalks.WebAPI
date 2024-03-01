using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NZWalks.WebAPI.Data;

namespace NZWalks.WebAPI.Data
{
	public class NZWalksAuthDbContext : IdentityDbContext 
	{
		public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> dbContextOptions) : base(dbContextOptions)
		{
		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			var reader = Guid.NewGuid().ToString();
			var writer = Guid.NewGuid().ToString();

			builder.Entity<IdentityRole>().HasData(
				new IdentityRole
				{
					Id = reader,
					ConcurrencyStamp = reader,
					Name = "Reader",
					NormalizedName = "Reader".ToUpper()
				},
				new IdentityRole
				{
					Id = writer,
					ConcurrencyStamp = writer,
					Name = "Writer",
					NormalizedName = "Writer".ToUpper()
				});
		}
	}
}
