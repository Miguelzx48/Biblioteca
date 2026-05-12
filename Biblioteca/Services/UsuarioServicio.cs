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
        public void Crear(Usuario usuario)
        {
            UsuarioDb usuarioDb = new UsuarioDb();
            usuarioDb.CrearUsuario(usuario);
        }

        public List<Usuario> ObtenerTodos()
        {
            UsuarioDb usuarioDb = new UsuarioDb();
            return usuarioDb.Obtener();
        }

        public Usuario ObtenerPorId(int idUsuario)
        {
            UsuarioDb usuarioDb = new UsuarioDb();
            return usuarioDb.ObtenerPorId(idUsuario);
        }

        public void Actualizar(Usuario usuario)
        {
            UsuarioDb usuarioDb = new UsuarioDb();
            usuarioDb.Actualizar(usuario);
        }

        public void Eliminar(int idUsuario)
        {
            UsuarioDb usuarioDb = new UsuarioDb();
            usuarioDb.Eliminar(idUsuario);
        }
    }
}
