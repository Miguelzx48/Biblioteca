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
    internal class LibroDb
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
                    cmd.Parameters.AddWithValue("@aniopublicacion", libro.AnioPublicaion);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public LibroDb() { }
        public void Actualizar(libro libro)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    UPDATE Libros SET Stock = @stock, Descripcion = @descripcion     
                    WHERE IdLibro = @idLibro";    
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@stock",libro.Stock);
                    cmd.Parameters.AddWithValue("@descripcion", libro.Descripcion);
                    cmd.Parameters.AddWithValue("@idLibro", libro.IdLibro);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Buscar(int idLibro)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = @"SELECT IdLibro, Nombre, Autor, Categoria,
                        Editorial, Stock, Descripcion, AñoPublicaion
                        FROM Libros
                        WHERE IdLibro = @idLibro";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idLibro", idLibro);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            MessageBox.Show(
                                "Nombre: " + reader["Nombre"] +
                                "\nAutor: " + reader["Autor"] +
                                "\nCategoria: " + reader["Categoria"]
                            );
                        }
                        else
                        {
                            MessageBox.Show("Libro no encontrado");
                        }
                    }
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

    }
}
