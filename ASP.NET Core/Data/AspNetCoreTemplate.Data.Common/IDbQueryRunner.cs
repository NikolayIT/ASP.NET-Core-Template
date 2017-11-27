namespace AspNetCoreTemplate.Data.Common
{
    using System;

    public interface IDbQueryRunner : IDisposable
    {
        void RunQuery(string query, params object[] parameters);
    }
}
