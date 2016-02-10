namespace Crawler
{
    using System;
    using AngleSharp;
    using MvcTemplate.Data;
    using MvcTemplate.Data.Common;
    using MvcTemplate.Data.Models;
    using MvcTemplate.Services.Data;

    public static class Program
    {
        public static void Main()
        {
            var db = new ApplicationDbContext();
            var repo = new DbRepository<JokeCategory>(db);
            var categoriesService = new CategoriesService(repo);

            var configuration = Configuration.Default.WithDefaultLoader();
            var browsingContext = BrowsingContext.New(configuration);

            for (int i = 1; i <= 10000; i++)
            {
                var url = $"http://vicove.com/vic-{i}";
                var document = browsingContext.OpenAsync(url).Result;
                var jokeContent = document.QuerySelector("#content_box .post-content").TextContent.Trim();
                if (!string.IsNullOrWhiteSpace(jokeContent))
                {
                    var categoryName = document.QuerySelector("#content_box .thecategory a").TextContent.Trim();
                    var category = categoriesService.EnsureCategory(categoryName);
                    var joke = new Joke { Category = category, Content = jokeContent };
                    db.Jokes.Add(joke);
                    db.SaveChanges();
                    Console.WriteLine(i);
                }
            }
        }
    }
}
