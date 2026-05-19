using Biblioteca.Controllers;
using System.Windows.Controls;

namespace Biblioteca.Views.UsuarioPagina
{
    /// <summary>
    /// Lógica de interacción para Usuario.xaml
    /// </summary>
    public partial class Usuario : UserControl
    {
        private LibroController libroController = new LibroController();

        public Usuario()
        {
            InitializeComponent();

            CargarLibrosMasPrestados();
        }

        private void CargarLibrosMasPrestados()
        {
            var datos = libroController.LibrosMasPrestadosPorMes();

            itemsLibrosMasPrestados.ItemsSource = datos;
        }
    }
}