using Biblioteca.Views;
using Biblioteca.Views.Windows;
using Biblioteca.Views.UsuarioPagina;
using System.Windows;
using System.Windows.Controls;
namespace Biblioteca
{
    public partial class MainWindow : Window
    {
        private Models.Usuario _usuario;

        public static Biblioteca.Models.Usuario UsuarioActual;

        public MainWindow(Models.Usuario usuario)
        {
            InitializeComponent();

            UsuarioActual = usuario;
            _usuario = usuario;

            ConfigurarPermisos();
            CargarVistaInicial();
        }

        private void ConfigurarPermisos()
        {
            if (_usuario.TipoUsuario == 1)
            {
                btnInicioUsuario.Visibility = Visibility.Collapsed;
                btnPerfilUsuario.Visibility = Visibility.Collapsed;
                btnLibrosUsuario.Visibility = Visibility.Collapsed;
                btnPrestamosUsuario.Visibility = Visibility.Collapsed;
                btnMultasUsuario.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnInicioUsuario.Visibility = Visibility.Collapsed;
                btndashboard.Visibility = Visibility.Collapsed;
                btnLibros.Visibility = Visibility.Collapsed;
                btnListaLibros.Visibility = Visibility.Collapsed;
                btnPrestamoAdmin.Visibility = Visibility.Collapsed;
            }
        }

        private void CargarVistaInicial()
        {
            if (_usuario.TipoUsuario == 1)
            {
                MainContent.Content = new Usuario();
            }
        }

        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new Usuario();
        }

        private void Libros_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new LibroView(_usuario);
        }

        private void ListaLibros_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ListaLibrosView(_usuario);
        }

        private void Prestamo_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new PrestamoAdmin();
        }

        private void Usuarios_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new CrearUsuario();
        }

        private void InicioUsuario_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new Usuario();
        }

        private void PerfilUsuario_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new PerfilUsuario(_usuario);
        }

        private void LibrosUsuario_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new LibroUsuario();
        }

        private void PrestamosUsuario_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new PrestamoUsuario(_usuario.Id);
        }

        private void MultasUsuario_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new MultaUsuario(_usuario.Id);
        }

        private void CerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            LoginView login = new LoginView();

            login.Show();
            this.Close();
        }
    }
}