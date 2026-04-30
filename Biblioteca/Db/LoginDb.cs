using Biblioteca.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Db
{
    public class LoginDb
    {
        private readonly string connectionString = "";

        public Usuario Login(string email, string password)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT Id, Email, Nombres, Apellidos, TipoUsuario, FechaNacimiento, Telefono, Activo
                                WHERE Email = @email and Password = @password";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", password);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Usuario
                                (
                                    reader.GetInt32("Id"),
                                    reader.GetString("Email"),
                                    reader.GetString("Nombres"),
                                    reader.GetString("Apellidos"),
                                    reader.GetInt32("TipoUsuario"),
                                    reader.GetDateTime("FechaNacimiento"),
                                    reader.GetString("Telefono"),
                                    reader.GetBoolean("Activo")
                                );
                        }
                    }
                }
            }
            return null;
        }

        public void CambiarPassword(int idUsuario, string password)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Usuarios SET Password = @password WHERE Id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@id", idUsuario);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
