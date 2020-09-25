using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FunctionBrokenSqlDepenency
{
    public class EfFunction
    {
        private readonly IConfiguration _configuration;

        public EfFunction(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [FunctionName("EfFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var cs = _configuration.GetConnectionString("Demo");

            DateTime databaseDateTime = default;

            var builder = new DbContextOptionsBuilder<DemoDbContext>();
            builder.UseSqlServer(cs);

            using (var context = new DemoDbContext(builder.Options))
            {
                var foobar = context.Foobars.FromSqlRaw("SELECT GETUTCDATE() as MyDate").Single();
                databaseDateTime = foobar.MyDate;
            }

            return new OkObjectResult("Time from DB: " + databaseDateTime.ToString("O"));
        }
    }

}
