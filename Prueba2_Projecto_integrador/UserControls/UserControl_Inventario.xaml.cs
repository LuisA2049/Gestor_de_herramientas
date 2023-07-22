using MySql.Data.MySqlClient;
using Prueba2_Projecto_integrador.Clases;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Prueba2_Projecto_integrador
{
    /// <summary>
    /// Interaction logic for UserControl_Inventario.xaml
    /// </summary>
    public partial class UserControl_Inventario : UserControl
    {
        private static int intInventarioEditar;

        private readonly string comandoActulizarTabla = @"SELECT objeto_id as ""ID"",
	                           objeto_nombre as ""Nombre"",
                               objeto_descripcion as ""Descripción"",
                               objeto_cantidad as ""Cantidad"",
                               objeto_disponible as ""Disponible"",
                               objeto_estado as ""Estado""
                               FROM bd_prestamos_itspp.tabla_inventario
                               JOIN bd_prestamos_itspp.tabla_labs ON tabla_inventario.lab_id ='" + Usuario.IntLabUsuario + "';";

        private readonly string cmdActulizarTablaAdmin = @"SELECT objeto_id AS ""ID"",
                                   objeto_nombre AS ""Nombre"",
                                   objeto_descripcion AS ""Descripción"",
                                   objeto_cantidad AS ""Cantidad"",
                                   objeto_disponible AS ""Disponible"",
                                   objeto_estado AS ""Estado""
                                   FROM bd_prestamos_itspp.tabla_inventario";

        public UserControl_Inventario()
        {
            InitializeComponent();

        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Usuario.IntLabUsuario == 9)
            {
                ComandosSQL.ActualizaTabla(cmdActulizarTablaAdmin, dgInventario);
            }
            else
            {
                ComandosSQL.ActualizaTabla(comandoActulizarTabla, dgInventario);
            }
        }
        private void btnAgregar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Window_Inventario window_Inventario = new();
            window_Inventario.Closed += new EventHandler(ActualizarTabla);
            window_Inventario.ShowDialog();
        }

        private void txtBuscar_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            Aparencias.txtGotFocus(10, txtBuscar);
        }

        private void txtBuscar_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            Aparencias.txtLostFocus(10, txtBuscar);
        }

        private void dg_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;
            DataRowView row_selected = dataGrid.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                intInventarioEditar = int.Parse(row_selected["ID"].ToString());
            }
        }

        private void btnEditar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Window_Inventario window_Inventario = new(intInventarioEditar);
            window_Inventario.Closed += new EventHandler(ActualizarTabla);
            Variables.BoolEditar1 = true;
            window_Inventario.ShowDialog();
        }
        private void ActualizarTabla(object? sender, EventArgs e)
        {
            if (Usuario.IntLabUsuario == 9)
            {
                ComandosSQL.ActualizaTabla(cmdActulizarTablaAdmin, dgInventario);
            }
            else
            {
                ComandosSQL.ActualizaTabla(comandoActulizarTabla, dgInventario);
            }
        }

        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            MySqlConnection conexion = Conexion.getConexion();
            try
            {
                conexion.Open();

                string consultaSql = @"SELECT objeto_id AS ""ID"",
                                       objeto_nombre AS ""Nombre"",
                                       objeto_descripcion AS ""Descripción"",
                                       objeto_cantidad AS ""Cantidad"",
                                       objeto_disponible AS ""Disponible"",
                                       objeto_estado AS ""Estado""
                                       FROM bd_prestamos_itspp.tabla_inventario 
                                       JOIN bd_prestamos_itspp.tabla_labs ON tabla_inventario.lab_id = @LabID 
                                       WHERE tabla_inventario.objeto_nombre LIKE @parametro";

                MySqlCommand comando = new(consultaSql, conexion);

                comando.Parameters.AddWithValue("@LabID", Usuario.IntLabUsuario);

                if (Usuario.IntLabUsuario == 9)
                {
                    consultaSql = "SELECT tabla_inventario.objeto_id as \"ID\"," +
                           "tabla_inventario.objeto_nombre as \"Nombre\"," +
                           "tabla_inventario.objeto_descripcion as \"Descripción\"," +
                           "tabla_inventario.objeto_cantidad as \"Cantidad\"," +
                           "tabla_inventario.objeto_disponible as \"Disponible\"," +
                           "tabla_inventario.objeto_estado as \"Estado\" " +
                           "FROM bd_prestamos_itspp.tabla_inventario " +
                           "WHERE tabla_inventario.objeto_nombre LIKE @parametro";

                    comando = new(consultaSql, conexion);

                }

                comando.Parameters.AddWithValue("@parametro", "%" + txtBuscar.Text + "%");

                DataTable dt = new();

                dt.Load(comando.ExecuteReader());

                dgInventario.DataContext = dt;

                conexion.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al actualizar la tabla " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }


        }


    }
}
