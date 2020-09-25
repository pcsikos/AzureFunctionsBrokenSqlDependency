using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FunctionBrokenSqlDepenency
{
    public class Function1
    {
        private readonly IConfiguration _configuration;

        public Function1(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [FunctionName("Function1")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var cs = _configuration.GetConnectionString("Demo");

            DateTime databaseDateTime = default;
            using (var connection = new SqlConnection(cs))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT GETUTCDATE()";
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        databaseDateTime = reader.GetDateTime(0);
                    }
                }
            }
            return new OkObjectResult("Time from DB: " + databaseDateTime.ToString("O"));
        }
    }

}
