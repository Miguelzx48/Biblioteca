using Biblioteca.Controllers;
using Biblioteca.Models;
using System.Windows;
using System.Windows.Controls;

namespace Biblioteca.Views.UsuarioPagina
{
    public partial class LibroUsuario : UserControl
    {
        LibroController controller = new LibroController();

        public LibroUsuario()
        {
            InitializeComponent();

            CargarLibros();
        }

        private void CargarLibros()
        {
            itemsLibros.ItemsSource =
                controller.ObtenerTodos();
        }

        private void txtBuscarLibro_TextChanged(object sender, TextChangedEventArgs e)
        {
            string texto = txtBuscarLibro.Text;

            if (string.IsNullOrWhiteSpace(texto))
            {
                CargarLibros();
            }
            else
            {
                itemsLibros.ItemsSource =
                    controller.BuscadorLibros(texto);
            }
        }

        private void BtnVerDetallesClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            if (btn == null)
            {
                return;
            }

            libro libroSeleccionado =
                btn.DataContext as libro;

            if (libroSeleccionado == null)
            {
                MessageBox.Show("No se pudo cargar el detalle del libro");
                return;
            }

            DetalleLibro detalle =
                new DetalleLibro(libroSeleccionado);

            Window ventana = new Window();

            ventana.Content = detalle;
            ventana.Width = 1100;
            ventana.Height = 700;
            ventana.WindowStartupLocation =
                WindowStartupLocation.CenterScreen;

            ventana.ShowDialog();

            CargarLibros();
        }
    }
}