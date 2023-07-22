using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Prueba2_Projecto_integrador.Clases
{
    public class ComandosSQL
    {
        public static void ComandoNonquery(string Strcomando)
        {
            try
            {
                MySqlConnection conexion = Conexion.getConexion();

                MySqlCommand comando = new(Strcomando, conexion);

                conexion.Open();

                comando.ExecuteNonQuery();

                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static int ComandoObtenerInt(string Strcomando, int IntDato)
        {
            try
            {
                MySqlConnection conexion = Conexion.getConexion();

                MySqlCommand comando = new(Strcomando, conexion);

                conexion.Open();

                MySqlDataReader reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        IntDato = reader.GetInt32(0);
                    }
                }
                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return IntDato;
        }
        public static string ComandoObtenerString(string Strcomando, string StrDato)
        {
            try
            {
                MySqlConnection conexion = Conexion.getConexion();

                MySqlCommand comando = new(Strcomando, conexion);

                conexion.Open();

                MySqlDataReader reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        StrDato = reader.GetString(0);
                    }
                }

                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return StrDato;
        }
        public static Double ComandoObtenerDouble(string Strcomando, double DoubleDato)
        {
            try
            {
                MySqlConnection conexion = Conexion.getConexion();

                MySqlCommand comando = new(Strcomando, conexion);

                conexion.Open();

                MySqlDataReader reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DoubleDato = reader.GetDouble(0);
                    }
                }
                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return DoubleDato;
        }
        public static bool ComandoExiste(string Strcomando, bool si)
        {
            try
            {
                MySqlConnection conexion = Conexion.getConexion();

                MySqlCommand comando = new(Strcomando, conexion);

                conexion.Open();

                MySqlDataReader reader = comando.ExecuteReader();

                if (reader.HasRows)
                {

                    si = true;

                }

                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return si;

        }
        public static void ActualizaTabla(string Strcomando, DataGrid dataGrid)
        {
            try
            {
                MySqlConnection conexion = Conexion.getConexion();

                MySqlCommand comando = new(Strcomando, conexion);

                conexion.Open();

                DataTable dt = new();

                dt.Load(comando.ExecuteReader());

                conexion.Close();

                dataGrid.DataContext = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void CargarComboBox(string StrComando, ComboBox comboBox, string StrMemberPath, string StrValuePath)
        {
            try
            {
                MySqlConnection connection = Conexion.getConexion();

                MySqlDataAdapter adapter = new(StrComando, connection);

                connection.Open();

                DataTable dt = new();

                adapter.Fill(dt);

                comboBox.ItemsSource = dt.DefaultView;

                comboBox.DisplayMemberPath = StrMemberPath;

                comboBox.SelectedValuePath = StrValuePath;

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
