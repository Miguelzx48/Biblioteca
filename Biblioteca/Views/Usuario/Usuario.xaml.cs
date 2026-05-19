using Biblioteca.Controllers;
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
    /// Lógica de interacción para Usuario.xaml
    /// </summary>
    /// 
   
    public partial class Usuario : UserControl
    {

        private LibroController libroController = new LibroController();
        public Usuario()
        {
            InitializeComponent();
            CargarLibrosMasPrestados();
        }
        private void CargarLibrosMasPrestados()
        {
            var datos = libroController.LibrosMasPrestadosPorMes();

            itemsLibrosMasPrestados.ItemsSource = datos;
        }
    }
}
