using Biblioteca.Db;
using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace Biblioteca.Services
{
    public class PrestamoService
    {

        PrestamoDb prestamoDb = new PrestamoDb();

        public PrestamoService() { }
        public bool CrearPrestamo(Prestamo prestamo)
        {


            return prestamoDb.CrearPrestamo(prestamo);
        }

        public bool ActualizarMultas(Prestamo prestamo)
        {


            return prestamoDb.ActualizarMultas(prestamo);
        }

        public List<Prestamo> LeerListaPrestamos()
        {

            return prestamoDb.LeerListaPrestamos();
        }

        public Prestamo LeerPrestamoPorId(int idPrestamo)
        {


            return prestamoDb.LeerPrestamoPorId(idPrestamo);
        }

        public bool EditarPrestamo(int idPrestamo, Prestamo prestamo)
        {

            return prestamoDb.EditarPrestamo(idPrestamo, prestamo);
        }

        public bool EliminarPrestamo(int idPrestamo)
        {

            return prestamoDb.EliminarPrestamo(idPrestamo);
        }

        public List<Grafico> PrestamosPorMes()
        {

            return prestamoDb.PrestamosPorMes();
        }

        public List<Grafico> MultasPorMes()
        {
            return prestamoDb.MultasPorMes();
        }

        public bool ValidarPrestamo(int idUsuario)
        {
            PrestamoDb prestamoDb = new PrestamoDb();

            List<Prestamo> lista = prestamoDb.LeerListaPrestamos();

            foreach (Prestamo prestamo in lista)
            {
                if (prestamo.IdUsuario == idUsuario && prestamo.Estado == "Activo")
                {
                    return true;
                }
            }

            return false;
        }

        public libro UsuarioPrestamo(Prestamo prestamo)
        {
            LibroDb libroDb = new LibroDb();

            PrestamoDb prestamoDb =
                new PrestamoDb();

            libro lib = libroDb.LeerLibroPorId(prestamo.IdLibro);

            if (lib.Stock > 0 && ValidarPrestamo(prestamo.IdUsuario) == false)
            {
                prestamo.FechaPrestamo = DateTime.Now;

                prestamo.FechaEstimadaDevolucion = DateTime.Now.AddDays(15);

                prestamo.Estado = "Activo";

                prestamo.Multa = 0;

                prestamo.CantidadDiasMora = 0;

                lib.Stock--;

                libroDb.DescontarStock(lib.IdLibro, lib.Stock);

                prestamoDb.CrearPrestamo(prestamo);

                return lib;
            }

            return null;
        }
        public void ActualizarMultasUsuario(int idUsuario)
        {
            List<Prestamo> prestamos =
                prestamoDb.LeerListaPrestamos();

            foreach (Prestamo prestamo in prestamos)
            {
                if (
                    prestamo.IdUsuario == idUsuario &&
                    prestamo.Estado == "Activo"
                )
                {
                    CalcularMultas(prestamo);
                }
            }
        }
        public Prestamo CalcularMultas(Prestamo prestamo)
        {
            PrestamoDb prestamoDb =
                new PrestamoDb();

            if (
                prestamo.Estado == "Activo" &&
                DateTime.Now >
                prestamo.FechaEstimadaDevolucion
            )
            {
                prestamo.CantidadDiasMora =
                    (
                        DateTime.Now -
                        prestamo.FechaEstimadaDevolucion
                    ).Days;

                prestamo.Multa =
                    prestamo.CantidadDiasMora * 5000;

                prestamoDb.ActualizarMultas(prestamo);
            }

            return prestamo;
        }

        public bool DevolverPrestamo(Prestamo prestamo)
        {
            LibroDb libroDb = new LibroDb();

            PrestamoDb prestamoDb =
                new PrestamoDb();

            libro lib =
                libroDb.LeerLibroPorId(prestamo.IdLibro);

            if (
                lib != null &&
                prestamo.Estado == "Activo"
            )
            {
                prestamo.FechaRealDevolucion =
                    DateTime.Now;

                CalcularMultas(prestamo);

                prestamo.Estado = "Devuelto";

                lib.Stock++;

                libroDb.AumentarStock(
                    lib.IdLibro,
                    lib.Stock
                );

                return prestamoDb.EditarPrestamo(
                    prestamo.IdPrestamo,
                    prestamo
                );
            }

            return false;
        }
        public List<Prestamo> BuscarPrestamo(string texto)
        {
            return prestamoDb.BuscarPrestamo(texto);
        }


    }
}