using Biblioteca.Controllers;
using System;
using System.Windows;


namespace Biblioteca.Views.Windows
{
    /// <summary>
    /// Lógica de interacción para CrearUsuario.xaml
    /// </summary>
    public partial class CrearUsuario : Window
    {
        UsuarioController controller;
        Models.Usuario usuario;
        int tipoUsuario;
        bool esEdicion = false;

        public CrearUsuario()
        {
            InitializeComponent();
            controller = new UsuarioController();
            Title = "Crear Usuario";
        }

        public CrearUsuario(int tipousuario)
        {
            InitializeComponent();
            controller = new UsuarioController();
            Title = tipoUsuario == 0 ? "Crear Usuario" : "Crear Administrador";
            tipoUsuario = tipousuario;
        }

        public CrearUsuario(Models.Usuario usuario, int tipoUsuarioEdita)
        {
            InitializeComponent();
            controller = new UsuarioController();
            Title = usuario.TipoUsuario == 0 ? "Actualizar Usuario" : "Actualizar Administrador";
            esEdicion = true;
            this.usuario = usuario;
            DisableControlsEdition(tipoUsuarioEdita);
            SetControlsValues();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (esEdicion)
            {
                Actualizar();
            }
            else
            {
                Crear();
            }
        }

        private void Crear()
        {
            string email = txtEmail.Text;
            string password = txtPassword.Password;
            string nombres = txtNombres.Text;
            string apellidos = txtApellidos.Text;
            string telefono = txtTelefono.Text;
            DateTime fechaNacimiento = DateTime.Parse(dtpckFechaNacimiento.Text);

            usuario = new Models.Usuario(email, password, nombres, apellidos, tipoUsuario, fechaNacimiento, telefono, true);

            controller.Crear(usuario);
        }

        private void Actualizar()
        {
            usuario.Password = txtPassword.Password;
            usuario.Telefono = txtTelefono.Text;

            controller.Actualizar(usuario);
        }

        private void DisableControlsEdition(int tipousuarioedita)
        {
            if(tipousuarioedita == 1)
            {
                chkActivo.Visibility = Visibility.Visible;
            }

            txtEmail.IsEnabled = false;  
            txtNombres.IsEnabled = false;
            txtApellidos.IsEnabled = false;
            dtpckFechaNacimiento.IsEnabled = false;
        }

        private void SetControlsValues()
        {
            txtEmail.Text = usuario.Email;
            txtPassword.Password = usuario.Password;
            txtNombres.Text = usuario.Nombres;
            txtApellidos.Text = usuario.Apellidos;
            txtTelefono.Text = usuario.Telefono;
        }
    }
}
