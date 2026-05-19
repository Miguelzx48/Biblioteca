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
            lblMensajeRecuperar.Text = resultado;

            if (resultado == "Se ha enviado un correo con el código de recuperación.")
            {
                HabilitarControles();
            }
        }

        private void HabilitarControles()
        {
            // MOSTRAR CONTROLES NUEVOS
            stcLblRecuperar.Visibility = Visibility.Visible;
            stcCodigo.Visibility = Visibility.Visible;
            stcPassword.Visibility = Visibility.Visible;
            stcBtnGuardar.Visibility = Visibility.Visible;

            // OCULTAR CONTROLES ANTERIORES
            lblMensaje.Visibility = Visibility.Hidden;
            stcEmail.Visibility = Visibility.Collapsed;
            stcBtnRecuperar.Visibility = Visibility.Collapsed;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            string codigo = txtCodigo.Text;
            string nuevaPassword = txtPassword.Password;

            string resultado = loginService.CambiarPassword(codigo, email, nuevaPassword);

            lblMensajeRecuperar.Text = resultado;

            MessageBox.Show(resultado);

            if (resultado.Contains("exitosamente"))
            {
                LoginView login = new LoginView();

                login.Show();

                this.Close();
            }
        }
    }
    }
