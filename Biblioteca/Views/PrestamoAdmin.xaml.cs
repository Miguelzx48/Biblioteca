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

namespace Biblioteca.Views
{
    public partial class PrestamoAdmin : UserControl
    {
        PrestamoController controller = new PrestamoController();

        public PrestamoAdmin()
        {
            InitializeComponent();

            CargarPrestamos();
        }

        private void CargarPrestamos()
        {
            dgPrestamos.ItemsSource = null;

            dgPrestamos.ItemsSource = controller.LeerListaPrestamos();
        }

        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string texto = txtBuscar.Text;

            if (string.IsNullOrWhiteSpace(texto))
            {
                CargarPrestamos();
                return;
            }

            List<Prestamo> lista =
                controller.BuscarPrestamo(texto);

            dgPrestamos.ItemsSource = null;

            dgPrestamos.ItemsSource = lista;
        }
        private void BtnActualizarMultaClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            Prestamo prestamo =
                btn.DataContext as Prestamo;

            if (prestamo == null)
                return;

            Prestamo actualizado =
                controller.CalcularMultas(prestamo);

            MessageBox.Show(
                "Multa actualizada correctamente\n\n" +
                "Días mora: " + actualizado.CantidadDiasMora +
                "\nMulta: $" + actualizado.Multa,
                "Multa actualizada",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );

            CargarPrestamos();
        }

        private void BtnEliminarClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            Prestamo prestamo =
                btn.DataContext as Prestamo;

            if (prestamo == null)
                return;

            MessageBoxResult resultado =
                MessageBox.Show(
                    "¿Desea eliminar este préstamo?",
                    "Confirmar",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                );

            if (resultado == MessageBoxResult.Yes)
            {
                bool eliminado =
                    controller.EliminarPrestamo(
                        prestamo.IdPrestamo
                    );

                if (eliminado)
                {
                    MessageBox.Show(
                        "Préstamo eliminado correctamente",
                        "Éxito",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );

                    CargarPrestamos();
                }
                else
                {
                    MessageBox.Show(
                        "No se pudo eliminar el préstamo",
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
                }
            }
        }
    }
}