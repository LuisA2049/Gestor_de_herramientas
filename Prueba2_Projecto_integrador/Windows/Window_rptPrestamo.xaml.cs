using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using Prueba2_Projecto_integrador.Clases;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Prueba2_Projecto_integrador
{
    /// <summary>
    /// Interaction logic for Window_rptPrestamo.xaml
    /// </summary>
    public partial class Window_rptPrestamo : Window
    {
        public Window_rptPrestamo()
        {
            InitializeComponent();
        }
        private void ExportarDataGridAPDF(DataGrid dataGrid)
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
                    using Document documento = new(PageSize.A4.Rotate());
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
                    Font fontTextCell = new(bf2, 10, Font.NORMAL);

                    // Crea y configura el elemento de párrafo para el título
                    Paragraph title = new("Prestamos", font)
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
                    PdfPTable table = new PdfPTable(dataGrid.Columns.Count);

                    // Agrega las columnas de encabezado a la tabla
                    for (int i = 0; i < dataGrid.Columns.Count; i++)
                    {
                        table.AddCell(new Phrase(dataGrid.Columns[i].Header.ToString(), fontTextCell));
                    }

                    // Agrega las filas de datos a la tabla
                    for (int row = 0; row < dataGrid.Items.Count; row++)
                    {
                        for (int column = 0; column < dataGrid.Columns.Count; column++)
                        {
                            if (dataGrid.Columns[column].GetCellContent(dataGrid.Items[row]) is TextBlock cellContent)
                            {
                                table.AddCell(new Phrase(cellContent.Text, fontTextCell));
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
                string comando = "SELECT tabla_prestamos.prestamos_id AS 'ID', " +
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

                ComandosSQL.ActualizaTabla(comando, dg);

                ExportarDataGridAPDF(dg);

                this.Close();

            }
            else
            {
                MessageBox.Show("No dejar espacios en blanco.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);

            }


        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dpInicial_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            txtFinal.Visibility = Visibility.Collapsed;
            dpFinal.IsEnabled = true;
            dpFinal.DisplayDateStart = dpInicial.SelectedDate;
            dpFinal.Text = dpInicial.SelectedDate.ToString();
            if (dpInicial.Text == "")
            {
                txtFinal.Visibility = Visibility.Visible;
                dpFinal.IsEnabled = false;
            }
        }

        private void dpInicial_GotFocus(object sender, RoutedEventArgs e)
        {
            txtInicial.Visibility = Visibility.Collapsed;
        }
    }
}
