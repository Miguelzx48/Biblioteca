using Biblioteca.Models;
using Biblioteca.Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Biblioteca.Views
{
    public partial class ListaLibrosView : UserControl
    {
        public event Action<UserControl> CambiarVista;

        private libroService servicio = new libroService();
        private Models.Usuario _usuario;

        public string TextoBoton =>
            _usuario?.TipoUsuario == 1 ? "Gestionar" : "Examinar";

        public string ColorBoton =>
            _usuario?.TipoUsuario == 1 ? "Blue" : "Green";

        public ListaLibrosView(Models.Usuario usuario)
        {
            InitializeComponent();

            _usuario = usuario;

            DataContext = this;

            CargarLibros();
        }

        private void Accion_Click(object sender, RoutedEventArgs e)
        {
            Button boton = sender as Button;
            libro libroSeleccionado = boton.Tag as libro;

            if (_usuario != null && _usuario.TipoUsuario == 1)
            {
                var vista = new ActualizarLibroView(libroSeleccionado);
                CambiarVista?.Invoke(vista);
            }
            else
            {
                MessageBox.Show("EXAMINAR: " + libroSeleccionado.Nombre);
            }
        }

        private void CargarLibros()
        {
            dgLibros.ItemsSource = servicio.ObtenerLibros();
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
                servicio.Eliminar(libroSeleccionado.IdLibro);

                MessageBox.Show("Libro eliminado correctamente");

                CargarLibros();
            }
        }

        public void Actualizar(object sender, RoutedEventArgs e)
        {
            Button boton = sender as Button;
            libro libroSeleccionado = boton.Tag as libro;

            ActualizarLibroView vista =
                new ActualizarLibroView(libroSeleccionado);

            vista.LibroActualizado += () =>
            {
                CargarLibros();
                CambiarVista?.Invoke(this);
            };

            CambiarVista?.Invoke(vista);
        }
    }
}