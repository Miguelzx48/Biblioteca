using Biblioteca.Controllers;
using Biblioteca.Db;
using Biblioteca.Views;
using Biblioteca.Views.Windows;
using System.Windows;
using System.Windows.Controls;

namespace Biblioteca
{
    public partial class MainWindow : Window
    {
        private Models.Usuario _usuario;

        public static Biblioteca.Models.Usuario UsuarioActual;
        private void ConfigurarPermisos()
        {
            if (_usuario.TipoUsuario == 0)
            {
                btnUsuarios.Visibility = Visibility.Collapsed;
                btndashboard.Visibility = Visibility.Collapsed;
                btnLibros.Visibility = Visibility.Collapsed;
            }
        }

        public MainWindow(Models.Usuario usuario)
        {
            InitializeComponent();
            UsuarioActual = usuario;

            _usuario = usuario;

            if (_usuario.TipoUsuario == 0)
            {
                MainContent.Content = null;
            }
            else
            {
                MainContent.Content = null; 
            }
            ConfigurarPermisos();
        }

        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new DashboardView();
        }

        private void Libros_Click(object sender, RoutedEventArgs e)
        {
            LibroView vista = new LibroView(_usuario);

            vista.CambiarVista += (nuevaVista) =>
            {
                MainContent.Content = nuevaVista;
            };

            MainContent.Content = vista;
        }

        private void ListaLibros_Click(object sender, RoutedEventArgs e)
        {
            ListaLibrosView vista = new ListaLibrosView(_usuario);

            vista.CambiarVista += CambiarVista;

            MainContent.Content = vista;
        }
        private void CambiarVista(UserControl nuevaVista)
        {
            if (nuevaVista is ListaLibrosView lista)
            {
                lista.CambiarVista += CambiarVista;
            }

            if (nuevaVista is LibroView libro)
            {
                libro.CambiarVista += CambiarVista;
            }

            if (nuevaVista is ActualizarLibroView actualizar)
            {
                actualizar.LibroActualizado += () =>
                {
                    ListaLibrosView listaNueva =
                        new ListaLibrosView(_usuario);

                    listaNueva.CambiarVista += CambiarVista;

                    MainContent.Content = listaNueva;
                };
            }

            MainContent.Content = nuevaVista;
        }

        private void Prestamo_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new PrestamoView();
        }

        private void Usuarios_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new CrearUsuario();
        }

        private void CerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            var Login = new LoginView();
            Login.Show();
            this.Close();
        }


    }
}