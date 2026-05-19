using Biblioteca.Db;
using Biblioteca.Models;
using Biblioteca.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Controllers
{
    public class PrestamoController
    {

        private readonly PrestamoService servicioPrestamo;

        public PrestamoController()
        {
            servicioPrestamo = new PrestamoService();
        }
        public void CrearPrestamo(Prestamo prestamo)
        {
            servicioPrestamo.CrearPrestamo(prestamo);
        }
        public void Actualizar(Prestamo prestamo)
        {
            servicioPrestamo.Actualizar(prestamo);
        }

        public Prestamo CalcularMultas(Prestamo prestamo)
        {
            return servicioPrestamo.CalcularMultas(prestamo);
        }

        public bool DevolverPrestamo(Prestamo prestamo)
        {
            return servicioPrestamo.DevolverPrestamo(prestamo);
        }

        public List<Prestamo> LeerListaPrestamos()
        {
            return servicioPrestamo.LeerListaPrestamos();
        }

        public Prestamo LeerPrestamoPorId(int idPrestamo)
        {
            return servicioPrestamo.LeerPrestamoPorId(idPrestamo);
        }

        public void EliminarPrestamo(int idPrestamo)
        {
            servicioPrestamo.Eliminar(idPrestamo);
        }

        public List<Grafico> PrestamosPorMes()
        {
            return servicioPrestamo.PrestamosPorMes();
        }

        public List<Grafico> MultasPorMes()
        {
            return servicioPrestamo.MultasPorMes();
        }
    }
}
