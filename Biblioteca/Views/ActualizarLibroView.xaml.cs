using Biblioteca.Models;
using Biblioteca.Services;
using System;
using System.Windows;
namespace Biblioteca.Views
{
    public partial class ActualizarLibroView : Window
    {
        libro libroActual;
        public ActualizarLibroView(libro libroSeleccionado)
        {
            InitializeComponent();
            libroActual = libroSeleccionado;
            txtNombre.Text = libroActual.Nombre;
            txtAutor.Text = libroActual.Autor;
            txtStock.Text = libroActual.Stock.ToString();
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
                libroActual.Stock = Convert.ToInt32(txtStock.Text);
                libroActual.Descripcion = txtDescripcion.Text;
                libroService servicio = new libroService();
                servicio.Actualizar(libroActual);
                MessageBox.Show("Libro actualizado correctamente");
                this.Close();
            }
        }
    }
}