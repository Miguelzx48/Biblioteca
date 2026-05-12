using Biblioteca.Db;
using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Controllers
{
    internal class LibroController
    {
        public void CrearLibro(libro libro)
        {
            LibroDb libroDb = new LibroDb();
            libroDb.CrearLibro(libro);
        }

        public void Actualizar(libro libro)
        {
            LibroDb libroDb = new LibroDb();
            libroDb.Actualizar(libro);
        }

        public void Eliminar(int idLibro)
        {
            LibroDb libroDb = new LibroDb();
            libroDb.Eliminar(idLibro);
        }
    }
}
