using MySql.Data.MySqlClient;
using Prueba2_Projecto_integrador.Clases;
using System;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Prueba2_Projecto_integrador
{
    /// <summary>
    /// Interaction logic for UserControl_Prestamo.xaml
    /// </summary>
    /// 
    public partial class UserControl_Prestamo : UserControl
    {
        private static int intPrestamoEditar = 0;
        private static double doubleNumeroDeControl = 0;
        private static string strFecha = "";
        private string StrComando = "";

        private readonly string cmdAdmin = "SELECT tabla_prestamos.prestamos_id AS 'ID', " +
                                "DATE_FORMAT(tabla_prestamos.prestamos_fecha_prestamo, '%m/%d/%Y') AS 'Fecha Inicial', " +
                                "DATE_FORMAT(tabla_prestamos.prestamos_fecha_retorno, '%m/%d/%Y') AS 'Fecha de retorno', " +
                                "tabla_prestamos.prestamos_comentarios AS 'Comentarios', " +
                                "tabla_usuarios.usuarios_nombre AS 'Usuario', " +
                                "tabla_prestatario.prestatario_nombre AS 'Prestatario nombre', " +
                                "tabla_prestatario.prestatario_apellido AS 'Prestatario apellido', " +
                                "tabla_prestamos.prestatario_numControl AS 'Número de control', " +
                                "tabla_prestatario.prestatario_carrera AS 'Carrera', " +
                                "tabla_prestatario.prestatario_semestre AS 'Semestre', " +
                                "tabla_prestatario.prestatario_correo AS 'Correo', " +
                                "tabla_prestatario.prestatario_numero AS 'Número telefónico' " +
                                "FROM tabla_prestamos " +
                                "JOIN tabla_prestatario ON tabla_prestamos.prestatario_numControl = tabla_prestatario.prestatario_numControl " +
                                "JOIN tabla_usuarios ON tabla_prestamos.usuarios_id = tabla_usuarios.usuarios_id";
        public UserControl_Prestamo()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            if (Usuario.StrTpoUsuario == "Admin")
            {
                StrComando = cmdAdmin;

                ComandosSQL.ActualizaTabla(StrComando, dgPrestamos);
            }
            else
            {
                StrComando = @"SELECT tabla_prestamos.prestamos_id AS 'ID',
                               DATE_FORMAT(tabla_prestamos.prestamos_fecha_prestamo, '%m/%d/%Y') AS 'Fecha Inicial',
                               DATE_FORMAT(tabla_prestamos.prestamos_fecha_retorno, '%m/%d/%Y') AS 'Fecha de retorno',
                               tabla_prestamos.prestamos_comentarios AS 'Comentarios',
                               tabla_usuarios.usuarios_nombre AS 'Usuario',
                               tabla_prestatario.prestatario_nombre AS 'Prestatario nombre',
                               tabla_prestatario.prestatario_apellido AS 'Prestatario apellido',
                               tabla_prestamos.prestatario_numControl AS 'Número de control',
                               tabla_prestatario.prestatario_carrera AS 'Carrera',
                               tabla_prestatario.prestatario_semestre AS 'Semestre',
                               tabla_prestatario.prestatario_correo AS 'Correo',
                               tabla_prestatario.prestatario_numero AS 'Número telefónico'
                        FROM tabla_prestamos
                        JOIN tabla_prestatario ON tabla_prestamos.prestatario_numControl = tabla_prestatario.prestatario_numControl
                        JOIN tabla_usuarios ON tabla_prestamos.usuarios_id = tabla_usuarios.usuarios_id
                        WHERE tabla_usuarios.usuarios_nombre = '" + Usuario.StrNombreUsuario + "';";

                ComandosSQL.ActualizaTabla(StrComando, dgPrestamos);
            }
        }
        private void ActualizarTabla(object? sender, EventArgs e)
        {
            if (Usuario.StrTpoUsuario == "Admin")
            {
                StrComando = cmdAdmin;

                ComandosSQL.ActualizaTabla(StrComando, dgPrestamos);
            }
            else
            {
                StrComando = @"SELECT tabla_prestamos.prestamos_id AS 'ID',
                               DATE_FORMAT(tabla_prestamos.prestamos_fecha_prestamo, '%m/%d/%Y') AS 'Fecha Inicial',
                               DATE_FORMAT(tabla_prestamos.prestamos_fecha_retorno, '%m/%d/%Y') AS 'Fecha de retorno',
                               tabla_prestamos.prestamos_comentarios AS 'Comentarios',
                               tabla_usuarios.usuarios_nombre AS 'Usuario',
                               tabla_prestatario.prestatario_nombre AS 'Prestatario nombre',
                               tabla_prestatario.prestatario_apellido AS 'Prestatario apellido',
                               tabla_prestamos.prestatario_numControl AS 'Número de control',
                               tabla_prestatario.prestatario_carrera AS 'Carrera',
                               tabla_prestatario.prestatario_semestre AS 'Semestre',
                               tabla_prestatario.prestatario_correo AS 'Correo',
                               tabla_prestatario.prestatario_numero AS 'Número telefónico'
                        FROM tabla_prestamos
                        JOIN tabla_prestatario ON tabla_prestamos.prestatario_numControl = tabla_prestatario.prestatario_numControl
                        JOIN tabla_usuarios ON tabla_prestamos.usuarios_id = tabla_usuarios.usuarios_id
                        WHERE tabla_usuarios.usuarios_nombre = '" + Usuario.StrNombreUsuario + "';";

                ComandosSQL.ActualizaTabla(StrComando, dgPrestamos);
            }
        }

        private void txtBuscar_GotFocus(object sender, RoutedEventArgs e)
        {
            Aparencias.txtGotFocus(10, txtBuscar);
        }

        private void txtBuscar_LostFocus(object sender, RoutedEventArgs e)
        {
            Aparencias.txtLostFocus(10, txtBuscar);
        }

        private void btn_NuevoPrestamo_Click(object sender, RoutedEventArgs e)
        {
            Window_Prestamo window_Prestamo = new();
            window_Prestamo.Closed += new EventHandler(ActualizarTabla);
            window_Prestamo.ShowDialog();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            Variables.BoolEditar1 = true;
            Window_Prestamo window_Prestamo = new(intPrestamoEditar, doubleNumeroDeControl);
            window_Prestamo.Closed += new EventHandler(ActualizarTabla);
            window_Prestamo.ShowDialog();
        }

        private void dgPrestamos_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (e.AddedCells.Count > 0)
            {
                var selectedCell = e.AddedCells[0];
                var selectedRow = dgPrestamos.ItemContainerGenerator.ContainerFromItem(selectedCell.Item) as DataGridRow;

                selectedRow.MouseLeave += SelectedRow_MouseLeave;
            }

            DataGrid dataGrid = (DataGrid)sender;
            DataRowView row_selected = dataGrid.SelectedItem as DataRowView;

            if (row_selected != null)
            {
                intPrestamoEditar = int.Parse(row_selected["ID"].ToString());
                strFecha = row_selected["Fecha de retorno"].ToString();
                doubleNumeroDeControl = Double.Parse(row_selected["Número de control"].ToString());
            }
        }
        private void SelectedRow_MouseLeave(object sender, MouseEventArgs e)
        {
            var row = sender as DataGridRow;

            row.IsSelected = false;

            if (row.Background == Brushes.IndianRed)
            {
                row.Background = null;
            }

            row.MouseLeave -= SelectedRow_MouseLeave;
        }

        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                MySqlConnection conexion = Conexion.getConexion();

                if(Usuario.StrTpoUsuario == "Admin")
                {
                    string cmd = @"SELECT
                        tabla_prestamos.prestamos_id AS 'ID',
                        DATE_FORMAT(tabla_prestamos.prestamos_fecha_prestamo, '%m/%d/%Y') AS 'Fecha Inicial',
                        DATE_FORMAT(tabla_prestamos.prestamos_fecha_retorno, '%m/%d/%Y') AS 'Fecha de retorno',
                        tabla_prestamos.prestamos_comentarios AS 'Comentarios',
                        tabla_usuarios.usuarios_nombre AS 'Usuario',
                        tabla_prestatario.prestatario_nombre AS 'Prestatario nombre',
                        tabla_prestatario.prestatario_apellido AS 'Prestatario apellido',
                        tabla_prestamos.prestatario_numControl AS 'Número de control',
                        tabla_prestatario.prestatario_carrera AS 'Carrera',
                        tabla_prestatario.prestatario_semestre AS 'Semestre',
                        tabla_prestatario.prestatario_correo AS 'Correo',
                        tabla_prestatario.prestatario_numero AS 'Número telefónico'
                      FROM
                        tabla_prestamos
                      JOIN
                        tabla_prestatario ON tabla_prestamos.prestatario_numControl = tabla_prestatario.prestatario_numControl
                      JOIN
                        tabla_usuarios ON tabla_prestamos.usuarios_id = tabla_usuarios.usuarios_id
                      WHERE
                        tabla_prestamos.prestamos_id LIKE @parametro";

                    MySqlCommand comando = new(cmd, conexion);

                    comando.Parameters.AddWithValue("@parametro", "%" + txtBuscar.Text + "%");

                    conexion.Open();

                    MySqlDataAdapter adapter = new(comando);

                    DataTable dt = new();

                    adapter.Fill(dt);

                    conexion.Close();

                    dgPrestamos.DataContext = dt;
                }
                else
                {
                    string cmd = @"SELECT
                        tabla_prestamos.prestamos_id AS 'ID',
                        DATE_FORMAT(tabla_prestamos.prestamos_fecha_prestamo, '%m/%d/%Y') AS 'Fecha Inicial',
                        DATE_FORMAT(tabla_prestamos.prestamos_fecha_retorno, '%m/%d/%Y') AS 'Fecha de retorno',
                        tabla_prestamos.prestamos_comentarios AS 'Comentarios',
                        tabla_usuarios.usuarios_nombre AS 'Usuario',
                        tabla_prestatario.prestatario_nombre AS 'Prestatario nombre',
                        tabla_prestatario.prestatario_apellido AS 'Prestatario apellido',
                        tabla_prestamos.prestatario_numControl AS 'Número de control',
                        tabla_prestatario.prestatario_carrera AS 'Carrera',
                        tabla_prestatario.prestatario_semestre AS 'Semestre',
                        tabla_prestatario.prestatario_correo AS 'Correo',
                        tabla_prestatario.prestatario_numero AS 'Número telefónico'
                      FROM
                        tabla_prestamos
                      JOIN
                        tabla_prestatario ON tabla_prestamos.prestatario_numControl = tabla_prestatario.prestatario_numControl
                      JOIN
                        tabla_usuarios ON tabla_prestamos.usuarios_id = tabla_usuarios.usuarios_id
                      WHERE
                        tabla_usuarios.usuarios_nombre = @nombreUsuario AND
                        tabla_prestamos.prestamos_id LIKE @parametro";

                    MySqlCommand comando = new(cmd, conexion);
                    comando.Parameters.AddWithValue("@nombreUsuario", Usuario.StrNombreUsuario);
                    comando.Parameters.AddWithValue("@parametro", "%" + txtBuscar.Text + "%");

                    conexion.Open();

                    MySqlDataAdapter adapter = new(comando);

                    DataTable dt = new();

                    adapter.Fill(dt);

                    conexion.Close();

                    dgPrestamos.DataContext = dt;
                }  

                if (txtBuscar.Text == "Buscar")
                {
                    StrComando = @"SELECT tabla_prestamos.prestamos_id AS 'ID',
                               DATE_FORMAT(tabla_prestamos.prestamos_fecha_prestamo, '%m/%d/%Y') AS 'Fecha Inicial',
                               DATE_FORMAT(tabla_prestamos.prestamos_fecha_retorno, '%m/%d/%Y') AS 'Fecha de retorno',
                               tabla_prestamos.prestamos_comentarios AS 'Comentarios',
                               tabla_usuarios.usuarios_nombre AS 'Usuario',
                               tabla_prestatario.prestatario_nombre AS 'Prestatario nombre',
                               tabla_prestatario.prestatario_apellido AS 'Prestatario apellido',
                               tabla_prestamos.prestatario_numControl AS 'Número de control',
                               tabla_prestatario.prestatario_carrera AS 'Carrera',
                               tabla_prestatario.prestatario_semestre AS 'Semestre',
                               tabla_prestatario.prestatario_correo AS 'Correo',
                               tabla_prestatario.prestatario_numero AS 'Número telefónico'
                        FROM tabla_prestamos
                        JOIN tabla_prestatario ON tabla_prestamos.prestatario_numControl = tabla_prestatario.prestatario_numControl
                        JOIN tabla_usuarios ON tabla_prestamos.usuarios_id = tabla_usuarios.usuarios_id
                        WHERE tabla_usuarios.usuarios_nombre = '" + Usuario.StrNombreUsuario + "';";

                    ComandosSQL.ActualizaTabla(StrComando, dgPrestamos);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al actualizar la tabla " + ex.Message);
            }

        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {

            int intDevolver = 0;

            if (DateTime.TryParseExact(strFecha, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaIngresada))
            {
                if (fechaIngresada <= DateTime.Now.Date)
                {
                    MessageBoxResult resultado = MessageBox.Show("¿Desea continuar?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (resultado == MessageBoxResult.Yes)
                    {
                        string comando = "delete from tabla_prestamos where prestamos_id = '" + intPrestamoEditar + "';";

                        ComandosSQL.ComandoNonquery(comando);

                        comando = "SELECT COUNT(*) FROM tabla_prestatario_herramientas WHERE prestamos_id = '" + intPrestamoEditar + "';";

                        intDevolver = ComandosSQL.ComandoObtenerInt(comando, intDevolver);

                        comando = "UPDATE tabla_inventario " +
                              "JOIN tabla_prestatario_herramientas ON tabla_prestatario_herramientas.objeto_id = tabla_inventario.objeto_id " +
                              "SET tabla_inventario.objeto_cantidad = tabla_prestatario_herramientas.objeto_cantidad + tabla_inventario.objeto_cantidad " +
                              "WHERE prestamos_id = '" + intPrestamoEditar + "';";

                        ComandosSQL.ComandoNonquery(comando);

                        comando = "delete from bd_prestamos_itspp.tabla_prestatario where prestatario_numControl = '" + doubleNumeroDeControl + "' ;";

                        ComandosSQL.ComandoNonquery(comando);

                        comando = "delete from tabla_prestatario_herramientas where prestamos_id = '" + intPrestamoEditar + "';";

                        ComandosSQL.ComandoNonquery(comando);

                        if (Usuario.StrTpoUsuario == "Admin")
                        {
                            StrComando = cmdAdmin;

                            ComandosSQL.ActualizaTabla(StrComando, dgPrestamos);
                        }
                        else
                        {
                            StrComando = @"SELECT tabla_prestamos.prestamos_id AS 'ID',
                               DATE_FORMAT(tabla_prestamos.prestamos_fecha_prestamo, '%m/%d/%Y') AS 'Fecha Inicial',
                               DATE_FORMAT(tabla_prestamos.prestamos_fecha_retorno, '%m/%d/%Y') AS 'Fecha de retorno',
                               tabla_prestamos.prestamos_comentarios AS 'Comentarios',
                               tabla_usuarios.usuarios_nombre AS 'Usuario',
                               tabla_prestatario.prestatario_nombre AS 'Prestatario nombre',
                               tabla_prestatario.prestatario_apellido AS 'Prestatario apellido',
                               tabla_prestamos.prestatario_numControl AS 'Número de control',
                               tabla_prestatario.prestatario_carrera AS 'Carrera',
                               tabla_prestatario.prestatario_semestre AS 'Semestre',
                               tabla_prestatario.prestatario_correo AS 'Correo',
                               tabla_prestatario.prestatario_numero AS 'Número telefónico'
                                FROM tabla_prestamos
                                JOIN tabla_prestatario ON tabla_prestamos.prestatario_numControl = tabla_prestatario.prestatario_numControl
                                JOIN tabla_usuarios ON tabla_prestamos.usuarios_id = tabla_usuarios.usuarios_id
                                WHERE tabla_usuarios.usuarios_nombre = '" + Usuario.StrNombreUsuario + "';";

                            ComandosSQL.ActualizaTabla(StrComando, dgPrestamos);
                        }

                        StrComando = "INSERT INTO `bd_prestamos_itspp`.`tabla_bitacora` (`bitacora_fecha`, `bitacora_operacion`) VALUES ('Fecha: "+DateTime.Now.ToString()+"', 'El usuario: "+Usuario.StrNombreUsuario+" regreso el prestamo: "+intPrestamoEditar.ToString()+"');;";
                        ComandosSQL.ComandoNonquery(StrComando);

                    }
                }
                else
                {
                    MessageBox.Show("Lamentablemente, en este momento no se posible reembolsar el préstamo.", "¡Aviso!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }




        }
        private void dgPrestamos_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            DataGridRow row = e.Row;
            DataRowView rowView = e.Row.Item as DataRowView;

            if (rowView != null)
            {
                DataRow dataRow = rowView.Row;

                if (dataRow.Table.Columns.Contains("Fecha de retorno"))
                {

                    DateTime fechaRetorno = Convert.ToDateTime(dataRow["Fecha de retorno"]);


                    DateTime fechaActual = DateTime.Now.Date;


                    if (fechaRetorno.Date <= fechaActual)
                    {

                        row.Background = new SolidColorBrush(Colors.IndianRed);
                    }
                }
            }
        }

        private void txtBuscar_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsNumericInput(e.Text) || e.Text == " ")
            {
                MessageBox.Show("Solo se permiten números.\nRecuerda buscar por el ID del prestamo, tontit@ ;)", "Información", MessageBoxButton.OK, MessageBoxImage.Information);

                e.Handled = true;
            }
        }

        private bool IsNumericInput(string text)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(text, @"^[0-9]+$");
        }

    }
}
