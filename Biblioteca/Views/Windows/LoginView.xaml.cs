using Biblioteca.Controllers;
using Biblioteca.Models;
using Biblioteca.Views.Windows;
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
using System.Windows.Shapes;

namespace Biblioteca.Views
{
    /// <summary>
    /// Lógica de interacción para LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        
        public static Biblioteca.Models.Usuario UsuarioActual;

        LoginController controller;

        public LoginView()
        {
            InitializeComponent();
            controller = new LoginController();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Password;

            Models.Usuario usuario = controller.Login(email, password);

            if (usuario == null)
            {
                lblError.Content = "Usuario y/o contraseña incorrecta";
            }
            else
            {
                

                UsuarioActual = usuario;


                usuario.Activo = false;

                var main = new MainWindow(usuario);
                main.Show();
                this.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var crearCuenta = new CrearUsuario(0);
            crearCuenta.Show();

            
            this.Close();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            var olvidoPassword = new OlvidoPassword();
            olvidoPassword.Show();

            
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}