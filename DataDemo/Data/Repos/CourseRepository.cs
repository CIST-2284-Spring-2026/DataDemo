using DataDemo.Data.Models;
using Microsoft.Data.Sqlite;

namespace DataDemo.Data.Repos
{
    public class CourseRepository
    {
        private readonly DatabaseService db;

        public CourseRepository(DatabaseService db)
        {
            this.db = db;
        }

        public List<Course> GetAll()
        {
            List<Course> items = new List<Course>();
            using var conn = new SqliteConnection(db.ConnectionString);
            conn.Open();
            string sql = "SELECT Id, Name FROM Course ORDER BY Name;";
            using var cmd = new SqliteCommand(sql, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Course course = new Course
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                };
                items.Add(course);
            }
            return items;
        }

        public Course? GetById(int id)
        {
            using var conn = new SqliteConnection(db.ConnectionString);
            conn.Open();
            string sql = "SELECT Id, Name FROM Course WHERE Id = $id;";
            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("$id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Course
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                };
            }
            return null;
        }

        public void Add(Course campus)
        {
            using var conn = new SqliteConnection(db.ConnectionString);
            conn.Open();
            string sql = "INSERT INTO Course (Name) VALUES ($name);";
            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("$name", campus.Name);
            cmd.ExecuteNonQuery();
        }
        public void Update(Course course)
        {
            using var conn = new SqliteConnection(db.ConnectionString);
            conn.Open();
            string sql = "UPDATE Course SET Name = $name WHERE Id = $id;";
            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("$name", course.Name);
            cmd.Parameters.AddWithValue("$id", course.Id);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = new SqliteConnection(db.ConnectionString);
            conn.Open();
            string sql = "DELETE FROM Course WHERE Id = $id;";
            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("$id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
