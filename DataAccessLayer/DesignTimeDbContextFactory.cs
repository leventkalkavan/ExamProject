using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccessLayer;

public class DesignTimeDbContextFactory:IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer("Server=tcp:localhost,1433;Database=ExamProjectDb;User ID=SA;Password=levent123;Trusted_Connection=False;TrustServerCertificate=True;Encrypt=false;");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}