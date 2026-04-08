using DataDemo.Data.Models;
using Microsoft.Data.Sqlite;

namespace DataDemo.Data.Repos
{
    public class CampusRepository
    {
        private readonly DatabaseService db;

        public CampusRepository(DatabaseService db)
        {
            this.db = db;
        }

        public List<Campus> GetAll()
        {
            List<Campus> items = new List<Campus>();
            using var conn = new SqliteConnection(db.ConnectionString);
            conn.Open();
            string sql = "SELECT Id, Name FROM Campus ORDER BY Name;";
            using var cmd = new SqliteCommand(sql, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Campus campus = new Campus
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                };
                items.Add(campus);
            }
            return items;
        }

        public Campus? GetById(int id)
        {
            using var conn = new SqliteConnection(db.ConnectionString);
            conn.Open();
            string sql = "SELECT Id, Name FROM Campus WHERE Id = $id;";
            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("$id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Campus
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                };
            }
            return null;
        }

        public void Add(Campus campus)
        {
            using var conn = new SqliteConnection(db.ConnectionString);
            conn.Open();
            string sql = "INSERT INTO Campus (Name) VALUES ($name);";
            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("$name", campus.Name);
            cmd.ExecuteNonQuery();
        }
        public void Update(Campus campus)
        {
            using var conn = new SqliteConnection(db.ConnectionString);
            conn.Open();
            string sql = "UPDATE Campus SET Name = $name WHERE Id = $id;";
            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("$name", campus.Name);
            cmd.Parameters.AddWithValue("$id", campus.Id);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = new SqliteConnection(db.ConnectionString);
            conn.Open();
            string sql = "DELETE FROM Campus WHERE Id = $id;";
            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("$id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
