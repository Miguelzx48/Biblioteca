using Biblioteca.Db;
using Biblioteca.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Biblioteca.Views
{
    public partial class LibroView : UserControl
    {

        private Models.Usuario _usuario;

        private string rutaImagen = "";

        public LibroView(Models.Usuario usuario)
        {
            InitializeComponent();
            _usuario = usuario;
        }

        private void SeleccionarImagen(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            open.Filter = "Imagenes|*.jpg;*.png;*.jpeg";

            if (open.ShowDialog() == true)
            {
                rutaImagen = open.FileName;

                imgLibro.Source = new BitmapImage(
                    new Uri(rutaImagen));
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = sender as TextBox;

            if (txt.Foreground == Brushes.Gray)
            {
                txt.Text = "";
                txt.Foreground = Brushes.Black;
            }
        }
        
        private void RestaurarPlaceholders()
        {
            txtNombre.Text = "Título del libro...";
            txtNombre.Foreground = Brushes.Gray;

            txtAutor.Text = "Escriba aquí el nombre del autor";
            txtAutor.Foreground = Brushes.Gray;

            txtCategoria.Text = "Ingrese la categoría...";
            txtCategoria.Foreground = Brushes.Gray;

            txtEditorial.Text = "Editorial del libro...";
            txtEditorial.Foreground = Brushes.Gray;

            txtStock.Text = "Cantidad en stock...";
            txtStock.Foreground = Brushes.Gray;

            txtDescripcion.Text = "Resumen de la trama y detalles...";
            txtDescripcion.Foreground = Brushes.Gray;
        }
        public void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = sender as TextBox;

            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                if (txt.Name == "txtNombre")
                    txt.Text = "Título del libro...";

                else if (txt.Name == "txtAutor")
                    txt.Text = "Escriba aquí el nombre del autor";

                else if (txt.Name == "txtCategoria")
                    txt.Text = "Ingrese la categoría...";

                else if (txt.Name == "txtEditorial")
                    txt.Text = "Editorial del libro...";

                else if (txt.Name == "txtStock")
                    txt.Text = "Cantidad en stock...";

                else if (txt.Name == "txtDescripcion")
                    txt.Text = "Resumen de la trama y detalles...";

                txt.Foreground = Brushes.Gray;
            }
        }
       

        private void GuardarLibro(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    txtNombre.Text == "Título del libro...")
                {
                    MessageBox.Show("El campo Nombre es obligatorio",
                                    "Campo obligatorio",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);

                    txtNombre.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtAutor.Text) ||
                    txtAutor.Text == "Ingrese el autor...")
                {
                    MessageBox.Show("El campo Autor es obligatorio",
                                    "Campo obligatorio",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);

                    txtAutor.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtCategoria.Text) ||
                    txtCategoria.Text == "Ingrese la categoría...")
                {
                    MessageBox.Show("El campo Categoría es obligatorio",
                                    "Campo obligatorio",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);

                    txtCategoria.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtEditorial.Text) ||
                    txtEditorial.Text == "Editorial del libro...")
                {
                    MessageBox.Show("El campo Editorial es obligatorio",
                                    "Campo obligatorio",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);

                    txtEditorial.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtStock.Text) ||
                    txtStock.Text == "Cantidad en stock...")
                {
                    MessageBox.Show("El campo Stock es obligatorio",
                                    "Campo obligatorio",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);

                    txtStock.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDescripcion.Text) ||
                    txtDescripcion.Text == "Resumen de la trama y detalles...")
                {
                    MessageBox.Show("El campo Descripción es obligatorio",
                                    "Campo obligatorio",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);

                    txtDescripcion.Focus();
                    return;
                }

                if (dtaAnio.SelectedDate == null)
                {
                    MessageBox.Show("Debe seleccionar el año de publicación",
                                    "Campo obligatorio",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);

                    dtaAnio.Focus();
                    return;
                }

                libro libro = new libro();

                libro.Nombre = txtNombre.Text;
                libro.Autor = txtAutor.Text;
                libro.Categoria = txtCategoria.Text;
                libro.Editorial = txtEditorial.Text;
                libro.Stock = Convert.ToInt32(txtStock.Text);
                libro.Descripcion = txtDescripcion.Text;
                libro.AnioPublicacion = dtaAnio.SelectedDate?.Year ?? 0;
                libro.Imagen = rutaImagen;

                LibroDb db = new LibroDb();

                db.CrearLibro(libro);

                MessageBox.Show("Libro guardado correctamente");

                RestaurarPlaceholders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void GenerarPDF(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "PDF files (*.pdf)|*.pdf";
                save.FileName = "Libro.pdf";

                if (save.ShowDialog() == true)
                {
                    Document doc = new Document();
                    PdfWriter.GetInstance(doc, new FileStream(save.FileName, FileMode.Create));

                    doc.Open();

                    Paragraph titulo = new Paragraph("INFORMACIÓN DEL LIBRO");
                    titulo.Alignment = Element.ALIGN_CENTER;
                    titulo.SpacingAfter = 20f;

                    doc.Add(titulo);

                    doc.Add(new Paragraph("Nombre: " + txtNombre.Text));
                    doc.Add(new Paragraph("Autor: " + txtAutor.Text));
                    doc.Add(new Paragraph("Categoría: " + txtCategoria.Text));
                    doc.Add(new Paragraph("Editorial: " + txtEditorial.Text));
                    doc.Add(new Paragraph("Stock: " + txtStock.Text));
                    doc.Add(new Paragraph("Descripción: " + txtDescripcion.Text));
                    doc.Add(new Paragraph("Año: " + dtaAnio.SelectedDate?.Year));

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