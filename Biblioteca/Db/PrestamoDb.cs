using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


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
                    cmd.Parameters.AddWithValue("@idPrestamos", prestamo.IdPrestamo);
                    cmd.Parameters.AddWithValue("@idUsuario",prestamo.IdUsuario );
                    cmd.Parameters.AddWithValue("@idLibro", prestamo.IdLibro);
                    cmd.Parameters.AddWithValue("@fechaPrestamo", prestamo.FechaPrestamo);
                    cmd.Parameters.AddWithValue("@fechaEstimadaDevolucion",prestamo.FechaEstimadaDevolucion);
                    cmd.Parameters.AddWithValue("@fechaRealDevolucion", prestamo.FechaRealDevolucion);
                    cmd.Parameters.AddWithValue("@multa", prestamo.Multa);
                    cmd.Parameters.AddWithValue("@cantiadDiasMora", prestamo.CantidadDiasMora);
                    cmd.Parameters.AddWithValue("@estado", prestamo.Estado);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Actualizar(Prestamo prestamo)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
            UPDATE Prestamos 
            SET Estado = @estado,
                Multa = @multa,
                CantidadDiasMora = @cantidadDiasMora,
                FechaRealDevolucion = @fechaRealDevolucion
            WHERE IdPrestamo = @idPrestamo";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@estado", prestamo.Estado);
                    cmd.Parameters.AddWithValue("@multa", prestamo.Multa);
                    cmd.Parameters.AddWithValue("@cantidadDiasMora", prestamo.CantidadDiasMora);
                    cmd.Parameters.AddWithValue("@fechaRealDevolucion", prestamo.FechaRealDevolucion);
                    cmd.Parameters.AddWithValue("@idPrestamo", prestamo.IdPrestamo);

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
