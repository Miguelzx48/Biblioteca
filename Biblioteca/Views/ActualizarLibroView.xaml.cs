using Biblioteca.Models;
using Biblioteca.Services;
using System;
using System.Windows;
using System.Windows.Controls;


namespace Biblioteca.Views
{
    public partial class ActualizarLibroView : UserControl
    {
        public event Action LibroActualizado;

        libro libroActual;
        public ActualizarLibroView(libro libroSeleccionado)
        {
            InitializeComponent();
            libroActual = libroSeleccionado;
            txtNombre.Text = libroActual.Nombre;
            txtAutor.Text = libroActual.Autor;
            txtStock.Text = libroActual.Stock.ToString();
            txtCategoria.Text = libroActual.Categoria;
            txtEditorial.Text = libroActual.Editorial;
            txtDescripcion.Text = libroActual.Descripcion;
        }
        private void ActualizarLibro(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultado = MessageBox.Show(
                "¿Seguro desea actualizar este libro?",
                "Confirmación",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (resultado == MessageBoxResult.Yes)
            {
                libroActual.Nombre = txtNombre.Text;
                libroActual.Autor = txtAutor.Text;
                libroActual.Editorial = txtEditorial.Text;
                libroActual.Categoria = txtCategoria.Text;
                libroActual.Stock = Convert.ToInt32(txtStock.Text);
                libroActual.Descripcion = txtDescripcion.Text;
                libroService servicio = new libroService();
                servicio.Actualizar(libroActual);
                MessageBox.Show("Libro actualizado correctamente");

                LibroActualizado?.Invoke();
            }
        }

        private void EliminarLibro(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultado = MessageBox.Show(
                "¿Seguro desea eliminar este libro?",
                "Confirmación",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);
            if (resultado == MessageBoxResult.Yes)
            {
                libroService servicio = new libroService();
                servicio.Eliminar(libroActual.IdLibro);
                MessageBox.Show("Libro eliminado correctamente");
                LibroActualizado?.Invoke();
            }
        }

    }
}