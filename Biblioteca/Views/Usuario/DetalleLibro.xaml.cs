using Biblioteca.Controllers;
using Biblioteca.Models;
using System.Windows;
using System.Windows.Controls;

namespace Biblioteca.Views.Usuario
{
    public partial class DetalleLibroUsuario : UserControl
    {
        private libro libroActual;

        PrestamoController prestamoController = new PrestamoController();
        LibroController libroController = new LibroController();

        public DetalleLibroUsuario(libro libro)
        {
            InitializeComponent();
            libroActual = libro;
            CargarDatos();
        }

        private void CargarDatos()
        {
            txtNombre.Text = libroActual.Nombre;
            txtAutor.Text = "Autor: " + libroActual.Autor;
            txtCategoria.Text = "Categoría: " + libroActual.Categoria;
            txtEditorial.Text = "Editorial: " + libroActual.Editorial;
            txtAnio.Text = "Año: " + libroActual.AnioPublicacion;
            txtStock.Text = "Stock: " + libroActual.Stock;
            txtDescripcion.Text = libroActual.Descripcion;

            // si no tienes imagen en DB, lo dejamos vacío seguro
            imgLibro.Source = null;
        }

        private void BtnRealizarPrestamoClick(object sender, RoutedEventArgs e)
        {
            int idUsuario = LoginView.UsuarioActual.Id;

            Prestamo prestamo = new Prestamo
            {
                IdUsuario = idUsuario,
                IdLibro = libroActual.IdLibro
            };

            libro resultado = prestamoController.UsuarioPrestamo(prestamo);

            if (resultado != null)
            {
                MessageBox.Show("Préstamo realizado correctamente");

                // actualizar stock en pantalla
                libroActual.Stock = resultado.Stock;
                txtStock.Text = "Stock: " + libroActual.Stock;
            }
            else
            {
                MessageBox.Show("No se pudo realizar el préstamo");
            }
        }

        private void BtnVolverClick(object sender, RoutedEventArgs e)
        {
            // puedes reemplazar esto según tu navegación
            this.Visibility = Visibility.Collapsed;
        }
    }
}