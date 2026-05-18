using Biblioteca.Controllers;
using Biblioteca.Db;
using Biblioteca.Views;
using System.Windows;

namespace Biblioteca
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            UsuarioController usuarioController = new UsuarioController();
            usuarioController.ValidarAdminDefault();

            UsuarioDb db = new UsuarioDb();
            db.ProbarConexion();

            var login = new LoginView();
            login.Show();

            //LibroView view = new LibroView();
            //view.Show();
        }
    }
}