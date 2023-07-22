using Microsoft.Win32;
using Prueba2_Projecto_integrador.Clases;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Prueba2_Projecto_integrador
{
    /// <summary>
    /// Interaction logic for Window_Usuario.xaml
    /// </summary>
    public partial class Window_Usuario : Window
    {
        private string Comando = "";
        private static int IntEditar;
        public Window_Usuario()
        {
            InitializeComponent();
        }

        public Window_Usuario(int intEditar)
        {
            InitializeComponent();
            IntEditar = intEditar;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Variables.BoolEditar1)
            {
                Comando = "SELECT usuarios_nombre FROM bd_prestamos_itspp.tabla_usuarios WHERE usuarios_id = '" + IntEditar + "'";

                txtNombre.Text = ComandosSQL.ComandoObtenerString(Comando, txtNombre.Text);

                Comando = "SELECT usuarios_contraseña FROM bd_prestamos_itspp.tabla_usuarios WHERE usuarios_id = '" + IntEditar + "'";

                txtContra.Text = ComandosSQL.ComandoObtenerString(Comando, txtContra.Text);

                Comando = "SELECT usuarios_tipo FROM bd_prestamos_itspp.tabla_usuarios WHERE usuarios_id = '" + IntEditar + "'";

                cbTipo.Text = ComandosSQL.ComandoObtenerString(Comando, cbTipo.Text);

                Comando = "SELECT lab_id FROM bd_prestamos_itspp.tabla_usuarios WHERE usuarios_id = '" + IntEditar + "'";

                int index = 0;

                index = ComandosSQL.ComandoObtenerInt(Comando, index);

                Comando = "SELECT lab_nombre FROM bd_prestamos_itspp.tabla_labs WHERE lab_id = '" + index + "'";

                cbLaboratorio.Text = ComandosSQL.ComandoObtenerString(Comando, cbLaboratorio.Text);

                imgUsuario.Source = new BitmapImage(new Uri(Imagenes.ImagenUsuario(txtNombre.Text)));

            }

            Comando = "SELECT * FROM bd_prestamos_itspp.tabla_labs;";

            ComandosSQL.CargarComboBox(Comando, cbLaboratorio, "lab_nombre", "lab_id");
        }
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnListo_Click(object sender, RoutedEventArgs e)
        {
            if (txtNombre.Text == " "
                 || txtNombre.Text == "Nombre"
                 || txtContra.Text == " "
                 || txtContra.Text == "Contraseña"
                 || cbLaboratorio.Text == " "
                 || cbLaboratorio.Text == "Laboractorio"
                 || cbTipo.Text == " "
                 || cbTipo.Text == "Tipo"
                 || imgUsuario.Source.ToString() == string.Empty)
            {
                MessageBox.Show("No dejar espacios en blanco.\nIncluyendo la imagen.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (!Variables.BoolEditar1)
            {
                Comando = @"INSERT INTO `bd_prestamos_itspp`.`tabla_usuarios` (`usuarios_nombre`, `usuarios_contraseña`, `usuarios_tipo`, `lab_id`) 
                            VALUES ('" + txtNombre.Text + "', '" + txtContra.Text + "', '" + cbTipo.Text + "', '" + cbLaboratorio.SelectedValue + "');";

                ComandosSQL.ComandoNonquery(Comando);

                this.Close();
            }
            else
            {
                Comando = @"UPDATE `bd_prestamos_itspp`.`tabla_usuarios` SET" +
                    "`usuarios_nombre` = '" + txtNombre.Text + "', " +
                    "`usuarios_contraseña` = '" + txtContra.Text + "', " +
                    "`usuarios_tipo` = '" + cbTipo.Text + "' " + " WHERE (`usuarios_id` = '" + IntEditar + "');";

                ComandosSQL.ComandoNonquery(Comando);

                this.Close();
            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            string StrCarpetaBase = AppDomain.CurrentDomain.BaseDirectory;

            OpenFileDialog openFileDialog = new()
            {
                Filter = "Archivos de imagen|*.png;*.jpg;*.jpeg;*.bmp;*.gif|Todos los archivos|*.*",

                InitialDirectory = Path.Combine(StrCarpetaBase, "Imagenes", "Usuarios")

            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                try
                {
                    SaveFileDialog saveFileDialog = new()
                    {
                        Filter = "Archivos PNG (*.png)|*.png",
                        InitialDirectory = Path.Combine(StrCarpetaBase, "Imagenes", "Usuarios")
                    };

                    if (saveFileDialog.ShowDialog() == true)
                    {
                        string saveFilePath = saveFileDialog.FileName;
                        imgUsuario.Source = new BitmapImage(new Uri(saveFileDialog.FileName));
                        MessageBox.Show("Imagen guardada exitosamente.", "Guardar", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (IOException)
                {
                    MessageBox.Show("Error al cargar la imagen.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        private void txtNombre_GotFocus(object sender, RoutedEventArgs e)
        {
            Aparencias.txtGotFocus(1, txtNombre);
        }

        private void txtNombre_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Aparencias.SoloLetras(e);
        }

        private void txtNombre_LostFocus(object sender, RoutedEventArgs e)
        {
            Aparencias.txtLostFocus(1, txtNombre);
        }


        private void txtContra_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtContra.Text == null || txtContra.Text == "")
            {
                txtContra.Text = "Contraseña";
            }
        }

        private void txtContra_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtContra.Text == "Contraseña")
            {
                txtContra.Text = null;
            }
        }

        private void cbLaboratorio_GotFocus(object sender, RoutedEventArgs e)
        {
            if (cbLaboratorio.Text == "Laboratorio")
            {
                cbLaboratorio.Text = null;
            }
        }

        private void cbLaboratorio_LostFocus(object sender, RoutedEventArgs e)
        {
            string[] laboratorios = new string[cbLaboratorio.Items.Count];

            for (int i = 0; i < cbLaboratorio.Items.Count; i++)
            {
                DataRowView rowView = (DataRowView)cbLaboratorio.Items[i];
                laboratorios[i] = rowView["lab_nombre"].ToString();
            }

            foreach (string x in laboratorios)
            {
                if (cbLaboratorio.Text == x)
                {
                    cbLaboratorio.Text = x;
                    break;
                }
                else if (laboratorios.Contains(cbLaboratorio.Text))
                {

                }
                else
                {
                    cbLaboratorio.Text = "Laboratorio";
                }
            }
        }

        private void txtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            Aparencias.limitarTexto(sender, 30);
        }

        private void txtContra_TextChanged(object sender, TextChangedEventArgs e)
        {
            Aparencias.limitarTexto(sender, 30);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Variables.BoolEditar1 = false;
        }
    }
}
