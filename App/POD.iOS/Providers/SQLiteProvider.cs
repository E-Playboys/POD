using System;
using System.IO;
using POD.iOS.Providers;
using POD.Repository;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteProvider))]
namespace POD.iOS.Providers
{
    public class SQLiteProvider : ISQLiteProvider
    {
        public SQLiteConnection GetConnection(string databaseName)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            var libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            var path = Path.Combine(libraryPath, databaseName);
            var conn = new SQLiteConnection(path);
            return conn;
        }
    }
}
