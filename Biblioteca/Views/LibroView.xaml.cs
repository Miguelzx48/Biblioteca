using Biblioteca.Db;
using Biblioteca.Models;
using System;
using System.Windows;
using MySql.Data.MySqlClient;
using Biblioteca.Views;
namespace Biblioteca.Views

{
    public partial class LibroView : Window
    {
        public LibroView()
        {
            InitializeComponent();
        }
        private void ActualizarLibro(object sender, RoutedEventArgs e)
        {
            ActualizarLibroView ventana = new ActualizarLibroView();

            ventana.Show();
        }
        
        private void EliminarLibro(object sender, RoutedEventArgs e)
        {
            EliminarLibroView ventana = new EliminarLibroView();

            ventana.Show();
        }
        private void GuardarLibro(object sender, RoutedEventArgs e)
        {
            try
            {
                libro libro = new libro();

                libro.Nombre = txtNombre.Text;
                libro.Autor = txtAutor.Text;
                libro.Categoria = txtCategoria.Text;
                libro.Editorial = txtEditorial.Text;
                libro.Stock = Convert.ToInt32(txtStock.Text);
                libro.Descripcion = txtDescripcion.Text;
                libro.AnioPublicaion = Convert.ToInt32(txtAnio.Text);

                LibroDb db = new LibroDb();

                db.CrearLibro(libro);

                MessageBox.Show("Libro guardado correctamente");
                txtNombre.Clear();
                txtAutor.Clear();
                txtCategoria.Clear();
                txtEditorial.Clear();
                txtStock.Clear();
                txtDescripcion.Clear();
                txtAnio.Clear();
            }
            
           catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}