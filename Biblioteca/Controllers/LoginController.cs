using Biblioteca.Models;
using Biblioteca.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Controllers
{
    public class LoginController
    {
        public Usuario Login(string email, string password)
        {
            LoginService service = new LoginService();
            return service.Login(email, password);
        }

        public void CambiarPassword(int idUsuario, string password)
        {
            LoginService service = new LoginService();
            service.CambiarPassword(idUsuario, password);
        }
    }
}
