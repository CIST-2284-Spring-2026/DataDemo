using Microsoft.Data.Sqlite;

namespace DataDemo
{
    public class SchoolRepository
    {
        private readonly DatabaseService db;

        public SchoolRepository(DatabaseService db)
        {
            this.db = db;
        }

        public List<string> GetCampuses()
        {
            return GetList("SELECT Name FROM Campus");
        }

        public List<string> GetCourses()
        {
            return GetList("SELECT Name FROM Course");
        }

        public List<string> GetMajors()
        {
            return GetList("SELECT Title FROM Major");
        }

        private List<string> GetList(string sql)
        {
            List<string> items = new();
            using var conn = new SqliteConnection(db.ConnectionString);
            conn.Open();

            using var cmd = new SqliteCommand(sql, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                items.Add(reader.GetString(0));
            }
            return items;
        }
    }
}
