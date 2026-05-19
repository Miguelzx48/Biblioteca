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

        public string RecuperarPassword(string email)
        {
            LoginService service = new LoginService();
            return service.RecuperarPassword(email);
        }

        public string CambiarPassword(string codigoRecuperacion, string email, string password)
        {
            LoginService service = new LoginService();
            return service.CambiarPassword(codigoRecuperacion, email, password);
        }
    }
}
