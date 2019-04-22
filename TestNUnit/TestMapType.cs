using ImportExportSql;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestNUnit
{
    public class TestMapType
    {
        [Test]
        public void TestMap()
        {
            //Type type = typeof(byte[]);
            var list = new List<string>() { "varchar", "datetime", "smalldatetime", "int", "tinyint",
                    "uniqueidentifier", "bit", "nvarchar", "bigint", "float", "real", "money",
                    "decimal", "varbinary", "smallint"};
            var lst = list.Select(t => Row.MapTypeFromSql(t)).ToList();
            lst.ForEach(t => Assert.IsNotNull(t, t));
            var lst2 = lst.Select(t => Type.GetType(t, false, true)).ToList();
            lst2.ForEach(t => Assert.IsNotNull(t));
        }
    }
}
