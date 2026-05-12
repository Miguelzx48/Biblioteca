using Biblioteca.Db;
using System;
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
            try
            {
                int idLibro = int.Parse(txtIdLibro.Text);

                LibroDb db = new LibroDb();
                db.Eliminar(idLibro);

                MessageBox.Show("Libro eliminado correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}