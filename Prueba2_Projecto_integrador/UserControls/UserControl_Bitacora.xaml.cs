using iTextSharp.text;
using iTextSharp.text.pdf;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using Prueba2_Projecto_integrador.Clases;
using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Prueba2_Projecto_integrador
{
    /// <summary>
    /// Interaction logic for UserControl_Bitacora.xaml
    /// </summary>
    public partial class UserControl_Bitacora : UserControl
    {
        public UserControl_Bitacora()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            string comando = "SELECT tabla_bitacora.bitacora_id AS \"ID\", " +
                "tabla_bitacora.bitacora_fecha AS \"Fecha\"," +
                "tabla_bitacora.bitacora_operacion AS \"Operacion\" FROM tabla_bitacora";

            ComandosSQL.ActualizaTabla(comando, dgBitacora);
        }
        private void ExportarDataGridViewAPDF(DataGrid dataGrid)
        {
            try
            {
                // Crear el objeto SaveFileDialog para permitir al usuario seleccionar la ubicación y el nombre del archivo PDF
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Archivo PDF|*.pdf",
                    Title = "Guardar como PDF"
                };

                // Mostrar el cuadro de diálogo y obtener el resultado
                bool? dialogResult = saveFileDialog.ShowDialog();

                // Si el usuario hace clic en "Guardar"
                if (dialogResult == true)
                {
                    // Obtener la ruta de archivo seleccionada por el usuario
                    string rutaArchivo = saveFileDialog.FileName;

                    // Crear el documento PDF
                    using (Document documento = new())
                    {
                        // Crear un objeto PdfWriter para escribir en el archivo
                        PdfWriter.GetInstance(documento, new FileStream(rutaArchivo, FileMode.Create));

                        // Abrir el documento
                        documento.Open();

                        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(Imagenes.ImagenEscuela());
                        image.ScaleAbsolute(150f, 100f);
                        image.SetAbsolutePosition(5f, documento.PageSize.Height - image.ScaledHeight - 5f);
                        documento.Add(image);

                        // Crea la fuente con el tamaño y estilo deseados
                        BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                        BaseFont bf2 = BaseFont.CreateFont("C:\\Windows\\Fonts\\Arial.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

                        Font font = new(bf, 20, Font.BOLD);
                        Font fontText = new(bf2, 15, Font.NORMAL);

                        // Crea y configura el elemento de párrafo para el título
                        Paragraph title = new("Bitacora", font)
                        {
                            Alignment = Element.ALIGN_CENTER,
                            SpacingAfter = 40f // Espacio después del título (ajusta según sea necesario)
                        };
                        documento.Add(title);

                        // Crea y configura el elemento de párrafo para la fecha
                        Paragraph fecha = new("Fecha inicial: " + dpInicial.Text + "   Fecha final: " + dpFinal.Text, fontText)
                        {
                            Alignment = Element.ALIGN_CENTER,
                            SpacingAfter = 10f
                        };
                        documento.Add(fecha);

                        // Crea una tabla en el documento PDF
                        PdfPTable table = new(dataGrid.Columns.Count);

                        // Agrega las columnas de encabezado a la tabla
                        for (int i = 0; i < dataGrid.Columns.Count; i++)
                        {
                            table.AddCell(new Phrase(dataGrid.Columns[i].Header.ToString(), fontText));
                        }

                        // Agrega las filas de datos a la tabla
                        for (int row = 0; row < dataGrid.Items.Count; row++)
                        {
                            for (int column = 0; column < dataGrid.Columns.Count; column++)
                            {
                                if (dataGrid.Columns[column].GetCellContent(dataGrid.Items[row]) is TextBlock cellContent)
                                {
                                    table.AddCell(new Phrase(cellContent.Text, fontText));
                                }
                            }
                        }

                        // Agrega la tabla al documento
                        documento.Add(table);

                        // Cerrar el documento
                        documento.Close();

                        // Abrir el archivo PDF con la aplicación predeterminada del sistema
                        Process.Start(new ProcessStartInfo("cmd", $"/c start {rutaArchivo}") { CreateNoWindow = true });
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que ocurra
                MessageBox.Show("Error al exportar a PDF: " + ex.Message);
            }
        }

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            if (dpInicial.Text != "" && dpFinal.Text != "")
            {
                ExportarDataGridViewAPDF(dgBitacora);
            }
            else
            {
                MessageBox.Show("Por favor, asegúrate de completar todos los campos. Evita dejar espacios en blanco.");
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (dpInicial.Text != "" && dpFinal.Text != "")
            {

                string? fechaInicial = dpInicial.SelectedDate?.ToString("M/dd/yyyy", CultureInfo.InvariantCulture);
                string? fechaFinal = dpFinal.SelectedDate?.ToString("M/dd/yyyy", CultureInfo.InvariantCulture);

                MySqlConnection conexion = Conexion.getConexion();

                MySqlCommand command = new MySqlCommand(
                    "SELECT tabla_bitacora.bitacora_id AS `ID`, " +
                    "tabla_bitacora.bitacora_fecha AS `Fecha`, " +
                    "tabla_bitacora.bitacora_operacion AS `Operacion` " +
                    "FROM tabla_bitacora " +
                    "WHERE tabla_bitacora.bitacora_fecha LIKE CONCAT('%', @FechaInicial, '%') " +
                    "OR tabla_bitacora.bitacora_fecha LIKE CONCAT('%', @FechaFinal, '%');",
                    conexion);

                command.Parameters.AddWithValue("@FechaInicial", fechaInicial);
                command.Parameters.AddWithValue("@FechaFinal", fechaFinal);

                conexion.Open();

                DataTable dt = new();

                dt.Load(command.ExecuteReader());

                conexion.Close();

                dgBitacora.DataContext = dt;
            }
            else
            {
                MessageBox.Show("Por favor, asegúrate de completar todos los campos. Evita dejar espacios en blanco.");
            }


        }

        private void dpInicial_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            dpFinal.IsEnabled = true;
            dpFinal.DisplayDateStart = dpInicial.SelectedDate;
            dpFinal.Text = dpInicial.SelectedDate.ToString();
            if (dpInicial.Text == "")
            {
                dpFinal.IsEnabled = false;
            }
        }

        private void dpInicial_MouseEnter(object sender, MouseEventArgs e)
        {

        }


    }
}
