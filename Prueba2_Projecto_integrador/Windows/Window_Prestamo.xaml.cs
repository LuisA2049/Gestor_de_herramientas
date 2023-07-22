using MySql.Data.MySqlClient;
using Prueba2_Projecto_integrador.Clases;
using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Prueba2_Projecto_integrador
{
    public partial class Window_Prestamo : Window
    {

        private string strObjetoId = "";
        private double DoubleNumeroDeControl = 0;
        private bool boolClickEliminar = false;
        private int IntNumConTemporal = 0;
        private string Comando = "";

        public Window_Prestamo()
        {
            InitializeComponent();
            rdExcelente.IsChecked = true;
            dpPrestamo.DisplayDateStart = DateTime.Now;
        }

        public Window_Prestamo(int IntPrestamoId, Double doubleNumeroControl)
        {
            InitializeComponent();
            rdExcelente.IsChecked = true;
            dpPrestamo.DisplayDateStart = DateTime.Now;
            txtNumero.Text = IntPrestamoId.ToString();
            DoubleNumeroDeControl = doubleNumeroControl;
        }

        private void CargarComboBox(RadioButton radioButton)
        {
            try
            {
                if (Usuario.IntLabUsuario == 9)
                {
                    Comando = "SELECT * FROM bd_prestamos_itspp.tabla_inventario where objeto_disponible = 'Disponible' and objeto_estado ='" + radioButton.Content + "'";

                    ComandosSQL.CargarComboBox(Comando, cbBuscar, "objeto_nombre", "objeto_id");

                }
                else
                {
                    Comando = "SELECT * FROM bd_prestamos_itspp.tabla_inventario where objeto_disponible = 'Disponible' and objeto_estado ='" + radioButton.Content + "'" +
                        "and lab_id ='" + Usuario.IntLabUsuario + "' ";

                    ComandosSQL.CargarComboBox(Comando, cbBuscar, "objeto_nombre", "objeto_id");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private string ObtenerFilasDelDataGrid(DataGrid dataGrid)
        {
            StringBuilder stringBuilder = new StringBuilder();

            // Recorremos cada fila del DataGrid
            foreach (var item in dataGrid.Items)
            {
                string fila = ObtenerFilaComoCadena(item, dataGrid);

                // Agregamos la fila al StringBuilder
                stringBuilder.AppendLine(fila);
            }

            // Devolvemos la cadena resultante
            return stringBuilder.ToString();
        }

        private string ObtenerFilaComoCadena(object item, DataGrid dataGrid)
        {
            StringBuilder filaBuilder = new StringBuilder();

            // Recorremos cada columna de la fila
            foreach (var column in dataGrid.Columns)
            {
                string valor = ObtenerValorCelda(item, column);

                // Agregamos el valor al StringBuilder, separado por comas o el carácter que desees.
                filaBuilder.Append(valor + ",");
            }

            // Eliminamos la última coma (si lo deseas) y devolvemos la fila como cadena.
            if (filaBuilder.Length > 0)
                filaBuilder.Length--; // Elimina el último carácter (la última coma)

            return filaBuilder.ToString();
        }

        private string ObtenerValorCelda(object item, DataGridColumn column)
        {
            var cellContent = column.GetCellContent(item);
            if (cellContent is TextBlock textBlock)
            {
                return textBlock.Text;
            }

            return string.Empty;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlConnection conexion = Conexion.getConexion();

                CargarComboBox(rdExcelente);

                if (!Variables.BoolEditar1)
                {
                    MySqlCommand comando = new("SELECT max(prestamos_id) FROM tabla_prestamos;", conexion);

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if(reader.IsDBNull(0) || reader.GetString(0) == "0")
                            {
                                txtNumero.Text = "1";
                            }
                            else
                            {
                                int x = int.Parse(reader.GetString(0)) + 1;
                                txtNumero.Text = x.ToString();
                            }
                        }
                    }

                    conexion.Close();
                }

                if (Variables.BoolEditar1)
                {
                    string comando = "SELECT prestatario_nombre FROM bd_prestamos_itspp.tabla_prestatario WHERE prestatario_numControl ='" + DoubleNumeroDeControl + "'";

                    txtNombre.Text = ComandosSQL.ComandoObtenerString(comando, txtNombre.Text);

                    comando = "SELECT prestatario_apellido FROM bd_prestamos_itspp.tabla_prestatario WHERE prestatario_numControl ='" + DoubleNumeroDeControl + "'";

                    txtApellido.Text = ComandosSQL.ComandoObtenerString(comando, txtApellido.Text);

                    txtNumControl.Text = DoubleNumeroDeControl.ToString();

                    IntNumConTemporal = int.Parse(txtNumControl.Text.ToString());

                    comando = "SELECT prestatario_carrera FROM bd_prestamos_itspp.tabla_prestatario WHERE prestatario_numControl ='" + DoubleNumeroDeControl + "'";

                    cbCarrera.Text = ComandosSQL.ComandoObtenerString(comando, cbCarrera.Text);

                    comando = "SELECT prestatario_semestre FROM bd_prestamos_itspp.tabla_prestatario WHERE prestatario_numControl ='" + DoubleNumeroDeControl + "'";

                    cbSemestre.Text = ComandosSQL.ComandoObtenerString(comando, cbSemestre.Text);

                    comando = "SELECT prestatario_correo FROM bd_prestamos_itspp.tabla_prestatario WHERE prestatario_numControl ='" + DoubleNumeroDeControl + "'";

                    txtCorreo.Text = ComandosSQL.ComandoObtenerString(comando, txtCorreo.Text);

                    comando = "SELECT prestatario_numero FROM bd_prestamos_itspp.tabla_prestatario WHERE prestatario_numControl ='" + DoubleNumeroDeControl + "'";

                    txtNumeroCelular.Text = ComandosSQL.ComandoObtenerString(comando, txtNumeroCelular.Text);

                    comando = "SELECT prestamos_fecha_prestamo FROM bd_prestamos_itspp.tabla_prestamos WHERE prestatario_numControl ='" + DoubleNumeroDeControl + "'";

                    dpPrestamo.Text = ComandosSQL.ComandoObtenerString(comando, dpPrestamo.Text);

                    comando = "SELECT prestamos_fecha_retorno FROM bd_prestamos_itspp.tabla_prestamos WHERE prestatario_numControl ='" + DoubleNumeroDeControl + "'";

                    dpRetorno.Text = ComandosSQL.ComandoObtenerString(comando, dpRetorno.Text);

                    comando = "SELECT tabla_inventario.objeto_nombre as  \"Nombre\"," +
                    "tabla_inventario.objeto_descripcion as  \"Descripción\", " +
                    "tabla_prestatario_herramientas.objeto_cantidad as  \"Cantidad\"," +
                    "tabla_inventario.objeto_estado as  \"Estado\" " +
                       "FROM bd_prestamos_itspp.tabla_inventario " +
                       "INNER JOIN tabla_prestatario_herramientas ON tabla_prestatario_herramientas.objeto_id = tabla_inventario.objeto_id " +
                       "WHERE prestamos_id='" + int.Parse(txtNumero.Text) + "'";

                    ComandosSQL.ActualizaTabla(comando, dgHerramientas_por_prestar);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtNombre_LostFocus(object sender, RoutedEventArgs e)
        {
            Aparencias.txtLostFocus(1, txtNombre);
        }
        private void txtNombre_GotFocus(object sender, RoutedEventArgs e)
        {
            Aparencias.txtGotFocus(1, txtNombre);
        }
        private void txtNombre_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Aparencias.SoloLetras(e);
        }
        private void txtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            Aparencias.limitarTexto(sender, 30);
        }
        private void txtApellido_GotFocus(object sender, RoutedEventArgs e)
        {
            Aparencias.txtGotFocus(2, txtApellido);
        }

        private void txtApellido_LostFocus(object sender, RoutedEventArgs e)
        {
            Aparencias.txtLostFocus(2, txtApellido);
        }
        private void txtApellido_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Aparencias.SoloLetras(e);
        }

        private void txtApellido_TextChanged(object sender, TextChangedEventArgs e)
        {
            Aparencias.limitarTexto(sender, 30);
        }
        private void txtNumControl_GotFocus(object sender, RoutedEventArgs e)
        {
            Aparencias.txtGotFocus(3, txtNumControl);
        }

        private void txtNumControl_LostFocus(object sender, RoutedEventArgs e)
        {
            Aparencias.txtLostFocus(3, txtNumControl);
        }
        private void txtNumControl_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Aparencias.SoloNumeros(e);
        }
        private void txtNumControl_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtNumControl.Text != "Número de control")
            {
                TextBox textBox = (TextBox)sender;
                int max = 8;

                if (textBox.Text.Length > max)
                {
                    textBox.Text = textBox.Text.Substring(0, max);
                    textBox.CaretIndex = max;
                }
            }
        }
        private void txtComentario_LostFocus(object sender, RoutedEventArgs e)
        {
            Aparencias.txtLostFocus(13, txtComentario);
        }

        private void txtComentario_GotFocus(object sender, RoutedEventArgs e)
        {
            Aparencias.txtGotFocus(13, txtComentario);
        }

        private void txtComentario_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {

        }

        private void txtCorreo_GotFocus(object sender, RoutedEventArgs e)
        {
            Aparencias.txtGotFocus(6, txtCorreo);
        }

        private void txtCorreo_LostFocus(object sender, RoutedEventArgs e)
        {
            Aparencias.txtLostFocus(6, txtCorreo);
        }
        private void txtCorreo_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {

        }

        private void txtCorreo_TextChanged(object sender, TextChangedEventArgs e)
        {
            Aparencias.limitarTexto(sender, 60);
        }
        private void txtNumeroCelular_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtNumeroCelular.Text != "Número telefónico")
            {
                TextBox textBox = (TextBox)sender;
                int max = 10;

                if (textBox.Text.Length > max)
                {
                    textBox.Text = textBox.Text.Substring(0, max);
                    textBox.CaretIndex = max;
                }
            }
        }

        private void txtNumeroCelular_LostFocus(object sender, RoutedEventArgs e)
        {
            Aparencias.txtLostFocus(7, txtNumeroCelular);
        }

        private void txtNumeroCelular_GotFocus(object sender, RoutedEventArgs e)
        {
            Aparencias.txtGotFocus(7, txtNumeroCelular);
        }

        private void txtNumeroCelular_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Aparencias.SoloNumeros(e);
        }

        private void cbCarrera_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void cbCarrera_GotFocus(object sender, RoutedEventArgs e)
        {
            Aparencias.cbGotFocus(1, cbCarrera);
        }

        private void cbSemestre_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void cbSemestre_GotFocus(object sender, RoutedEventArgs e)
        {
            Aparencias.cbGotFocus(2, cbSemestre);
        }

        private void cbBuscar_LostFocus(object sender, RoutedEventArgs e)
        {
            string[] herramientas = new string[cbBuscar.Items.Count];

            for (int i = 0; i < cbBuscar.Items.Count; i++)
            {
                DataRowView rowView = (DataRowView)cbBuscar.Items[i];

                herramientas[i] = rowView["objeto_nombre"].ToString();
            }

            foreach (string x in herramientas)
            {
                if (cbBuscar.Text == x)
                {
                    cbBuscar.Text = x;
                    break;
                }
                else if (herramientas.Contains(cbBuscar.Text))
                {

                }
                else
                {
                    cbBuscar.Text = "Buscar";
                    txtDescripcion.Text = null;
                    cbCantidad.Items.Clear();
                }
            }
        }

        private void cbBuscar_GotFocus(object sender, RoutedEventArgs e)
        {
            Aparencias.cbGotFocus(3, cbBuscar);
        }

        private void cbBuscar_IsMouseCaptureWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            cbCantidad.Items.Clear();

            MySqlConnection conexion = Conexion.getConexion();

            MySqlCommand comando = new("SELECT objeto_descripcion, objeto_cantidad FROM bd_prestamos_itspp.tabla_inventario\r\nwhere objeto_nombre like  '" + cbBuscar.Text + "' LIMIT 1", conexion);

            conexion.Open();

            MySqlDataReader reader = comando.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    txtDescripcion.Text = reader.GetString(0);
                }

                if (reader.GetString(1) != "0")
                {
                    for (int i = 1; i <= int.Parse(reader.GetString(1)); i++)
                    {
                        cbCantidad.Items.Add(i.ToString());
                    }
                    cbCantidad.Text = cbCantidad.Items[0].ToString();
                }
            }

            conexion.Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int intCantidadHerramientas = 0;
                int intObjetoId = 0;
                int intObjetoCantidad = 0;

                string comando = "SELECT COUNT(*) FROM bd_prestamos_itspp.tabla_prestatario_herramientas WHERE prestamos_id = '"
                    + int.Parse(txtNumero.Text) + "'";

                ComandosSQL.ComandoObtenerInt(comando, intCantidadHerramientas);

                for (int i = 0; i < intCantidadHerramientas; i++)
                {
                    comando = "SELECT objeto_id FROM bd_prestamos_itspp.tabla_prestatario_herramientas WHERE prestamos_id = '"
                        + int.Parse(txtNumero.Text) + "'";

                    ComandosSQL.ComandoObtenerInt(comando, intObjetoId);

                    comando = "SELECT objeto_cantidad FROM bd_prestamos_itspp.tabla_prestatario_herramientas WHERE objeto_id ='"
                        + intObjetoId + "'";

                    ComandosSQL.ComandoObtenerInt(comando, intObjetoCantidad);


                    comando = "UPDATE `bd_prestamos_itspp`.`tabla_inventario` SET `objeto_cantidad` =  objeto_cantidad + " + intObjetoCantidad +
                        " WHERE (`objeto_id` = '" + intObjetoId + "');";

                    ComandosSQL.ComandoNonquery(comando);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }

            this.Close();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (cbBuscar.Text == "Buscar"
               || cbBuscar.Text == null
               || cbCantidad.Text == "Cantidad"
               || cbCantidad.Text == null)
            {
                MessageBox.Show("Por favor, asegúrate de completar todos los campos. Evita dejar espacios en blanco.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                try
                {
                    string comando = "INSERT INTO `bd_prestamos_itspp`.`tabla_prestatario_herramientas`" +
                        " (`prestamos_id`, `objeto_id`, `objeto_cantidad`) " +
                        "VALUES ('" + int.Parse(txtNumero.Text) + "', '" + cbBuscar.SelectedValue + "', '" + int.Parse(cbCantidad.Text) + "')";

                    ComandosSQL.ComandoNonquery(comando);

                    comando = "update tabla_inventario set objeto_cantidad " +
                        "= objeto_cantidad - '" + int.Parse(cbCantidad.Text) + "' where objeto_id = '" + cbBuscar.SelectedValue + "'";

                    ComandosSQL.ComandoNonquery(comando);

                    comando = "SELECT tabla_inventario.objeto_nombre as  \"Nombre\"," +
                    "tabla_inventario.objeto_descripcion as  \"Descripción\", " +
                    "tabla_prestatario_herramientas.objeto_cantidad as  \"Cantidad\"," +
                    "tabla_inventario.objeto_estado as  \"Estado\" " +
                       "FROM bd_prestamos_itspp.tabla_inventario " +
                       "INNER JOIN tabla_prestatario_herramientas ON tabla_prestatario_herramientas.objeto_id = tabla_inventario.objeto_id " +
                       "WHERE prestamos_id='" + int.Parse(txtNumero.Text) + "'";

                    ComandosSQL.ActualizaTabla(comando, dgHerramientas_por_prestar);

                    cbBuscar.Text = "Buscar";
                    txtDescripcion.Text = "Descripción";
                    cbCantidad.Items.Clear();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnListo_Click(object sender, RoutedEventArgs e)
        {
            if (txtNombre.Text == "Nombre"
                || txtApellido.Text == "Apellido"
                || txtNumControl.Text == "Número de control"
                || cbCarrera.Text == "Carrera"
                || cbSemestre.Text == "Semestre"
                || txtCorreo.Text == "Correo electrónico"
                || txtNumeroCelular.Text == "Número telefónico"
                || dpPrestamo.Text == "")
            {
                MessageBox.Show("No dejar espacios en blanco.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                string? StrFormatoFechaDpPrestamo = dpPrestamo.SelectedDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                string? StrFormatoFechaDpRetorno = dpRetorno.SelectedDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                try
                {
                    if (!Variables.BoolEditar1)
                    {

                        string comando = "SELECT prestatario_numControl FROM bd_prestamos_itspp.tabla_prestatario WHERE prestatario_numControl ='" + int.Parse(txtNumControl.Text) + "'";

                        bool existe = false;

                        existe = ComandosSQL.ComandoExiste(comando, existe);

                        if (existe)
                        {
                            MessageBox.Show("Ya esta hecho un prestamo con ese Número de control.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            if (dgHerramientas_por_prestar.Items.Count > 0)
                            {
                                comando = "INSERT INTO `bd_prestamos_itspp`.`tabla_prestatario` " +
                                    "(`prestatario_numControl`, `prestatario_nombre`, `prestatario_apellido`," +
                                    " `prestatario_carrera`, `prestatario_semestre`, `prestatario_correo`, `prestatario_numero`) VALUES " +
                                    "('" + int.Parse(txtNumControl.Text) + "', '" + txtNombre.Text + "', '" + txtApellido.Text + "', '" + cbCarrera.Text + "', '" + cbSemestre.Text + "', '" + txtCorreo.Text + "', '" + Double.Parse(txtNumeroCelular.Text) + "');";

                                ComandosSQL.ComandoNonquery(comando);

                                comando = "INSERT INTO `bd_prestamos_itspp`.`tabla_prestamos` " +
                               "(`prestamos_id`, `prestamos_fecha_prestamo`, `prestamos_fecha_retorno`, `prestamos_comentarios`, `usuarios_id`, `prestatario_numControl`) VALUES " +
                               "('" + int.Parse(txtNumero.Text) + "', '" + StrFormatoFechaDpPrestamo + "', '" + StrFormatoFechaDpRetorno + "', '" + txtComentario.Text + "', '" + Usuario.IntIdUsuario + "', '" + int.Parse(txtNumControl.Text) + "');";

                                ComandosSQL.ComandoNonquery(comando);

                                string StrHerramientasPrestatario = ObtenerFilasDelDataGrid(dgHerramientas_por_prestar).Substring(1);

                                comando = "INSERT INTO `bd_prestamos_itspp`.`tabla_bitacora` (`bitacora_fecha`, `bitacora_operacion`) VALUES ('Fecha: " + DateTime.Now.ToString() + "', 'El usuario: " + Usuario.StrNombreUsuario + " realizo el prestamo: " + txtNumero.Text + " confirmo el prestamo de las herramienta(s)/objeto(s): " + StrHerramientasPrestatario + "' );";

                                ComandosSQL.ComandoNonquery(comando);

                                this.Close();

                            }
                            else
                            {
                                MessageBox.Show("No dejar espacios en blanco.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                    }
                    else
                    {
                        if (dgHerramientas_por_prestar.Items.Count > 0)
                        {
                            string comando = "UPDATE `bd_prestamos_itspp`.`tabla_prestatario` SET `prestatario_numControl` = '" + DoubleNumeroDeControl + "'," +
                           "`prestatario_nombre` = '" + txtNombre.Text + "', `prestatario_apellido` = '" + txtApellido.Text + "', " +
                           "`prestatario_carrera` = '" + cbCarrera.Text + "', `prestatario_semestre` = '" + cbSemestre.Text + "', " +
                           "`prestatario_correo` = '" + txtCorreo.Text + "', `prestatario_numero` = '" + double.Parse(txtNumeroCelular.Text) + "' " +
                           "WHERE (`prestatario_numControl` = '" + IntNumConTemporal + "');";

                            ComandosSQL.ComandoNonquery(comando);

                            comando = "UPDATE `bd_prestamos_itspp`.`tabla_prestamos` SET `prestamos_fecha_prestamo` = '" + StrFormatoFechaDpPrestamo + "'," +
                                " `prestamos_fecha_retorno` = '" + StrFormatoFechaDpRetorno + "', `prestamos_comentarios` = '" + txtComentario.Text + "', " +
                                "`usuarios_id` = '" + Usuario.IntIdUsuario + "', `prestatario_numControl` = '" + DoubleNumeroDeControl + "' WHERE (`prestamos_id` = '" + int.Parse(txtNumero.Text) + "');";

                            ComandosSQL.ComandoNonquery(comando);

                            string StrHerramientasPrestatario = ObtenerFilasDelDataGrid(dgHerramientas_por_prestar).Substring(1);

                            comando = "INSERT INTO `bd_prestamos_itspp`.`tabla_bitacora` (`bitacora_fecha`, `bitacora_operacion`) VALUES ('Fecha: " + DateTime.Now.ToString() + "', 'El usuario: " + Usuario.StrNombreUsuario + " actulizo el prestamo: " + txtNumero.Text + " confirmo el prestamo de las siguientes herramienta(s)/objeto(s): " + StrHerramientasPrestatario + "' );";

                            ComandosSQL.ComandoNonquery(comando);

                            this.Close();

                        }
                        else
                        {
                            MessageBox.Show("No dejar espacios en blanco.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        }    
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    this.Close();
                }
            }
        }

        private void dpPrestamo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            dpRetorno.IsEnabled = true;
            dpRetorno.DisplayDateStart = dpPrestamo.SelectedDate;
            dpRetorno.Text = dpPrestamo.SelectedDate.ToString();
            if (dpPrestamo.Text == "")
            {
                dpRetorno.IsEnabled = false;
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            boolClickEliminar = true;
        }

        private void dgHerramientas_por_prestar_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;

            DataRowView row_selected = dataGrid.SelectedItem as DataRowView;

            if (row_selected != null)
            {
                if (boolClickEliminar)
                {
                    string comando = "SELECT objeto_id FROM tabla_inventario WHERE objeto_nombre ='" + row_selected["Nombre"].ToString() + "';";

                    strObjetoId = ComandosSQL.ComandoObtenerString(comando, strObjetoId);

                    comando = "DELETE FROM bd_prestamos_itspp.tabla_prestatario_herramientas WHERE prestamos_id ='" + int.Parse(txtNumero.Text) + "'"
                        + "AND objeto_id ='" + int.Parse(strObjetoId) + "'" + "AND objeto_cantidad = '" + int.Parse(row_selected["Cantidad"].ToString()) + "' LIMIT 1;";

                    ComandosSQL.ComandoNonquery(comando);

                    comando = "UPDATE tabla_inventario SET objeto_cantidad = objeto_cantidad + " + int.Parse(row_selected["Cantidad"].ToString()) + " WHERE objeto_id = '" + strObjetoId + "'";

                    ComandosSQL.ComandoNonquery(comando);

                    comando = "SELECT tabla_inventario.objeto_nombre as  \"Nombre\"," +
                         " tabla_inventario.objeto_descripcion as  \"Descripción\", " +
                         "tabla_prestatario_herramientas.objeto_cantidad as  \"Cantidad\"" +
                            "FROM bd_prestamos_itspp.tabla_inventario " +
                            "INNER JOIN tabla_prestatario_herramientas ON tabla_prestatario_herramientas.objeto_id = tabla_inventario.objeto_id " +
                            "WHERE prestamos_id='" + int.Parse(txtNumero.Text) + "'";

                    ComandosSQL.ActualizaTabla(comando, dgHerramientas_por_prestar);

                    cbBuscar.Text = "Buscar";
                    txtDescripcion.Text = "Descripción";
                    cbCantidad.Items.Clear();

                }
            }
        }

        private void btnEliminar_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            boolClickEliminar = true;
        }

        private void btnEliminar_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            boolClickEliminar = false;
        }

        private void dpRetorno_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Variables.BoolEditar1 = false;
        }

        private void rdExcelente_Checked(object sender, RoutedEventArgs e)
        {
            CargarComboBox(rdExcelente);
            txtDescripcion.Text = null;
            cbCantidad.Items.Clear();
        }

        private void rdExcelente_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void rdBueno_Checked(object sender, RoutedEventArgs e)
        {
            CargarComboBox(rdBueno);
            txtDescripcion.Text = null;
            cbCantidad.Items.Clear();
        }

        private void rdBueno_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void rdMalo_Checked(object sender, RoutedEventArgs e)
        {
            CargarComboBox(rdMalo);
            txtDescripcion.Text = null;
            cbCantidad.Items.Clear();
        }

        private void rdMalo_Unchecked(object sender, RoutedEventArgs e)
        {

        }
    }

}