using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Concrete.Core.Data;

public class ConcreteContextFactory : IDesignTimeDbContextFactory<ConcreteContext>
{
	public ConcreteContext CreateDbContext(string[] args)
	{
		var builder = new DbContextOptionsBuilder<ConcreteContext>();
		builder.UseSqlServer();
		var result = new ConcreteContext(builder.Options);
		return result;
	}
}
