using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySqlConnector;
using MySqlConnection = MySqlConnector.MySqlConnection;
using MySqlCommand = MySql.Data.MySqlClient.MySqlCommand;

namespace Biblioteca.Db
{
    internal class PrestamoDb
    {
        private readonly string connectionString = "";
        public PrestamoDb() { }
        public void CrearPrestamo(Prestamo prestamo)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    Insert INTO Prestamos (IdPrestamo,IdUsuario,IdLibro, FechaPrestamo, FechaEstimadaDevolucion,FechaRealDevolucion,Multa,CantidadDiasMora,Estado )
                    VALUES ( @idPrestamos ,@idUsuario ,@idLibro, @fechaPrestamo,@fechaEstimadaDevolucion ,@fechaRealDevolucion,@multa,@cantiadDiasMora,@estado  )";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idPrestamos ", prestamo.IdPrestamo);
                    cmd.Parameters.AddWithValue("@idUsuario",prestamo.IdUsuario );
                    cmd.Parameters.AddWithValue("@idLibro", prestamo.IdLibro);
                    cmd.Parameters.AddWithValue("@fechaPrestamo", prestamo.FechaPrestamo);
                    cmd.Parameters.AddWithValue("@fechaEstimadaDevolucio",prestamo.FechaEstimadaDevolucion);
                    cmd.Parameters.AddWithValue("@fechaRealDevolucion", prestamo.FechaRealDevolucion);
                    cmd.Parameters.AddWithValue("@multa", prestamo.Multa);
                    cmd.Parameters.AddWithValue("@cantiadDiasMora", prestamo.CantidadDiasMora);
                    cmd.Parameters.AddWithValue("@estado", prestamo.Estado);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    
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
                    cmd.Parameters.AddWithValue("@stok", libro.Stock);
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
                    cmd.Parameters.AddWithValue("@idLibro", idLibro);
                    cmd.ExecuteNonQuery();
                }
            }
        }
       
    }
}
