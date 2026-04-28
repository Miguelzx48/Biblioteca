using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int TipoUsuario { get; set; }
        public DateTime FechaNacimiento { get; }
        public bool Activo { get; set; }

        public Usuario(int id, string email, string password, string nombres, string apellidos, int tipoUsuario, DateTime fechaNacimiento, bool activo)
        {
            Id = id;
            Email = email;
            Password = password;
            Nombres = nombres;
            Apellidos = apellidos;
            TipoUsuario = tipoUsuario;
            FechaNacimiento = fechaNacimiento;
            Activo = activo;
        }
    }
}
