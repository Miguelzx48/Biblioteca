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
        PrestamoService service = new PrestamoService();

        public bool CrearPrestamo(Prestamo prestamo)
        {

            return service.CrearPrestamo(prestamo);
        }

        public bool ActualizarMultas(Prestamo prestamo)
        {

            return service.ActualizarMultas(prestamo);
        }

        public List<Prestamo> LeerListaPrestamos()
        {

            return service.LeerListaPrestamos();
        }

        public Prestamo LeerPrestamoPorId(int idPrestamo)
        {

            return service.LeerPrestamoPorId(idPrestamo);
        }

        public bool EditarPrestamo(int idPrestamo, Prestamo prestamo)
        {

            return service.EditarPrestamo(idPrestamo, prestamo);
        }

        public bool EliminarPrestamo(int idPrestamo)
        {

            return service.EliminarPrestamo(idPrestamo);
        }

        public List<Grafico> PrestamosPorMes()
        {

            return service.PrestamosPorMes();
        }

        public List<Grafico> MultasPorMes()
        {

            return service.MultasPorMes();
        }

        public bool ValidarPrestamo(int idUsuario)
        {

            return service.ValidarPrestamo(idUsuario);
        }

        public libro UsuarioPrestamo(Prestamo prestamo)
        {

            return service.UsuarioPrestamo(prestamo);
        }

        public Prestamo CalcularMultas(Prestamo prestamo)
        {

            return service.CalcularMultas(prestamo);
        }

        public bool DevolverPrestamo(Prestamo prestamo)
        {
            return service.DevolverPrestamo(prestamo);
        }

        public List<Prestamo> BuscarPrestamo(string texto)
        {
            return service.BuscarPrestamo(texto);
        }
    }
}
