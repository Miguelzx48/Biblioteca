using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Db
{
    internal class LibroDb
    {
        private readonly string connectionString = "";

        public void CrearLibro(libro libro)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    Insert INTO Libros ( Nombre , Autor , Categoria,  Editorial , Stock ,  Descripcion , AñoPublicaion )
                    VALUES ( ,@nombre ,@autor ,@categoria, @editorial,@stock ,@ descripcion,@ añoPublicaion )";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", libro.Nombre);
                    cmd.Parameters.AddWithValue("@autor", libro.Autor);
                    cmd.Parameters.AddWithValue("@categoria", libro.Categoria);
                    cmd.Parameters.AddWithValue("@editorial", libro.Editorial);
                    cmd.Parameters.AddWithValue("@stock", libro.Stock);
                    cmd.Parameters.AddWithValue("@descripcion", libro.Descripcion);
                    cmd.Parameters.AddWithValue("@añopublicacion", libro.AñoPublicaion);
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
                    cmd.Parameters.AddWithValue("@stok",libro.Stock);
                    cmd.Parameters.AddWithValue("@descripcion", libro.Descripcion);
                    cmd.Parameters.AddWithValue("@idLibro", libro.IdLibro);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Buscar(libro libro)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT  IdLibro, Nombre,Autor ,Categorial, EditorialStock, Descripcion ,AñoPublicaion WHERE Idlibro = @idLibro"; 
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", libro.Nombre);
                    cmd.Parameters.AddWithValue("@autor", libro.Autor);
                    cmd.Parameters.AddWithValue("@categoria", libro.Categoria);
                    cmd.Parameters.AddWithValue("@editorial", libro.Editorial);
                    cmd.Parameters.AddWithValue("@stock", libro.Stock);
                    cmd.Parameters.AddWithValue("@descripcion", libro.Descripcion);
                    cmd.Parameters.AddWithValue("@añopublicacion", libro.AñoPublicaion);
                    cmd.ExecuteNonQuery();
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
    }
}
