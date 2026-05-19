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
    /// Lógica de interacción para OlvidoPassword.xaml
    /// </summary>
    public partial class OlvidoPassword : Window
    {
        private readonly Services.LoginService loginService;
        private string email;
        public OlvidoPassword()
        {
            InitializeComponent();
            loginService = new Services.LoginService();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            email = txtEmail.Text;
            string resultado = loginService.RecuperarPassword(email);
            lblMensaje.Content = resultado;

            if (resultado == "Se ha enviado un correo con el código de recuperación.")
            {
                HabilitarControles();
            }
        }

        private void HabilitarControles()
        {
            stcCodigo.Visibility = Visibility.Visible;
            stcPassword.Visibility = Visibility.Visible;
            stcLblRecuperar.Visibility = Visibility.Visible;
            stcBtnGuardar.Visibility = Visibility.Visible;

            stcEmail.Visibility = Visibility.Hidden;
            stcBtnRecuperar.Visibility = Visibility.Hidden;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            string codigo = txtCodigo.Text;
            string nuevaPassword = txtPassword.Password;

            string resultado = loginService.CambiarPassword(codigo, email, nuevaPassword);

            lblMensajeRecuperar.Content = resultado;

            btnRecuperar.IsEnabled = false;
        }
    }
    }
