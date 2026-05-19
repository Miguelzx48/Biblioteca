using Biblioteca.Controllers;
using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Biblioteca.Views.Usuario
{
    /// <summary>
    /// Lógica de interacción para PrestamoUsuario.xaml
    /// </summary>
    public partial class PrestamoUsuario : UserControl
    {
        PrestamoController controller = new PrestamoController();

        private int idUsuarioActual;

        public PrestamoUsuario(int idUsuario)
        {
            InitializeComponent();

            idUsuarioActual = idUsuario;

            CargarPrestamos();
        }

        private void CargarPrestamos()
        {
            List<Prestamo> prestamos = controller.LeerListaPrestamos();

            // filtramos SOLO los del usuario logueado
            List<Prestamo> filtrados = new List<Prestamo>();

            foreach (var p in prestamos)
            {
                if (p.IdUsuario == idUsuarioActual)
                {
                    filtrados.Add(p);
                }
            }

            dgPrestamos.ItemsSource = filtrados;
        }

        private void BtnDevolverClick(object sender, RoutedEventArgs e)
        {
            Prestamo prestamo = (Prestamo)dgPrestamos.SelectedItem;

            if (prestamo == null)
            {
                MessageBox.Show("Selecciona un préstamo");
                return;
            }

            bool ok = controller.DevolverPrestamo(prestamo);

            if (ok)
            {
                MessageBox.Show("Libro devuelto correctamente");
                CargarPrestamos();
            }
            else
            {
                MessageBox.Show("No se pudo devolver el préstamo");
            }
        }
    }
}
