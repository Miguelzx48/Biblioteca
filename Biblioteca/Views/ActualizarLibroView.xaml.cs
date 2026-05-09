using System;
using System.Windows;

namespace Biblioteca.Views
{
    public partial class ActualizarLibroView : Window
    {
        public ActualizarLibroView()
        {
            InitializeComponent();
        }

        private void Actualizar(object sender, RoutedEventArgs e)
        {
            
            MessageBox.Show("Libro actualizado correctamente");
        }
    }
}