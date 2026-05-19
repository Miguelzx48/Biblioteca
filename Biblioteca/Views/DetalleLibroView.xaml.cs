using Biblioteca.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Biblioteca.Views
{
    public partial class DetalleLibroView : UserControl
    {
        public event Action<UserControl> CambiarVista;

        private Usuario _usuario;

        public DetalleLibroView(libro libro, Usuario usuario)
        {
            InitializeComponent();
            _usuario = usuario;
            txtNombre.Text = libro.Nombre;
            txtAutor.Text = libro.Autor;
            txtCategoria.Text = libro.Categoria;
            txtEditorial.Text = libro.Editorial;
            txtStock.Text = libro.Stock.ToString();
            txtDescripcion.Text = libro.Descripcion;
            txtAnio.Text = libro.AnioPublicacion.ToString();
            if (!string.IsNullOrEmpty(libro.Imagen))
            {
                MessageBox.Show(libro.Imagen);
                imgLibro.Source = new BitmapImage(
                    new Uri(libro.Imagen));
            }
        }
        private void Cerrar_Click(object sender, RoutedEventArgs e)
        {
            ListaLibrosView vista = new ListaLibrosView(_usuario);

            CambiarVista?.Invoke(vista);
        }
    }
}