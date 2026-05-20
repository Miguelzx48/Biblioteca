using Biblioteca.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
                    Insert INTO Libros ( Nombre , Autor , Categoria,  Editorial , Stock ,  Descripcion , AnioPublicacion,Imagen)
                    VALUES (@nombre ,@autor ,@categoria, @editorial,@stock ,@descripcion,@anioPublicacion,@imagen)";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", libro.Nombre);
                    cmd.Parameters.AddWithValue("@autor", libro.Autor);
                    cmd.Parameters.AddWithValue("@categoria", libro.Categoria);
                    cmd.Parameters.AddWithValue("@editorial", libro.Editorial);
                    cmd.Parameters.AddWithValue("@stock", libro.Stock);
                    cmd.Parameters.AddWithValue("@descripcion", libro.Descripcion);
                    cmd.Parameters.AddWithValue("@aniopublicacion", libro.AnioPublicacion);
                    cmd.Parameters.AddWithValue("@imagen", libro.Imagen);
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

                string query = "SELECT IdLibro, Nombre, Autor, Categoria, Editorial, Stock, Descripcion, AnioPublicacion, Imagen FROM Libros";

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
                                AnioPublicacion = Convert.ToInt32(reader["AnioPublicacion"]),
                                Imagen = reader["Imagen"].ToString()
                            };
                            lista.Add(libro);
                        }
                    }
                }
             }
            return lista;
        }
        public libro LeerLibroPorId(int idLibro)
        {

            libro lib = null;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {

                conn.Open();

                string query = @" SELECT * FROM Libros WHERE IdLibro = @idLibro";

                using (MySqlCommand stmt = new MySqlCommand(query, conn))
                {
                    stmt.Parameters.AddWithValue("@idLibro", idLibro);

                    using (MySqlDataReader result = stmt.ExecuteReader())
                    {
                        if (result.Read())
                        {

                            lib = new libro();

                            lib.IdLibro = result.GetInt32("IdLibro");
                            lib.Nombre = result.GetString("Nombre");
                            lib.Autor = result.GetString("Autor");
                            lib.Categoria = result.GetString("Categoria");
                            lib.Editorial = result.GetString("Editorial");
                            lib.Stock = result.GetInt32("Stock");
                            lib.Descripcion = result.GetString("Descripcion");
                            lib.AnioPublicacion = result.GetInt32("AnioPublicacion");

                        }
                    }
                }
            }

            return lib;
        }
        public bool DescontarStock(int idLibro, int stock)
        {

            string query = @" UPDATE Libros SET Stock = @stock WHERE IdLibro = @idLibro";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {

                conn.Open();

                using (MySqlCommand stmt = new MySqlCommand(query, conn))
                {
                    stmt.Parameters.AddWithValue("@idLibro", idLibro);

                    stmt.Parameters.AddWithValue("@stock", stock);

                    int result = stmt.ExecuteNonQuery();

                    return result > 0;
                }
            }
        }
        public bool AumentarStock(int idLibro, int stock)
        {
            return DescontarStock(idLibro, stock);
        }
        public List<libro> BuscadorLibros(String texto)
        {

            List<libro> lista = new List<libro>();

            string query = @" SELECT * FROM Libros WHERE Nombre LIKE @texto OR Autor LIKE @texto 
                            OR Categoria LIKE @texto OR Editorial LIKE @texto";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {

                conn.Open();

                using (MySqlCommand stmt = new MySqlCommand(query, conn))
                {
                    stmt.Parameters.AddWithValue("@texto", "%" + texto + "%");

                    using (MySqlDataReader result = stmt.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            libro lib = new libro();

                            lib.IdLibro = result.GetInt32("IdLibro");
                            lib.Nombre = result.GetString("Nombre");
                            lib.Autor = result.GetString("Autor");
                            lib.Categoria = result.GetString("Categoria");
                            lib.Editorial = result.GetString("Editorial");
                            lib.Stock = result.GetInt32("Stock");
                            lib.Descripcion = result.GetString("Descripcion");
                            lib.AnioPublicacion = result.GetInt32("AnioPublicacion");
                            lib.Imagen = result.IsDBNull(result.GetOrdinal("Imagen")) ? null : result.GetString("Imagen");

                            lista.Add(lib);
                        }
                    }
                }
            }

            return lista;
        }
        public List<Grafico> LibrosMasPrestadosPorMes()
        {

            List<Grafico> lista = new List<Grafico>();

            string query = @"SELECT l.Nombre, COUNT(p.IdLibro) AS Cantidad FROM Prestamos p
                            INNER JOIN Libros l ON p.IdLibro = l.IdLibro WHERE MONTH(p.FechaPrestamo) = MONTH(CURDATE()) AND YEAR(p.FechaPrestamo) = YEAR(CURDATE())
                            GROUP BY l.Nombre ORDER BY Cantidad DESC LIMIT 5";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                    
                conn.Open();

                using (MySqlCommand stmt = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader result = stmt.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            Grafico grafico = new Grafico();

                            grafico.Nombre = result.GetString("Nombre");
                            grafico.Cantidad = result.GetDouble("Cantidad");

                            lista.Add(grafico);
                        }
                    }
                }
            }

            return lista;
        }
    }
}
