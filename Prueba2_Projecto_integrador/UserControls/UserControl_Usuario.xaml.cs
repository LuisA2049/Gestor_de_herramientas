using MySql.Data.MySqlClient;
using Prueba2_Projecto_integrador.Clases;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Prueba2_Projecto_integrador
{
    /// <summary>
    /// Interaction logic for UserControl_Usuario.xaml
    /// </summary>
    public partial class UserControl_Usuario : UserControl
    {
        private readonly string Comando = @"SELECT usuarios_id AS ""ID"",
                       usuarios_nombre AS ""Nombre"",
                       usuarios_tipo AS ""Tipo"",
                       tabla_labs.lab_nombre AS ""Laboratorio""
                FROM bd_prestamos_itspp.tabla_usuarios 
                JOIN bd_prestamos_itspp.tabla_labs 
                ON bd_prestamos_itspp.tabla_usuarios.lab_id = bd_prestamos_itspp.tabla_labs.lab_id;
                ";
        private static int intUsuarioEditar = 0;
        public UserControl_Usuario()
        {
            InitializeComponent();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ComandosSQL.ActualizaTabla(Comando, dg);
        }

        private void ActualizarTabla(object? sender, EventArgs e)
        {


            ComandosSQL.ActualizaTabla(Comando, dg);
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            Variables.BoolEditar1 = true;
            Window_Usuario window_Usuario = new(intUsuarioEditar);
            window_Usuario.Closed += new EventHandler(ActualizarTabla);
            window_Usuario.ShowDialog();
        }

        private void dg_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;
            DataRowView row_selected = dataGrid.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                intUsuarioEditar = int.Parse(row_selected["ID"].ToString());
            }
        }

        private void txtBuscar_LostFocus(object sender, RoutedEventArgs e)
        {
            Aparencias.txtLostFocus(10, txtBuscar);
        }

        private void txtBuscar_GotFocus(object sender, RoutedEventArgs e)
        {
            Aparencias.txtGotFocus(10, txtBuscar);
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            Window_Usuario window_Usuario = new();
            window_Usuario.Closed += new EventHandler(ActualizarTabla);
            window_Usuario.ShowDialog();
        }

        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            MySqlConnection conexion = Conexion.getConexion();

            try
            {
                MySqlCommand comando = new(
                                         "SELECT tabla_usuarios.usuarios_id AS \"ID\"," +
                                         "tabla_usuarios.usuarios_nombre AS \"Nombre\"," +
                                         "tabla_usuarios.usuarios_tipo AS \"Tipo\"," +
                                         "tabla_labs.lab_nombre AS \"Laboratorio\" " +
                                         "FROM bd_prestamos_itspp.tabla_usuarios " +
                                         "JOIN bd_prestamos_itspp.tabla_labs ON tabla_usuarios.lab_id = tabla_labs.lab_id " +
                                         "WHERE tabla_usuarios.usuarios_nombre LIKE @parametro", conexion);

                comando.Parameters.AddWithValue("@parametro", "%" + txtBuscar.Text + "%");

                conexion.Open();

                DataTable dt = new();

                dt.Load(comando.ExecuteReader());

                conexion.Close();

                dg.DataContext = dt;

                if (txtBuscar.Text == "Buscar")
                {
                    ComandosSQL.ActualizaTabla(Comando, dg);
                }

            }
            catch (Exception ex)
            {
                // Manejo de la excepción
                MessageBox.Show("Ocurrió un error al actualizar la tabla " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión en caso de estar abierta
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }


    }
}
