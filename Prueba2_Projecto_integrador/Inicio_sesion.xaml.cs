using MySql.Data.MySqlClient;
using Prueba2_Projecto_integrador.Clases;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;


namespace Prueba2_Projecto_integrador
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Inicio_sesion : Window
    {
        private string StrComando = "";

        private readonly MySqlConnection conexion = Conexion.getConexion();
        public Inicio_sesion()
        {
            InitializeComponent();
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtContra.Focus();
        }

        private void txtContra_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtContra.Password) && txtContra.Password.Length > 0)
            {
                textPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                textPassword.Visibility = Visibility.Visible;
            }
        }

        private void btnIniciar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StrComando = "select usuarios_tipo from tabla_usuarios where usuarios_nombre ='" + txtUser.Text + "'";

                Usuario.StrTpoUsuario = ComandosSQL.ComandoObtenerString(StrComando, Usuario.StrTpoUsuario);

                StrComando = "select usuarios_id from tabla_usuarios where usuarios_nombre ='" + txtUser.Text + "'";

                Usuario.IntIdUsuario = ComandosSQL.ComandoObtenerInt(StrComando, Usuario.IntIdUsuario);

                StrComando = "select lab_id from tabla_usuarios where usuarios_nombre ='" + txtUser.Text + "'";

                Usuario.IntLabUsuario = ComandosSQL.ComandoObtenerInt(StrComando, Usuario.IntLabUsuario);

                StrComando = "select * from tabla_usuarios where usuarios_nombre ='" + txtUser.Text + "' AND usuarios_contraseña = '" + txtContra.Password.ToString() + "'";

                MySqlCommand comando = new(StrComando, conexion);

                conexion.Open();

                MySqlDataReader reader = comando.ExecuteReader();

                if (reader.Read())
                {
                    Usuario.StrNombreUsuario = txtUser.Text;
                    Menu menu = new();
                    menu.Show();
                    StrComando = "INSERT INTO `bd_prestamos_itspp`.`tabla_bitacora` (`bitacora_fecha`, `bitacora_operacion`) VALUES ('Fecha: "+DateTime.Now.ToString()+"', 'Inicio sesión el usuario: "+Usuario.StrNombreUsuario+"');";
                    ComandosSQL.ComandoNonquery(StrComando);
                    this.Close();
                }
                else
                {
                    spError.Visibility = Visibility.Visible;
                    txtUser.Text = "Usuario";
                    txtContra.Password = null;
                }

                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void txtUser_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtUser.Text == "Usuario")
            {
                txtUser.Text = null;
            }
        }

        private void txtUser_LostFocus(object sender, RoutedEventArgs e)
        {
            Aparencias.txtLostFocus(8, txtUser);
        }

        private void iconSalir_MouseEnter(object sender, MouseEventArgs e)
        {
            iconSalir.Background = new SolidColorBrush(Colors.Red);
        }

        private void iconSalir_MouseLeave(object sender, MouseEventArgs e)
        {
            iconSalir.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void iconSalir_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            StrComando = "INSERT INTO `bd_prestamos_itspp`.`tabla_bitacora` (`bitacora_fecha`, `bitacora_operacion`) VALUES ('Fecha: "+DateTime.Now.ToString()+"', 'Cerro sesión el usuario: " + Usuario.StrNombreUsuario + "');";
            ComandosSQL.ComandoNonquery(StrComando);
            this.Close();
        }

    }
}
