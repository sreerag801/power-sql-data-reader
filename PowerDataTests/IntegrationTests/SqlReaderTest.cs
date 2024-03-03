using Microsoft.VisualStudio.TestTools.UnitTesting;
using PowerDataNet.SqlServer;
using PowerDataTests.IntegrationTests.Models;
using System.Data.SqlClient;

namespace PowerDataTests.IntegrationTests
{
    [TestClass]
    public class SqlReaderTest
    {
        IntegrationTestInitializer _context;

        [TestInitialize]
        public void TestInit()
        {
            _context = new IntegrationTestInitializer();

            
            _context.Init();
        }

        [TestMethod]
        public void Test_1()
        {
            var service = _context.GetService<ISqlConnectionStringProvider>();

            var sqlReader = new SqlReader(service);

            var query = @"SELECT TOP (1) [ID]
,[Name]
,[Sex]   
FROM [learn_sql].[dbo].[athlete_events] with(nolock)";

            
            var r = sqlReader.SqlExecuteReader(query, (r) => Map(r));
        }

        public AthletEvents Map(SqlDataReader r)
        {
            return new AthletEvents()
            {
                ID = r["ID"].ToString(),
                Name = r["Name"].ToString(),
                Sex = r["Sex"].ToString()
            };
        }
    }
}
