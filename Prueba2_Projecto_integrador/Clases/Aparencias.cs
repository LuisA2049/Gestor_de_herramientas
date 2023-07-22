using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Prueba2_Projecto_integrador.Clases
{
    public class Aparencias
    {
        public static void txtLostFocus(int IntCualTxt, TextBox textBox)
        {
            switch (IntCualTxt)
            {
                case 1:

                    if (textBox.Text == null || textBox.Text.StartsWith(" ") || textBox.Text == "")
                    {
                        textBox.Text = "Nombre";
                    }

                    break;

                case 2:

                    if (textBox.Text == null || textBox.Text.StartsWith(" ") || textBox.Text == "")
                    {
                        textBox.Text = "Apellido";
                    }

                    break;

                case 3:

                    if (textBox.Text == null || textBox.Text.StartsWith(" ") || textBox.Text == "")
                    {
                        textBox.Text = "Número de control";
                    }

                    break;

                case 6:

                    if (textBox.Text == null || textBox.Text.StartsWith(" ") || textBox.Text == "")
                    {
                        textBox.Text = "Correo electrónico";
                    }

                    break;

                case 7:

                    if (textBox.Text == null || textBox.Text.StartsWith(" ") || textBox.Text == "")
                    {
                        textBox.Text = "Número telefónico";
                    }

                    break;

                case 8:

                    if (textBox.Text == null || textBox.Text.StartsWith(" ") || textBox.Text == "")
                    {
                        textBox.Text = "Usuario";
                    }

                    break;

                case 10:

                    if (textBox.Text == null || textBox.Text.StartsWith(" ") || textBox.Text == "")
                    {
                        textBox.Text = "Buscar";
                    }

                    break;

                case 11:

                    if (textBox.Text == null || textBox.Text.StartsWith(" ") || textBox.Text == "")
                    {
                        textBox.Text = "Descripción";
                    }

                    break;

                case 12:

                    if (textBox.Text == null || textBox.Text.StartsWith(" ") || textBox.Text == "")
                    {
                        textBox.Text = "Cantidad";
                    }

                    break;

                case 13:

                    if (textBox.Text == null || textBox.Text.StartsWith(" ") || textBox.Text == "")
                    {
                        textBox.Text = "Comentario";
                    }

                    break;

                case 14:

                    if (textBox.Text == null || textBox.Text.StartsWith(" ") || textBox.Text == "")
                    {
                        textBox.Text = "Laboratorio";
                    }

                    break;
            }

        }

        public static void txtGotFocus(int IntCualTxt, TextBox textBox)
        {
            switch (IntCualTxt)
            {
                case 1:

                    if (textBox.Text == "Nombre")
                    {
                        textBox.Text = null;
                    }

                    break;

                case 2:

                    if (textBox.Text == "Apellido")
                    {
                        textBox.Text = null;
                    }

                    break;

                case 3:

                    if (textBox.Text == "Número de control")
                    {
                        textBox.Text = null;
                    }

                    break;

                case 6:

                    if (textBox.Text == "Correo electrónico")
                    {
                        textBox.Text = null;
                    }

                    break;

                case 7:

                    if (textBox.Text == "Número telefónico")
                    {
                        textBox.Text = null;
                    }

                    break;

                case 10:

                    if (textBox.Text == "Buscar")
                    {
                        textBox.Text = null;
                    }

                    break;

                case 11:

                    if (textBox.Text == "Descripción")
                    {
                        textBox.Text = null;
                    }

                    break;

                case 12:

                    if (textBox.Text == "Cantidad")
                    {
                        textBox.Text = null;
                    }

                    break;

                case 13:

                    if (textBox.Text == "Comentario")
                    {
                        textBox.Text = null;
                    }

                    break;


            }

        }

        public static void cbGotFocus(int IntCualCb, ComboBox comboBox)
        {
            switch (IntCualCb)
            {
                case 1:

                    if (comboBox.Text == "Carrera")
                    {
                        comboBox.Text = null;
                    }

                    break;

                case 2:

                    if (comboBox.Text == "Semestre")
                    {
                        comboBox.Text = null;
                    }

                    break;

                case 3:

                    if (comboBox.Text == "Buscar")
                    {
                        comboBox.Text = null;
                    }

                    break;

                case 4:

                    if (comboBox.Text == "Laboratorio")
                    {
                        comboBox.Text = null;
                    }

                    break;
            }
        }

        public static void SoloLetras(System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new(@"^[a-zA-ZñÑáéíóúÁÉÍÓÚ]+$");

            if (!regex.IsMatch(e.Text))
            {
                e.Handled = true;
                MessageBox.Show("Solo se permiten letras.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public static void SoloNumeros(System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!IsNumericInput(e.Text) || e.Text == " ")
            {
                MessageBox.Show("Solo se permiten números.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);

                e.Handled = true;
            }
        }
        private static bool IsNumericInput(string text)
        {
            return Regex.IsMatch(text, @"^[0-9]+$");
        }
        public static void limitarTexto(object sender, int intMaximo)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Text.Length > intMaximo)
            {
                textBox.Text = textBox.Text.Substring(0, intMaximo);
                textBox.CaretIndex = intMaximo;
            }
        }

    }
}
