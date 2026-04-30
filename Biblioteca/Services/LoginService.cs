using Biblioteca.Db;
using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Services
{
    public class LoginService
    {
        public Usuario Login(string email, string password)
        {
            LoginDb db = new LoginDb();
            return db.Login(email, password);
        }

        public void CambiarPassword(int idUsuario, string password)
        {
            LoginDb db = new LoginDb();
            db.CambiarPassword(idUsuario, password);
        }
    }
}
