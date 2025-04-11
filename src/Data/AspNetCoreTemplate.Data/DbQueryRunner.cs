namespace AspNetCoreTemplate.Data
{
    using System;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common;

    using Microsoft.EntityFrameworkCore;

    public class DbQueryRunner(ApplicationDbContext context) : IDbQueryRunner
    {
        public ApplicationDbContext Context { get; set; } = context ?? throw new ArgumentNullException(nameof(context));

        public Task RunQueryAsync(string query, params object[] parameters)
            => this.Context.Database.ExecuteSqlRawAsync(query, parameters);

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Context?.Dispose();
            }
        }
    }
}
