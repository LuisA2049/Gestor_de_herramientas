using Prueba2_Projecto_integrador.Clases;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Prueba2_Projecto_integrador
{
    /// <summary>
    /// Interaction logic for Window_Inventario.xaml
    /// </summary>
    public partial class Window_Inventario : Window
    {

        private readonly int _inventarioId;
        private string estado = "";
        private string? comando = null;

        public Window_Inventario()
        {
            InitializeComponent();
        }

        public Window_Inventario(int IntHerramienraID)
        {
            InitializeComponent();
            _inventarioId = IntHerramienraID;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                comando = "SELECT * FROM bd_prestamos_itspp.tabla_labs;";

                ComandosSQL.CargarComboBox(comando, cbLaboratorio, "lab_nombre", "lab_id");

                if (Variables.BoolEditar1)
                {
                    comando = "SELECT objeto_nombre FROM bd_prestamos_itspp.tabla_inventario WHERE objeto_id ='" + _inventarioId + "'";

                    txtNombre.Text = ComandosSQL.ComandoObtenerString(comando, txtNombre.Text);

                    comando = "SELECT objeto_descripcion FROM bd_prestamos_itspp.tabla_inventario WHERE objeto_id ='" + _inventarioId + "'";

                    txtDescripcion.Text = ComandosSQL.ComandoObtenerString(comando, txtDescripcion.Text);

                    comando = "SELECT objeto_cantidad FROM bd_prestamos_itspp.tabla_inventario WHERE objeto_id ='" + _inventarioId + "'";

                    txtCantidad.Text = ComandosSQL.ComandoObtenerString(comando, txtCantidad.Text);

                    comando = "SELECT lab_nombre FROM bd_prestamos_itspp.tabla_inventario JOIN bd_prestamos_itspp.tabla_labs ON tabla_inventario.lab_id = '" + _inventarioId + "'";

                    cbLaboratorio.Text = ComandosSQL.ComandoObtenerString(comando, cbLaboratorio.Text);

                    comando = "SELECT objeto_estado FROM bd_prestamos_itspp.tabla_inventario WHERE objeto_id ='" + _inventarioId + "'";

                    estado = ComandosSQL.ComandoObtenerString(comando, estado);

                    switch (estado)
                    {
                        case "Excelente":

                            rdExcelente.IsChecked = true;

                            break;

                        case "Bueno":

                            rdBueno.IsChecked = true;

                            break;


                        case "Malo":

                            rdMalo.IsChecked = true;

                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnListo_Click(object sender, RoutedEventArgs e)
        {
            if (txtNombre.Text == ""
                || txtNombre.Text == "Nombre"
                || txtDescripcion.Text == ""
                || txtDescripcion.Text == "Descripción"
                || txtCantidad.Text == ""
                || txtCantidad.Text == "Cantidad"
                || cbLaboratorio.Text == ""
                || cbLaboratorio.Text == "Laboratorio")
            {
                MessageBox.Show("Asegurate de llenar todos los campos.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                string comando;

                if (Variables.BoolEditar1)
                {
                    string strLabid = cbLaboratorio.SelectedValue.ToString();

                    comando = "UPDATE `bd_prestamos_itspp`.`tabla_inventario` SET `objeto_nombre` = '" + txtNombre.Text + "'," +
                             " `objeto_descripcion` = '" + txtDescripcion.Text + "', `objeto_cantidad` = '" + int.Parse(txtCantidad.Text) + "', `lab_id` = '" + int.Parse(strLabid) + "'" +
                             "where objeto_id='" + _inventarioId + "'; ";

                    ComandosSQL.ComandoNonquery(comando);

                    comando = "INSERT INTO `bd_prestamos_itspp`.`tabla_bitacora` (`bitacora_fecha`, `bitacora_operacion`) VALUES ('Fecha: " + DateTime.Now.ToString() + "', 'El usuario: " + Usuario.StrNombreUsuario + " edito la herramienta/objeto: " + txtNombre.Text +"' );";

                    ComandosSQL.ComandoNonquery(comando);

                    this.Close();
                }
                else
                {
                    comando = "INSERT INTO `bd_prestamos_itspp`.`tabla_inventario` (`objeto_nombre`, `objeto_descripcion`, `objeto_cantidad`, `objeto_disponible`, `objeto_estado`, `lab_id`) " +
                        "VALUES ('" + txtNombre.Text + "', '" + txtDescripcion.Text + "', '" + int.Parse(txtCantidad.Text) + "', 'Disponible', '" + estado + "', '" + cbLaboratorio.SelectedValue + "');";

                    ComandosSQL.ComandoNonquery(comando);

                    comando = "INSERT INTO `bd_prestamos_itspp`.`tabla_bitacora` (`bitacora_fecha`, `bitacora_operacion`) VALUES ('Fecha: " + DateTime.Now.ToString() + "', 'El usuario: " + Usuario.StrNombreUsuario + " agrego la herramienta/objeto: " + txtNombre.Text + "' );";

                    ComandosSQL.ComandoNonquery(comando);

                    this.Close();
                }
            }

        }

        private void txtNombre_GotFocus(object sender, RoutedEventArgs e)
        {
            Aparencias.txtGotFocus(1, txtNombre);
        }

        private void txtNombre_LostFocus(object sender, RoutedEventArgs e)
        {
            Aparencias.txtLostFocus(1, txtNombre);
        }

        private void txtDescripcion_LostFocus(object sender, RoutedEventArgs e)
        {
            Aparencias.txtLostFocus(11, txtDescripcion);
        }

        private void txtDescripcion_GotFocus(object sender, RoutedEventArgs e)
        {
            Aparencias.txtGotFocus(11, txtDescripcion);
        }

        private void txtCantidad_GotFocus(object sender, RoutedEventArgs e)
        {
            Aparencias.txtGotFocus(12, txtCantidad);
        }

        private void txtCantidad_LostFocus(object sender, RoutedEventArgs e)
        {
            Aparencias.txtLostFocus(12, txtCantidad);
        }

        private void cbLaboratorio_GotFocus(object sender, RoutedEventArgs e)
        {
            Aparencias.cbGotFocus(4, cbLaboratorio);
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
            Aparencias.limitarTexto(sender, 45);
        }

        private void txtDescripcion_TextChanged(object sender, TextChangedEventArgs e)
        {
            Aparencias.limitarTexto(sender, 200);
        }

        private void txtCantidad_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtCantidad.Text != "Cantidad")
            {
                TextBox textBox = (TextBox)sender;
                int maxLength = 4; // Definir el límite máximo de caracteres

                if (textBox.Text.Length > maxLength)
                {
                    textBox.Text = textBox.Text.Substring(0, maxLength); // Recortar el texto excedente
                    textBox.CaretIndex = maxLength; // Colocar el cursor al final del texto
                }
            }
        }

        private void txtNombre_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }
        private void txtCantidad_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            Aparencias.SoloNumeros(e);
        }

        private void rdMalo_Checked(object sender, RoutedEventArgs e)
        {
            estado = rdMalo.Content.ToString();
        }

        private void rdBueno_Checked(object sender, RoutedEventArgs e)
        {
            estado = rdBueno.Content.ToString();
        }

        private void rdExcelente_Checked(object sender, RoutedEventArgs e)
        {
            estado = rdExcelente.Content.ToString();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Variables.BoolEditar1 = false;
        }


    }
}
