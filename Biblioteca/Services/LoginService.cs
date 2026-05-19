
using Biblioteca.Db;
using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Services
{
    public class LoginService
    {
        private string codigoRecuperacion;
        private DateTime expiracionCodigo;
        public Usuario Login(string email, string password)
        {
            LoginDb db = new LoginDb();
            return db.Login(email, password);
        }

        public string CambiarPassword(string codigoRecuperacion, string email, string password)
        {
            if (codigoRecuperacion != this.codigoRecuperacion)
            {
                return "El código de recuperación es incorrecto.";
            }


            if (DateTime.Now > expiracionCodigo)
            {
                return "El código de recuperación ha expirado.";
            }

            LoginDb db = new LoginDb();
            db.CambiarPassword(email, password);

            return "Contraseña cambiada exitosamente.";
        }

        public string RecuperarPassword(string email)
        {
            LoginDb db = new LoginDb();
            if (!db.ValidarCorreo(email))
            {
                return "El correo electrónico no está registrado.";
            }

            var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new System.Net.NetworkCredential("miguelzx.48@gmail.com", "hncxdvnlosxlbycp"),
                EnableSsl = true
            };

            GenerarCodigoRecuperacion();

            var mensaje = new MailMessage("send@biblioteca.com", email)
            {
                Subject = "Recuperación de contraseña",
                Body = $"Tu código de recuperación es: {codigoRecuperacion}"
            };

            smtp.Send(mensaje);

            return "Se ha enviado un correo con el código de recuperación.";
        }

        private void GenerarCodigoRecuperacion()
        {
            codigoRecuperacion = new Random().Next(100000, 999999).ToString();
            expiracionCodigo = DateTime.Now.AddMinutes(15);
        }
    }
}
