namespace MvcTemplate.Services.Web
{
    using System;
    using System.Web;
    using System.Web.Caching;

    public class HttpCacheService : ICacheService
    {
        private static readonly object LockObject = new object();

        public T Get<T>(string itemName, Func<T> getDataFunc, int durationInSeconds)
        {
            if (HttpRuntime.Cache[itemName] == null)
            {
                lock (LockObject)
                {
                    if (HttpRuntime.Cache[itemName] == null)
                    {
                        var data = getDataFunc();
                        HttpRuntime.Cache.Insert(
                            itemName,
                            data,
                            null,
                            DateTime.Now.AddSeconds(durationInSeconds),
                            Cache.NoSlidingExpiration);
                    }
                }
            }

            return (T)HttpRuntime.Cache[itemName];
        }

        public void Remove(string itemName)
        {
            HttpRuntime.Cache.Remove(itemName);
        }
    }
}
