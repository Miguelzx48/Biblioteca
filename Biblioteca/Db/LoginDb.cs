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
        private readonly string connectionString = "server=localhost;database=Biblioteca;user=root;password=123;";

        public Usuario Login(string email, string password)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT Id, Email, Nombres, Apellidos, TipoUsuario, FechaNacimiento, Telefono, Activo
                                FROM Usuarios WHERE Email = @email and Password = @password";
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

        public void CambiarPassword(string email, string password)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Usuarios SET Password = @password WHERE Email = @Email";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public bool ValidarCorreo(string email)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Usuarios WHERE Email = @Email";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }
    }
}
