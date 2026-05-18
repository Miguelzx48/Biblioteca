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
        public void Actulizar(Prestamo prestamo)
        {
            PrestamoDb prestamoDb = new PrestamoDb();
            prestamoDb.Actualizar(prestamo);
        }
    }
}