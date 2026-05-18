using Biblioteca.Db;
using Biblioteca.Models;
using Biblioteca.Services;
<<<<<<< HEAD
=======
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
>>>>>>> maicol
using System;
using System.Collections.Generic;
using System.IO;
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
        private void ExportarPDF(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog save = new SaveFileDialog();

                save.Filter = "PDF files (*.pdf)|*.pdf";
                save.FileName = "InventarioLibros.pdf";

                if (save.ShowDialog() == true)
                {
                    Document doc = new Document(PageSize.A4.Rotate());

                    PdfWriter.GetInstance(doc,
                        new FileStream(save.FileName, FileMode.Create));

                    doc.Open();

                    Paragraph titulo = new Paragraph("INVENTARIO DE LIBROS");
                    titulo.Alignment = Element.ALIGN_CENTER;
                    titulo.SpacingAfter = 20f;

                    doc.Add(titulo);

                    PdfPTable tabla = new PdfPTable(7);

                    tabla.WidthPercentage = 100;

                    tabla.AddCell("ID");
                    tabla.AddCell("Nombre");
                    tabla.AddCell("Autor");
                    tabla.AddCell("Categoría");
                    tabla.AddCell("Editorial");
                    tabla.AddCell("Stock");
                    tabla.AddCell("Año");

                    LibroDb db = new LibroDb();

                    var lista = db.ObtenerTodos();

                    foreach (var libro in lista)
                    {
                        tabla.AddCell(libro.IdLibro.ToString());
                        tabla.AddCell(libro.Nombre);
                        tabla.AddCell(libro.Autor);
                        tabla.AddCell(libro.Categoria);
                        tabla.AddCell(libro.Editorial);
                        tabla.AddCell(libro.Stock.ToString());
                        tabla.AddCell(libro.AnioPublicacion.ToString());
                    }

                    doc.Add(tabla);

                    doc.Close();

                    MessageBox.Show("PDF generado correctamente");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}