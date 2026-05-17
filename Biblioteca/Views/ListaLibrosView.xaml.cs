using Biblioteca.Db;
using Biblioteca.Models;
using Biblioteca.Services;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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