using Biblioteca.Controllers;
using Biblioteca.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Biblioteca.Views.UsuarioPagina
{
    public partial class DetalleLibro : UserControl
    {
        private libro libroActual;

        PrestamoController prestamoController = new PrestamoController();

        public DetalleLibro(libro libro)
        {
            InitializeComponent();

            libroActual = libro;

            CargarDatos();
        }

        private void CargarDatos()
        {
            if (libroActual == null)
            {
                MessageBox.Show("No se pudo cargar el libro");
                return;
            }

            txtNombre.Text = libroActual.Nombre;
            txtAutor.Text = "Autor: " + libroActual.Autor;
            txtCategoria.Text = "Categoría: " + libroActual.Categoria;
            txtEditorial.Text = "Editorial: " + libroActual.Editorial;
            txtAnio.Text = "Año: " + libroActual.AnioPublicacion;
            txtStock.Text = "Stock: " + libroActual.Stock;
            txtDescripcion.Text = libroActual.Descripcion;

            try
            {
                if (!string.IsNullOrEmpty(libroActual.Imagen))
                {
                    imgLibro.Source =
                        new BitmapImage(
                            new Uri(libroActual.Imagen,
                            UriKind.RelativeOrAbsolute));
                }
                else
                {
                    imgLibro.Source = null;
                }
            }
            catch
            {
                imgLibro.Source = null;
            }
        }

        private void BtnRealizarPrestamoClick(object sender, RoutedEventArgs e)
        {
            int idUsuario = MainWindow.UsuarioActual.Id;

            Prestamo prestamo = new Prestamo();

            prestamo.IdUsuario = idUsuario;
            prestamo.IdLibro = libroActual.IdLibro;

            libro resultado =
                prestamoController.UsuarioPrestamo(prestamo);

            if (resultado != null)
            {
                MessageBox.Show("Préstamo realizado correctamente");

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
            Window ventana = Window.GetWindow(this);

            if (ventana != null)
            {
                ventana.Close();
            }
        }
    }
}