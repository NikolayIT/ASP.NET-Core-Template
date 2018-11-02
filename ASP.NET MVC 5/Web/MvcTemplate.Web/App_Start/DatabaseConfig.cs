namespace MvcTemplate.Web
{
    using System.Data.Entity;
    using MvcTemplate.Data;
    using MvcTemplate.Data.Migrations;

    public static class DatabaseConfig
    {
        public static void Config()
        {
            // Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }
    }
}
