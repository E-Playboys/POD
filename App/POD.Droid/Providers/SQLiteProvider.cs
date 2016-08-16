using System.IO;
using POD.Droid.Providers;
using POD.Repository;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteProvider))]
namespace POD.Droid.Providers
{
    public class SQLiteProvider : ISQLiteProvider
    {
        public SQLiteConnection GetConnection(string databaseName)
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, databaseName);
            var conn = new SQLiteConnection(path);
            return conn;
        }
    }
}