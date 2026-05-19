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
        public void CrearPrestamo(Prestamo prestamo)
        {
            PrestamoDb prestamoDb = new PrestamoDb();
            prestamoDb.CrearPrestamo(prestamo);
        }
        public void Actualizar(Prestamo prestamo)
        {
            PrestamoDb prestamoDb = new PrestamoDb();
            prestamoDb.Actualizar(prestamo);
        }
        public Prestamo CalcularMultas(Prestamo prestamo)
        {
            PrestamoDb prestamoDb = new PrestamoDb();
            
            if (DateTime.Now > prestamo.FechaEstimadaDevolucion)
            {
                prestamo.CantidadDiasMora = (DateTime.Now - prestamo.FechaEstimadaDevolucion).Days;
                prestamo.Multa = prestamo.CantidadDiasMora * 5000;
            }
            else
            {
                prestamo.CantidadDiasMora = 0;
                prestamo.Multa = 0;
            }

            prestamoDb.Actualizar(prestamo);

            return prestamo;
        }
        public bool DevolverPrestamo(Prestamo prestamo)
        {
            PrestamoDb prestamoDb = new PrestamoDb();
            LibroDb libroDb = new LibroDb();
            libro libro = libroDb.LeerLibroPorId(prestamo.IdLibro);

            if (prestamo.Estado == 0)
            {
                prestamo.FechaRealDevolucion = DateTime.Now;
                prestamo.Estado = 1;

                libro.Stock++;

                libroDb.ActualizarStock(libro.IdLibro, libro.Stock);

                prestamoDb.Actualizar(prestamo);
            }

            return false;
        }
        public List<Grafico> PrestamosPorMes()
        {
            PrestamoDb prestamoDb = new PrestamoDb();
            return prestamoDb.PrestamosPorMes();
        }
        public List<Grafico> MultasPorMes()
        {
            PrestamoDb prestamoDb = new PrestamoDb();
            return prestamoDb.MultasPorMes();
        }
        public Prestamo LeerPrestamoPorId(int idPrestamo)
        {
            PrestamoDb prestamoDb = new PrestamoDb();
            return prestamoDb.LeerPrestamoPorId(idPrestamo);
        }
        public List<Prestamo> LeerListaPrestamos()
        {
            PrestamoDb prestamoDb = new PrestamoDb();
            return prestamoDb.LeerListaPrestamos();
        }
        public void Eliminar(int idPrestamo)
        {
            PrestamoDb prestamoDb = new PrestamoDb();
            prestamoDb.Eliminar(idPrestamo);
        }
}