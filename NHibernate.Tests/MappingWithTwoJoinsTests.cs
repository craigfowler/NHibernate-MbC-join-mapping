using System;
using NUnit.Framework;
using System.Linq;

namespace NHibernate.Tests
{
    [TestFixture]
    public class MappingWithTwoJoinsTests
    {
        [Test]
        public void NHibernate_should_not_throw_when_querying_a_Person_mapped_using_MbC()
        {
            var sessionFactoryCreator = new SessionFactoryCreator();

            using (var sessionFactory = sessionFactoryCreator.GetSessionFactoryWithMbcMappings())
            using(var session = sessionFactory.OpenSession())
            {
                var initializer = new DbInitializer();
                initializer.InitializeDatabase(session.Connection);

                Assert.That(() => session.Query<Person>().FirstOrDefault(x => x.Name == "Jane Doe"), Throws.Nothing);
            }
        }

        [Test]
        public void NHibernate_should_not_throw_when_querying_a_Person_mapped_using_XML()
        {
            var sessionFactoryCreator = new SessionFactoryCreator();

            using (var sessionFactory = sessionFactoryCreator.GetSessionFactoryWithXmlMappings())
            using (var session = sessionFactory.OpenSession())
            {
                var initializer = new DbInitializer();
                initializer.InitializeDatabase(session.Connection);

                Assert.That(() => session.Query<Person>().FirstOrDefault(x => x.Name == "Jane Doe"), Throws.Nothing);
            }
        }
    }
}
