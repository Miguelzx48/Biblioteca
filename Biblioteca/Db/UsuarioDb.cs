using Biblioteca.Models;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Biblioteca.Db
{
    public class UsuarioDb
    {
        private readonly string connectionString = "server=localhost;database=Biblioteca;user=root;password=123;";

        public void CrearUsuarioAdminDefault()
        {
            Usuario admin = new Usuario
            (
                "admin@biblioteca.com",
                "admin123",
                "Admin",
                "Default",
                1,
                new DateTime(1990, 1, 1),
                "1234567890",
                true
            );
            CrearUsuario(admin);
        }

        public string CrearUsuario(Usuario usuario)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    Insert INTO Usuarios (Email, Password, Nombres, Apellidos, TipoUsuario, FechaNacimiento, Telefono, Activo)
                    VALUES (@email, @password, @nombres, @apellidos, @tipousuario, @fechanacimiento, @telefono, activo)";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@email", usuario.Email);
                    cmd.Parameters.AddWithValue("@password", usuario.Password);
                    cmd.Parameters.AddWithValue("@nombres", usuario.Nombres);
                    cmd.Parameters.AddWithValue("@apellidos", usuario.Apellidos);
                    cmd.Parameters.AddWithValue("@tipousuario", usuario.TipoUsuario);
                    cmd.Parameters.AddWithValue("@fechanacimiento",usuario.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@telefono",usuario.Telefono);
                    cmd.Parameters.AddWithValue("@activo",usuario.Activo);
                    cmd.ExecuteNonQuery();
                }
            }

            return "Usuario creado exitosamente";
        }

        public string Actualizar(Usuario usuario)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    UPDATE Usuarios SET Telefono = @telefono ,Password = @password, Activo = @activo
                    WHERE Id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {  
                    cmd.Parameters.AddWithValue("@telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("@password", usuario.Password);
                    cmd.Parameters.AddWithValue("@activo", usuario.Activo);
                    cmd.Parameters.AddWithValue("@id", usuario.Id);
                    cmd.ExecuteNonQuery();
                }
            }

            return "Usuario actualizado exitosamente";
        }

        public List<Usuario> Obtener()
        {
            List<Usuario> usuarios = new List<Usuario>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Id, Email, Nombres, Apellidos, TipoUsuario, FechaNacimiento, Telefono, Activo";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuarios.Add(
                            new Usuario
                            (
                                reader.GetInt32("Id"),
                                reader.GetString("Email"),
                                reader.GetString("Nombres"),
                                reader.GetString("Apellidos"),
                                reader.GetInt32("TipoUsuario"),
                                reader.GetDateTime("FechaNacimiento"),
                                reader.GetString("Telefono"),
                                reader.GetBoolean("Activo")
                            ));
                    }
                }   
            }
            return usuarios;
        }
        public Usuario ObtenerPorId(int idUsuario)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT Id, Email, Nombres, Apellidos, TipoUsuario, FechaNacimiento, Telefono, Activo
                                WHERE Id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idUsuario);
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

        public void Eliminar(int idUsuario)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Usuarios SET Activo = 0 WHERE Id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idUsuario);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool ExisteCorreo(string email)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Usuarios WHERE email = @email";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        public void ProbarConexion()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    MessageBox.Show("Conexion exitosa");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }

}
    


