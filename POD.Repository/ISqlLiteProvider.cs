using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace POD.Repository
{
    public interface ISqlLiteProvider
    {
        SQLiteConnection GetConnection(string databaseName);
    }
}
