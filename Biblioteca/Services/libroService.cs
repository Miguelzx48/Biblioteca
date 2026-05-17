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
    public class libroService
    {
        public void CrearLibro(libro libro)
        {
            LibroDb libroDb = new   LibroDb();
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
        public List<libro> ObtenerLibros()
        {
            LibroDb libroDb = new LibroDb();
            return libroDb.ObtenerTodos();
        }
    }
}

