using Biblioteca.Db;
using Biblioteca.Models;
using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Services
{
    public class UsuarioServicio
    {
        private readonly UsuarioDb usuarioDb;
        public UsuarioServicio()
        {
            usuarioDb = new UsuarioDb();
        }

        public void ValidarAdminDefault()
        {
            if (!usuarioDb.ExisteUsuarioAdminDefault())
            {
                usuarioDb.CrearUsuarioAdminDefault();
            }
        }

        public void Crear(Usuario usuario)
        {
            usuarioDb.CrearUsuario(usuario);
        }

        public List<Usuario> ObtenerTodos()
        {
            return usuarioDb.Obtener();  
        }

        public Usuario ObtenerPorId(int idUsuario)
        {
            return usuarioDb.ObtenerPorId(idUsuario);
        }

        public void Actualizar(Usuario usuario)
        {
            usuarioDb.Actualizar(usuario);
        }

        public void Eliminar(int idUsuario)
        {
            usuarioDb.Eliminar(idUsuario);
        }
    }
}
