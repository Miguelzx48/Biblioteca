using Biblioteca.Db;
using Biblioteca.Models;
using Biblioteca.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Controllers
{
    public class UsuarioController
    {
        public void ValidarAdminDefault()
        {
            UsuarioServicio usuarioServicio = new UsuarioServicio();
            usuarioServicio.ValidarAdminDefault();
        }

        public void Crear(Usuario usuario)
        {
            UsuarioServicio usuarioServicio = new UsuarioServicio();
            usuarioServicio.Crear(usuario);
        }

        public List<Usuario> ObtenerTodos()
        {
            UsuarioServicio usuarioServicio = new UsuarioServicio();
            return usuarioServicio.ObtenerTodos();
        }

        public Usuario ObtenerPorId(int idUsuario)
        {
            UsuarioServicio usuarioServicio = new UsuarioServicio();
            return usuarioServicio.ObtenerPorId(idUsuario);
        }

        public void Actualizar(Usuario usuario)
        {
            UsuarioServicio usuarioServicio = new UsuarioServicio();
            usuarioServicio.Actualizar(usuario);
        }

        public void Eliminar(int idUsuario)
        {
            UsuarioServicio usuarioServicio = new UsuarioServicio();
            usuarioServicio.Eliminar(idUsuario);
        }
    }
}
