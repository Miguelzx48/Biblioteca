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
    /// Lógica de interacción para MultaUsuario.xaml
    /// </summary>
    public partial class MultaUsuario : UserControl
    {
        PrestamoController controller = new PrestamoController();

        private int idUsuarioActual;

        public MultaUsuario(int idUsuario)
        {
            InitializeComponent();

            idUsuarioActual = idUsuario;

            CargarMultas();
        }

        private void CargarMultas()
        {
            List<Prestamo> prestamos = controller.LeerListaPrestamos();

            List<Prestamo> multas = new List<Prestamo>();

            double total = 0;

            foreach (var p in prestamos)
            {
                if (p.IdUsuario == idUsuarioActual && p.Multa > 0)
                {
                    multas.Add(p);
                    total += p.Multa;
                }
            }

            dgMultas.ItemsSource = multas;
            txtTotalMulta.Text = "Multa total: $" + total;
        }

        private void BtnPagarClick(object sender, RoutedEventArgs e)
        {
            Prestamo prestamo = (Prestamo)dgMultas.SelectedItem;

            if (prestamo == null)
            {
                MessageBox.Show("Selecciona una multa");
                return;
            }

            prestamo.Multa = 0;

            bool ok = controller.ActualizarMultas(prestamo);

            if (ok)
            {
                MessageBox.Show("Multa pagada correctamente");
                CargarMultas();
            }
            else
            {
                MessageBox.Show("Error al pagar multa");
            }
        }
    }
}
