using Microsoft.EntityFrameworkCore;
namespace GoogleTwitterOauth.Models.DBcontext
{
	public class DBContext : DbContext
	{
		public DBContext(DbContextOptions<DBContext> options) : base(options)
		{
		}

		public DBContext() { }

		public virtual DbSet<TwitterAuthtoken> TwitterAuth { get; set; }


	}
}
