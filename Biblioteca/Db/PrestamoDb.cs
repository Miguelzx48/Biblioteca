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
        public bool CrearPrestamo(Prestamo prestamo)
        {
            string query = @" INSERT INTO Prestamos (
        IdUsuario, IdLibro, FechaPrestamo, FechaEstimadaDevolucion, Estado, Multa, CantidadDiasMora)
    VALUES ( @idUsuario, @idLibro, @fechaPrestamo, @fechaEstimadaDevolucion, @estado, @multa, @cantidadDiasMora )";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                using (MySqlCommand stmt = new MySqlCommand(query, conn))
                {
                    stmt.Parameters.AddWithValue("@idUsuario", prestamo.IdUsuario);
                    stmt.Parameters.AddWithValue("@idLibro", prestamo.IdLibro);
                    stmt.Parameters.AddWithValue("@fechaPrestamo", prestamo.FechaPrestamo);
                    stmt.Parameters.AddWithValue("@fechaEstimadaDevolucion", prestamo.FechaEstimadaDevolucion);
                    stmt.Parameters.AddWithValue("@estado", prestamo.Estado);
                    stmt.Parameters.AddWithValue("@multa", prestamo.Multa);
                    stmt.Parameters.AddWithValue("@cantidadDiasMora", prestamo.CantidadDiasMora);

                    int result = stmt.ExecuteNonQuery();

                    return result > 0;
                }
            }
        }
        public bool ActualizarMultas(Prestamo prestamo)
        {
            string query = @" UPDATE Prestamos SET Multa = @multa, CantidadDiasMora = @cantidadDiasMora WHERE IdPrestamo = @idPrestamo";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                using (MySqlCommand stmt = new MySqlCommand(query, conn))
                {
                    stmt.Parameters.AddWithValue("@multa", prestamo.Multa);
                    stmt.Parameters.AddWithValue("@cantidadDiasMora", prestamo.CantidadDiasMora);
                    stmt.Parameters.AddWithValue("@idPrestamo", prestamo.IdPrestamo);

                    int result = stmt.ExecuteNonQuery();

                    return result > 0;
                }
            }
        }
        public List<Prestamo> LeerListaPrestamos()
        {
            List<Prestamo> lista = new List<Prestamo>();

            string query = @" SELECT * FROM Prestamos";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                using (MySqlCommand stmt = new MySqlCommand(query, conn))
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
                            pre.FechaRealDevolucion = result.IsDBNull(result.GetOrdinal("FechaRealDevolucion")) ? (DateTime?)null : result.GetDateTime("FechaRealDevolucion");
                            pre.Multa = result.GetDouble("Multa");
                            pre.Estado = result.GetString("Estado");
                            pre.CantidadDiasMora = result.GetInt32("CantidadDiasMora");

                            lista.Add(pre);
                        }
                    }
                }
            }

            return lista;
        }
        public Prestamo LeerPrestamoPorId(int idPrestamo)
        {
            Prestamo pre = null;

            string query = @" SELECT * FROM Prestamos WHERE IdPrestamo = @idPrestamo";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                using (MySqlCommand stmt = new MySqlCommand(query, conn))
                {
                    stmt.Parameters.AddWithValue("@idPrestamo", idPrestamo);

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
                            pre.FechaRealDevolucion = result.IsDBNull(result.GetOrdinal("FechaRealDevolucion")) ? (DateTime?)null : result.GetDateTime("FechaRealDevolucion");
                            pre.Multa = result.GetDouble("Multa");
                            pre.Estado = result.GetString("Estado");
                            pre.CantidadDiasMora = result.GetInt32("CantidadDiasMora");
                        }
                    }
                }
            }

            return pre;
        }
        public bool EditarPrestamo(int idPrestamo, Prestamo prestamo)
        {
            string query = @" UPDATE Prestamos SET FechaPrestamo = @fechaPrestamo, FechaEstimadaDevolucion = @fechaEstimadaDevolucion,
        FechaRealDevolucion = @fechaRealDevolucion, Multa = @multa, Estado = @estado, CantidadDiasMora = @cantidadDiasMora,
        IdUsuario = @idUsuario, IdLibro = @idLibro WHERE IdPrestamo = @idPrestamo";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                using (MySqlCommand stmt = new MySqlCommand(query, conn))
                {
                    stmt.Parameters.AddWithValue("@idPrestamo", idPrestamo);
                    stmt.Parameters.AddWithValue("@fechaPrestamo", prestamo.FechaPrestamo);
                    stmt.Parameters.AddWithValue("@fechaEstimadaDevolucion", prestamo.FechaEstimadaDevolucion);
                    stmt.Parameters.AddWithValue("@fechaRealDevolucion", prestamo.FechaRealDevolucion);
                    stmt.Parameters.AddWithValue("@multa", prestamo.Multa);
                    stmt.Parameters.AddWithValue("@estado", prestamo.Estado);
                    stmt.Parameters.AddWithValue("@cantidadDiasMora", prestamo.CantidadDiasMora);
                    stmt.Parameters.AddWithValue("@idUsuario", prestamo.IdUsuario);
                    stmt.Parameters.AddWithValue("@idLibro", prestamo.IdLibro);

                    int result = stmt.ExecuteNonQuery();

                    return result > 0;
                }
            }
        }
        public bool EliminarPrestamo(int idPrestamo)
        {
            string query = @" UPDATE Prestamos SET Estado = 'Eliminado' WHERE IdPrestamo = @idPrestamo";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                using (MySqlCommand stmt = new MySqlCommand(query, conn))
                {
                    stmt.Parameters.AddWithValue("@idPrestamo", idPrestamo);

                    int result = stmt.ExecuteNonQuery();

                    return result > 0;
                }
            }
        }
        public List<Grafico> PrestamosPorMes()
        {
            List<Grafico> lista = new List<Grafico>();

            string query = @"SELECT MONTHNAME(FechaPrestamo) AS Nombre, COUNT(IdPrestamo) AS Cantidad
                            FROM Prestamos GROUP BY MONTH(FechaPrestamo), MONTHNAME(FechaPrestamo) ORDER BY MONTH(FechaPrestamo)";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                using (MySqlCommand stmt = new MySqlCommand(query, conn))
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

            return lista;
        }
        public List<Grafico> MultasPorMes()
        {
            List<Grafico> lista = new List<Grafico>();

            string query = @"SELECT MONTHNAME(FechaPrestamo) AS Nombre, SUM(Multa) AS Cantidad FROM Prestamos
                            WHERE Multa > 0 GROUP BY MONTH(FechaPrestamo), MONTHNAME(FechaPrestamo) 
                            ORDER BY MONTH(FechaPrestamo)";

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
        public List<Prestamo> BuscarPrestamo(string texto)
        {
            List<Prestamo> lista = new List<Prestamo>();

            string query = @"SELECT * FROM Prestamos WHERE 
                           CAST(IdUsuario AS CHAR) LIKE @texto
                           OR CAST(IdLibro AS CHAR) LIKE @texto
                           OR Estado LIKE @texto";

            using (MySqlConnection conn =
                new MySqlConnection(connectionString))
            {
                conn.Open();

                using (MySqlCommand stmt =
                    new MySqlCommand(query, conn))
                {
                    stmt.Parameters.AddWithValue(
                        "@texto",
                        "%" + texto + "%");

                    using (MySqlDataReader result =
                        stmt.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            Prestamo pre = new Prestamo();

                            pre.IdPrestamo =
                                result.GetInt32("IdPrestamo");

                            pre.IdUsuario =
                                result.GetInt32("IdUsuario");

                            pre.IdLibro =
                                result.GetInt32("IdLibro");

                            pre.FechaPrestamo =
                                result.GetDateTime("FechaPrestamo");

                            pre.FechaEstimadaDevolucion =
                                result.GetDateTime("FechaEstimadaDevolucion");

                            pre.FechaRealDevolucion = result.IsDBNull(result.GetOrdinal("FechaRealDevolucion")) ? (DateTime?)null : result.GetDateTime("FechaRealDevolucion");

                            pre.Multa = result.GetInt32("Multa");

                            pre.Estado =
                                result.GetString("Estado");

                            pre.CantidadDiasMora =
                                result.GetInt32("CantidadDiasMora");

                            lista.Add(pre);
                        }
                    }
                }
            }

            return lista;
        }

    }
}
