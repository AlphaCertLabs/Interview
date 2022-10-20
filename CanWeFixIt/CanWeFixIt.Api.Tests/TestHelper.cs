using CanWeFixIt.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanWeFixIt.Api.Tests
{
    internal class TestHelper
    {
        public static CanWeFixItDbContext GetCanWeFixItDbContext()
        {
            var _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = _configuration.GetConnectionString("CanWeFixItDbContext");

            var options = new DbContextOptionsBuilder<CanWeFixItDbContext>()
                .UseSqlite(connectionString)
                .Options;

            var context = new CanWeFixItDbContext(options);

            return context;
        }
    }
}
