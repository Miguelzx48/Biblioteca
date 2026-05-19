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
        public void ProbarConexion()
        {
            LibroDb libroDb = new LibroDb();

            libroDb.ProbarConexion();
        }
        public List<libro> ObtenerTodos()
        {
            LibroDb libroDb = new LibroDb();

            return libroDb.ObtenerTodos();
        }
        public libro LeerLibroPorId(int idLibro)
        {
            LibroDb libroDb = new LibroDb();

            return libroDb.LeerLibroPorId(idLibro);
        }
        public bool DescontarStock(int idLibro, int stock)
        {
            LibroDb libroDb = new LibroDb();

            return libroDb.DescontarStock(idLibro, stock);
        }
        public bool AumentarStock(int idLibro, int stock)
        {
            LibroDb libroDb = new LibroDb();

            return libroDb.AumentarStock(idLibro, stock);
        }
        public List<libro> BuscadorLibros(string texto)
        {
            LibroDb libroDb = new LibroDb();

            return libroDb.BuscadorLibros(texto);
        }
        public List<Grafico> LibrosMasPrestadosPorMes()
        {
            LibroDb libroDb = new LibroDb();

            return libroDb.LibrosMasPrestadosPorMes();
        }
    }
}

