using MaterialDesignThemes.Wpf;
using System.Windows.Controls;

namespace Prueba2_Projecto_integrador
{
    /// <summary>
    /// Interaction logic for UserControl_Reportes.xaml
    /// </summary>
    public partial class UserControl_Reportes : UserControl
    {
        public UserControl_Reportes()
        {
            InitializeComponent();
        }

        private static void AgrandarImagen(PackIcon packIcon)
        {
            packIcon.Height += 2;
            packIcon.Width += 2;
        }

        private static void NormalImagen(PackIcon packIcon)
        {
            packIcon.Height -= 2;
            packIcon.Width -= 2;
        }

        private void iconPrestamo_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            AgrandarImagen(iconPrestamo);
        }

        private void iconPrestamo_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            NormalImagen(iconPrestamo);
        }

        private void iconInventario_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            AgrandarImagen(iconInventario);
        }

        private void iconInventario_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            NormalImagen(iconInventario);
        }

        private void iconUsuario_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            AgrandarImagen(iconUsuario);
        }

        private void iconUsuario_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            NormalImagen(iconUsuario);
        }

        private void iconPrestamo_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Window_rptPrestamo window_RptPrestamo = new();
            window_RptPrestamo.ShowDialog();
        }

        private void iconInventario_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Window_rptInventario window_RptInventario = new();
            window_RptInventario.ShowDialog();
        }

        private void iconUsuario_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Window_rptUsuario window_RptUsuario = new();
            window_RptUsuario.ShowDialog();
        }
    }
}
