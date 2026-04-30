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
        private readonly string connectionString = "";

        public void CrearUsuario(Usuario usuario)
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
        }

        public void Actualizar(Usuario usuario)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    UPDATE Usuarios SET Nombres = @nombres, Apellidos = @apellidos, Telefono = @telefono , Activo = @activo
                    WHERE Id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombres", usuario.Nombres);
                    cmd.Parameters.AddWithValue("@apellidos", usuario.Apellidos);
                    cmd.Parameters.AddWithValue("@telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("@activo", usuario.Activo);
                    cmd.Parameters.AddWithValue("@id", usuario.Id);
                    cmd.ExecuteNonQuery();
                }
            }
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
    }
}
