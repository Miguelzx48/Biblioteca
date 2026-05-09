using System.Windows;

namespace Biblioteca.Views
{
    public partial class EliminarLibroView : Window
    {
        public EliminarLibroView()
        {
            InitializeComponent();
        }

        private void Eliminar(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Libro eliminado");
        }
    }
}