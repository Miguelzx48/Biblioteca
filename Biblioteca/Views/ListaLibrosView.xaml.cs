using Biblioteca.Models;
using Biblioteca.Services;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;




namespace Biblioteca.Views
{
    public partial class ListaLibrosView : Window
    {
        libroService servicio = new libroService();

        public ListaLibrosView()
        {
            InitializeComponent();
            CargarLibros();
        }

        private void CargarLibros()
        {
            List<libro> lista = servicio.ObtenerLibros();

            dgLibros.ItemsSource = lista;
        }

        

        private void ActualizarLibro(object sender, RoutedEventArgs e)
        {
            Button boton = sender as Button;

            libro libroSeleccionado = boton.Tag as libro;

            MessageBox.Show("Actualizar: " + libroSeleccionado.Nombre);

            
        }
        private void Eliminar(object sender, RoutedEventArgs e)
        {
            Button boton = sender as Button;

            libro libroSeleccionado = boton.Tag as libro;

            MessageBoxResult resultado = MessageBox.Show(
                "¿Seguro desea eliminar el libro: " + libroSeleccionado.Nombre + "?",
                "Confirmar eliminación",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (resultado == MessageBoxResult.Yes)
            {
                int id = libroSeleccionado.IdLibro;

                libroService servicio = new libroService();

                servicio.Eliminar(id);

                MessageBox.Show("Libro eliminado correctamente");

                dgLibros.ItemsSource = servicio.ObtenerLibros();
            }
        }
       
        public void Actualizar(object sender, RoutedEventArgs e)
        {
            Button boton = sender as Button;

            libro libroSeleccionado = boton.Tag as libro;

            ActualizarLibroView ventana = new ActualizarLibroView(libroSeleccionado);

            ventana.ShowDialog();

            libroService servicio = new libroService();

            dgLibros.ItemsSource = servicio.ObtenerLibros();
        }
    }
}