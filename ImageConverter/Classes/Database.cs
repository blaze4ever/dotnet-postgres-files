using System;
using Npgsql;
using UniversalImageConverter.Structure;

namespace UniversalImageConverter.Classes
{
    class Database
    {
        private string _dbname;
        private string _host;
        private string _password;
        private int _port;
        private string _user;
        private string _connection_string;

        public Database()
        {
            LoadConfig();
        }

        private void LoadConfig()
        {
            _host = "localhost";
            _user = "postgres";
            _port = 5432;
            _dbname = "pictures";
            _password = "monitoring";
        }

        public string GetConnectionString()
        {
            _connection_string = $"Server={_host};Port={_port};User Id={_user};Password={_password};Database={_dbname};CommandTimeout=0;";
            return _connection_string;
        }

        public void InsertImage(string query, ObjectExtraData data)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(_connection_string))
                {
                    conn.Open();
                    var command = new NpgsqlCommand();
                    command.Connection = conn;
                    command.CommandText = query;
                    command.Parameters.AddWithValue("picture", NpgsqlTypes.NpgsqlDbType.Bytea, data.photo);
                    command.Parameters.AddWithValue("object_id", NpgsqlTypes.NpgsqlDbType.Integer, data.article_id);
                    command.ExecuteNonQuery();

                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"[ERROR] {DateTime.UtcNow} - InsertImage | {e.Message}");
            }
        }



        public byte[] GetImage(string query)
        {
            byte[] result = null;

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(_connection_string))
                {
                    conn.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(query, conn))
                    {
                        result = (byte[])command.ExecuteScalar();
                    }
                    conn.Close();
                }
            }

            catch (Exception e)
            {
                Console.WriteLine($"[ERROR] {DateTime.UtcNow} - GetImage | {e.Message}");
            }

            return result;
        }
    }
}
