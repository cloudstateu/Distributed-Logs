using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Back
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DatabaseContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public async Task ExectureQuery()
        {
            using (var cmd = this.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = _configuration.GetValue<string>("DatabaseQuery");

                await this.Database.OpenConnectionAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<bool> CanConnect()
        {
            var ret = true;

            try
            {
                await this.Database.OpenConnectionAsync();
                await this.Database.CloseConnectionAsync();
            }
            catch
            {
                ret = false;
            }


            return ret;
        }
    }
}
