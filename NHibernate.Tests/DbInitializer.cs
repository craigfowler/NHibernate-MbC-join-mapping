using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Reflection;

namespace NHibernate.Tests
{
    /// <summary>
    /// Initializer class executes the script <c>CreateDb.sql</c> on the specified connection.
    /// </summary>
    public class DbInitializer
    {
        public void InitializeDatabase(DbConnection connection)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            if (connection.State != ConnectionState.Open)
                connection.Open();

            var initScript = GetInitScript();
            ExecuteScript(connection, initScript);
        }

        string GetInitScript()
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("NHibernate.Tests.CreateDb.sql"))
            using(var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        void ExecuteScript(DbConnection connection, string script)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = script;
                command.ExecuteNonQuery();
            }
        }
    }
}
