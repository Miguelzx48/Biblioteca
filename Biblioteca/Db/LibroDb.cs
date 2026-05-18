using Biblioteca.Models;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Biblioteca.Db
{
    public class LibroDb
    {
        private readonly string connectionString = "server=localhost;database=Biblioteca;user=root;password=123;";


        public void CrearLibro(libro libro)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    Insert INTO Libros ( Nombre , Autor , Categoria,  Editorial , Stock ,  Descripcion , AnioPublicacion)
                    VALUES (@nombre ,@autor ,@categoria, @editorial,@stock ,@descripcion,@anioPublicacion)";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", libro.Nombre);
                    cmd.Parameters.AddWithValue("@autor", libro.Autor);
                    cmd.Parameters.AddWithValue("@categoria", libro.Categoria);
                    cmd.Parameters.AddWithValue("@editorial", libro.Editorial);
                    cmd.Parameters.AddWithValue("@stock", libro.Stock);
                    cmd.Parameters.AddWithValue("@descripcion", libro.Descripcion);
                    cmd.Parameters.AddWithValue("@aniopublicacion", libro.AnioPublicacion);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public LibroDb() { }
        public void Actualizar(libro libro)
        {
            using (MySqlConnection conn =
                   new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
        UPDATE Libros 
        SET 
            Nombre = @nombre,
            Autor = @autor,
            Stock = @stock,
            Descripcion = @descripcion
        WHERE IdLibro = @idLibro";

                using (MySqlCommand cmd =
                       new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@nombre", libro.Nombre);

                    cmd.Parameters.AddWithValue(
                        "@autor", libro.Autor);

                    cmd.Parameters.AddWithValue(
                        "@stock", libro.Stock);

                    cmd.Parameters.AddWithValue(
                        "@descripcion", libro.Descripcion);

                    cmd.Parameters.AddWithValue(
                        "@idLibro", libro.IdLibro);

                    int filas = cmd.ExecuteNonQuery();

                    MessageBox.Show(
                        "Filas actualizadas: " + filas);
                }
            }
        }

        public void Eliminar(int idLibro)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM  Libros  WHERE IdLibro = @idLibro";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idLibro",idLibro);
                    cmd.ExecuteNonQuery();
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
        public List<libro> ObtenerTodos()
        {
            List<libro> lista = new List<libro>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT IdLibro, Nombre, Autor, Categoria, Editorial, Stock, Descripcion, AnioPublicacion FROM Libros";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            libro libro = new libro
                            {
                                IdLibro = Convert.ToInt32(reader["IdLibro"]),
                                Nombre = reader["Nombre"].ToString(),
                                Autor = reader["Autor"].ToString(),
                                Categoria = reader["Categoria"].ToString(),
                                Editorial = reader["Editorial"].ToString(),
                                Stock = Convert.ToInt32(reader["Stock"]),
                                Descripcion = reader["Descripcion"].ToString(),
                                AnioPublicacion = Convert.ToInt32(reader["AnioPublicacion"])
                            };
                            lista.Add(libro);
                        }
                    }
                }
             }
            return lista;
        }
    }
}
