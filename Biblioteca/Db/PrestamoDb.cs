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
    public class PrestamoDb
    {
        private readonly string connectionString = "server=localhost;database=Biblioteca;user=root;password=123;";
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
                string query = @"UPDATE Prestamos SET Estado = @estado,Multa = @multa,CantidadDiasMora = @cantidadDiasMora,FechaRealDevolucion = @fechaRealDevolucion
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

        public bool ValidarPrestamo(int idUsuario)
        {
            string sql = @"SELECT * FROM Prestamos WHERE IdUsuario = @IdUsuario AND Estado = 'Activo'";

            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                        using (MySqlDataReader result = cmd.ExecuteReader())
                        {
                            if (result.Read())
                            {
                                return true;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return false;
        }

        public List<Prestamo> LeerListaPrestamos()
        {
            MySqlConnection conexion = new MySqlConnection(connectionString);

            string sql = "SELECT * FROM Prestamos";
            List<Prestamo> lstPrestamo = new List<Prestamo>();

            try
            {
                conexion.Open();
                using (MySqlCommand stmt = new MySqlCommand(sql, conexion))
                {
                    using (MySqlDataReader result = stmt.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            Prestamo pre = new Prestamo();

                            pre.IdPrestamo = result.GetInt32("IdPrestamo");
                            pre.IdUsuario = result.GetInt32("IdUsuario");
                            pre.IdLibro = result.GetInt32("IdLibro");
                            pre.FechaPrestamo = result.GetDateTime("FechaPrestamo");
                            pre.FechaEstimadaDevolucion = result.GetDateTime("FechaEstimadaDevolucion");
                            pre.FechaRealDevolucion = result.GetDateTime("FechaRealDevolucion");
                            pre.Multa = result.GetInt32("Multa");
                            pre.Estado = result.GetInt32("Estado");
                            pre.CantidadDiasMora = result.GetInt32("CantidadDiasMora");
                            lstPrestamo.Add(pre);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al leer préstamos: " + e.Message);
            }
            finally
            {
                conexion.Close();
            }

            return lstPrestamo;
        }

        public Prestamo LeerPrestamoPorId(int idPrestamo)
        {
            Prestamo pre = null;
            string sql = "SELECT * FROM Prestamos WHERE IdPrestamo = @IdPrestamo";

            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();
                    using (MySqlCommand stmt = new MySqlCommand(sql, conexion))
                    {
                        stmt.Parameters.AddWithValue("@IdPrestamo", idPrestamo);
                        using (MySqlDataReader result = stmt.ExecuteReader())
                        {
                            if (result.Read())
                            {
                                pre = new Prestamo();

                                pre.IdPrestamo = result.GetInt32("IdPrestamo");
                                pre.IdUsuario = result.GetInt32("IdUsuario");
                                pre.IdLibro = result.GetInt32("IdLibro");
                                pre.FechaPrestamo = result.GetDateTime("FechaPrestamo");
                                pre.FechaEstimadaDevolucion = result.GetDateTime("FechaEstimadaDevolucion");
                                pre.FechaRealDevolucion = result.GetDateTime("FechaRealDevolucion");
                                pre.Multa = result.GetInt32("Multa");
                                pre.Estado = result.GetInt32("Estado");
                                pre.CantidadDiasMora = result.GetInt32("CantidadDiasMora");
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error al leer préstamo: " + e.Message);
                }
                finally
                {
                    conexion.Close();
                }
            }

            return pre;
        }

        public List<Grafico> PrestamosPorMes()
        {

            List<Grafico> lista = new List<Grafico>();

            string sql = @"SELECT MONTHNAME(FechaPrestamo) AS Nombre, COUNT(IdPrestamo) AS Cantidad FROM Prestamos GROUP BY MONTH(FechaPrestamo), MONTHNAME(FechaPrestamo) ORDER BY MONTH(FechaPrestamo)";

            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();
                    using (MySqlCommand stmt = new MySqlCommand(sql, conexion))
                    { 
                        using (MySqlDataReader result = stmt.ExecuteReader())
                        {
                            while (result.Read())
                            {
                                Grafico graf = new Grafico();

                                graf.Nombre = result.GetString("Nombre");
                                graf.Cantidad = result.GetDouble("Cantidad");

                                lista.Add(graf);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error al cargar préstamos por mes: " + e.Message);
                }
            }

            return lista;
        }

        public List<Grafico> MultasPorMes()
        {
            List<Grafico> lista = new List<Grafico>();

            string sql = @"SELECT MONTHNAME(FechaPrestamo) AS Nombre, SUM(Multa) AS Cantidad FROM Prestamos WHERE Multa > 0 GROUP BY MONTH(FechaPrestamo), MONTHNAME(FechaPrestamo) ORDER BY MONTH(FechaPrestamo)";

            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conexion))
                    {
                        using (MySqlDataReader result = cmd.ExecuteReader())
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
                catch (Exception e)
                {
                    Console.WriteLine("Error al cargar multas por mes: " + e.Message);
                }
            }
            return lista;
        }
    }
}
