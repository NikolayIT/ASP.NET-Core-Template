using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcTemplate.Services.Web.Tests
{
    [TestFixture]
    public class IdentifierProviderTests
    {
        [Test]
        public void EncodingAndDecodingDoesntChangeTheId()
        {
            const int Id = 1337;
            IIdentifierProvider provider = new IdentifierProvider();
            var encoded = provider.EncodeId(Id);
            var actual = provider.DecodeId(encoded);
            Assert.AreEqual(Id, actual);
        }
    }
}
