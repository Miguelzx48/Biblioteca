using Biblioteca.Controllers;
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
    /// Lógica de interacción para LibroUsuario.xaml
    /// </summary>
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
            itemsLibros.ItemsSource = controller.ObtenerTodos();
        }

        private void btnBuscarLibroClick(object sender, RoutedEventArgs e)
        {
            string texto = txtBuscarLibro.Text;
            itemsLibros.ItemsSource = controller.BuscadorLibros(texto);
        }

        private void txtBuscarLibro_TextChanged(object sender, TextChangedEventArgs e)
        {
            string texto = txtBuscarLibro.Text;
            itemsLibros.ItemsSource = controller.BuscadorLibros(texto);
        }
        private void BtnVerDetallesClick(object sender, RoutedEventArgs e)
        {
            // lógica detalle
        }
    }
}
