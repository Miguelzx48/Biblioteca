using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Biblioteca.Views.Usuario
{
    /// <summary>
    /// Lógica de interacción para PerfilUsuario.xaml
    /// </summary>
    public partial class PerfilUsuario : UserControl
    {
        Biblioteca.Models.Usuario usuarioActual;

        public PerfilUsuario(Biblioteca.Models.Usuario usuario)
        {
            InitializeComponent();

            usuarioActual = usuario;

            CargarDatos();
        }

        private void CargarDatos()
        {
            txtNombres.Text = usuarioActual.Nombres;
            txtApellidos.Text = usuarioActual.Apellidos;
            txtEmail.Text = usuarioActual.Email;
            txtTelefono.Text = usuarioActual.Telefono;

            txtFechaNacimiento.Text =
                usuarioActual.FechaNacimiento.ToString("yyyy-MM-dd");

            txtActivo.Text =
                usuarioActual.Activo ? "Activo" : "Inactivo";
        }
    }
}
