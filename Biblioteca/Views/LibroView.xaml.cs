using Biblioteca.Db;
using Biblioteca.Models;
using System;
using System.Windows;
using MySql.Data.MySqlClient;
using Biblioteca.Views;
using System.Windows.Controls;
namespace Biblioteca.Views

{
    public partial class LibroView : UserControl
    {
        public event Action<UserControl> CambiarVista;

        private Models.Usuario _usuario;

        public LibroView(Models.Usuario usuario)
        {
            InitializeComponent();
            _usuario = usuario;
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
                libro.AnioPublicacion = dtaAnio.SelectedDate?.Year ?? 0;

                LibroDb db = new LibroDb();

                db.CrearLibro(libro);

                MessageBox.Show("Libro guardado correctamente");
                txtNombre.Clear();
                txtAutor.Clear();
                txtCategoria.Clear();
                txtEditorial.Clear();
                txtStock.Clear();
                txtDescripcion.Clear();
                dtaAnio.SelectedDate = null;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}