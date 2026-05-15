using Biblioteca.Db;
using Biblioteca.Models;
using System;
using System.Windows;

namespace Biblioteca.Views
{
    public partial class PrestamoView : Window
    {
        public PrestamoView()
        {
            InitializeComponent();
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Prestamo prestamo = new Prestamo();

                prestamo.IdPrestamo = Convert.ToInt32(txtIdPrestamo.Text);
                prestamo.IdUsuario = Convert.ToInt32(txtIdUsuario.Text);
                prestamo.IdLibro = Convert.ToInt32(txtIdLibro.Text);

               
               

                prestamo.FechaPrestamo = DateTime.Now;
                prestamo.FechaEstimadaDevolucion = DateTime.Now.AddDays(7);
                prestamo.FechaRealDevolucion = DateTime.Now;

                prestamo.CantidadDiasMora = 0;

                PrestamoDb db = new PrestamoDb();

                db.CrearPrestamo(prestamo);

                MessageBox.Show("Prestamo guardado");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}