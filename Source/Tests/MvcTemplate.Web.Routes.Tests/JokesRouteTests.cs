namespace MvcTemplate.Web.Routes.Tests
{
    using System.Web.Routing;

    using MvcRouteTester;

    using MvcTemplate.Web.Controllers;

    using NUnit.Framework;

    [TestFixture]
    public class JokesRouteTests
    {
        [Test]
        public void TestRouteById()
        {
            var routeCollection = new RouteCollection();
            const string url = "/Joke/Mjc2NS4xMjMxMjMxMzEyMw==";
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection.ShouldMap(url).To<JokesController>(c => c.ById("Mjc2NS4xMjMxMjMxMzEyMw=="));
        }
    }
}
