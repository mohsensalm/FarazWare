using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FarazWare.Persistence.Context
{
    //public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    //{
    //    //public DataContext CreateDbContext(string[] args)
    //    //{
    //    //    var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
    //    //    var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Jpneo.CorpBanking.API");
    //    //    IConfigurationRoot configuration = new ConfigurationBuilder().Build();

    //    //    var connectionString = configuration.GetConnectionString("DefaultConnection");

    //    //    optionsBuilder.UseSqlServer(connectionString);

    //    //    return new DataContext(optionsBuilder.Options);
    //    //}
    //}
}