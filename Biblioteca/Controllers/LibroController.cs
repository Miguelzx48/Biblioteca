using Biblioteca.Db;
using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Services;

namespace Biblioteca.Controllers
{
    public class LibroController
    {

        libroService servicio = new libroService();
        public void CrearLibro(libro libro)
        {
           servicio.CrearLibro(libro);
        }

        public void Actualizar(libro libro)
        {
              servicio.Actualizar(libro);
        }

        public void Eliminar(int idLibro)
        {
            servicio.Eliminar(idLibro);
        }

        public void ProbarConexion()
        {
            servicio.ProbarConexion();
        }

        public List<libro> ObtenerTodos()
        {
            return servicio.ObtenerTodos();
        }

        public libro LeerLibroPorId(int idLibro)
        {
            return servicio.LeerLibroPorId(idLibro);
        }

        public bool DescontarStock(int idLibro, int stock)
        {
            return servicio.DescontarStock(idLibro, stock);
        }

        public bool AumentarStock(int idLibro, int stock)
        {
            return servicio.AumentarStock(idLibro, stock);
        }

        public List<libro> BuscadorLibros(string texto)
        {
            return servicio.BuscadorLibros(texto);
        }

        public List<Grafico> LibrosMasPrestadosPorMes()
        {
            return servicio.LibrosMasPrestadosPorMes();
        }
    }
}
