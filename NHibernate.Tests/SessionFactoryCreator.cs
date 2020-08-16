using System;
using System.Reflection;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;

namespace NHibernate.Tests
{
    public class SessionFactoryCreator
    {
        public ISessionFactory GetSessionFactoryWithMbcMappings()
        {
            var config = GetConfigWithoutMappings();
            AddMbcMappings(config);
            return config.BuildSessionFactory();
        }

        public ISessionFactory GetSessionFactoryWithXmlMappings()
        {
            var config = GetConfigWithoutMappings();
            AddXmlMappings(config);
            return config.BuildSessionFactory();
        }

        Configuration GetConfigWithoutMappings()
        {
            var config = new Configuration();

            // For simplicity of reproduction I'm using SQLite in-memory DB, but
            // I have seen the same problem reproduced using MS-SQL 2012 as well, so
            // I don't think that db/dialect is relevant.
            config.DataBaseIntegration(db =>
            {
                db.Driver<Driver.SQLite20Driver>();
                db.Dialect<Dialect.SQLiteDialect>();
                db.ConnectionString = "Data Source=:memory:;Version=3;New=True;";
            });

            return config;
        }

        void AddMbcMappings(Configuration config)
        {
            var mapper = new ConventionModelMapper();
            mapper.AddMapping<PersonMap>();
            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            config.AddDeserializedMapping(mapping, "MbC-Mappings");
        }

        void AddXmlMappings(Configuration config)
        {
            config.AddAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
