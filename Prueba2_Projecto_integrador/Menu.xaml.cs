using MaterialDesignThemes.Wpf;
using Prueba2_Projecto_integrador.Clases;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace Prueba2_Projecto_integrador
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        private int intClic = 0;
        public Menu()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtUser.Text = Usuario.StrNombreUsuario;

            imgUsuario.Source = new BitmapImage(new Uri(Imagenes.ImagenUsuario(txtUser.Text)));

            imglogo.Source = new BitmapImage(new Uri(Imagenes.ImagenEscuelaMenu()));

        }

        private void MouseClic(Grid grid, Button button, PackIcon packIcon)
        {
            Brush? brushBlanco = new BrushConverter().ConvertFrom("#f6f9f8") as Brush;
            Brush? brushRojo = new BrushConverter().ConvertFrom("#7a2323") as Brush;

            grid.Background = brushBlanco;
            button.Foreground = brushRojo;
            packIcon.Foreground = brushRojo;

            List<(Grid gridBtn, Button button, PackIcon icon)> Lista = new()
            {
                (gridbtnPrestamos, btnPrestamos, iconPrestamo),
                (gridbtnInventario, btnInventario, iconInventario),
                (gridbtnReportes, btnReportes, iconReportes),
                (gridbtnBitacora, btnBitacora, iconBitacora),
                (gridbtnUsuario, btnUsuario, iconUsuario),
                (gridbtn3d, btn3d, icon3d)
            };

            if (grid.Background == brushBlanco && button.Foreground == brushRojo && packIcon.Foreground == brushRojo)
            {
                for (int i = 0; i < Lista.Count; i++)
                {
                    var primerElemento = Lista[i];

                    Grid GridBtn = primerElemento.gridBtn;
                    Button Button = primerElemento.button;
                    PackIcon Icon = primerElemento.icon;

                    if (GridBtn.Name != grid.Name)
                    {
                        GridBtn.Background = Brushes.Transparent;
                        Button.Foreground = brushBlanco;
                        Icon.Foreground = brushBlanco;
                    }
                }
            }

        }
        private static void MouseArriba(Grid grid, Button button, PackIcon packIcon)
        {
            Brush? brushBlanco = new BrushConverter().ConvertFrom("#f6f9f8") as Brush;
            Brush? brushRojo = new BrushConverter().ConvertFrom("#7a2323") as Brush;

            grid.Background = brushBlanco;
            button.Foreground = brushRojo;
            packIcon.Foreground = brushRojo;
        }
        private static void MouseAfuera(Grid grid, Button button, PackIcon packIcon)
        {
            Brush? brushBlanco = new BrushConverter().ConvertFrom("#f6f9f8") as Brush;

            grid.Background = Brushes.Transparent;
            button.Foreground = brushBlanco;
            packIcon.Foreground = brushBlanco;
        }
        private void Button_Click_Inventario(object sender, RoutedEventArgs e)
        {
            intClic = 2;

            CC.Content = new UserControl_Inventario();

            MouseClic(gridbtnInventario, btnInventario, iconInventario);
        }

        private void btnReportes_Click(object sender, RoutedEventArgs e)
        {
            intClic = 3;
            CC.Content = new UserControl_Reportes();

            MouseClic(gridbtnReportes, btnReportes, iconReportes);
        }

        private void btnPrestamos_Click(object sender, RoutedEventArgs e)
        {
            intClic = 1;
            CC.Content = new UserControl_Prestamo();

            MouseClic(gridbtnPrestamos, btnPrestamos, iconPrestamo);
        }

        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            Inicio_sesion inicio_Sesion = new();
            inicio_Sesion.Show();
            this.Close();
        }

        private void btn3d_Click(object sender, RoutedEventArgs e)
        {
            intClic = 6;
            CC.Content = new UserControl_3D();

            MouseClic(gridbtn3d, btn3d, icon3d);
        }

        private void btnBitacora_Click(object sender, RoutedEventArgs e)
        {
            intClic = 4;
            CC.Content = new UserControl_Bitacora();

            MouseClic(gridbtnBitacora, btnBitacora, iconBitacora);

        }

        private void btnUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (Usuario.StrTpoUsuario == "Admin")
            {
                intClic = 5;
                CC.Content = new UserControl_Usuario();

                MouseClic(gridbtnUsuario, btnUsuario, iconUsuario);
            }
            else
            {
                MessageBox.Show("Lamentablemente, no dispones de los permisos adecuados en este momento. :(", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void gridbtnPrestamos_MouseEnter(object sender, MouseEventArgs e)
        {
            MouseArriba(gridbtnPrestamos, btnPrestamos, iconPrestamo);
        }

        private void gridbtnPrestamos_MouseLeave(object sender, MouseEventArgs e)
        {
            if (intClic != 1)
            {
                MouseAfuera(gridbtnPrestamos, btnPrestamos, iconPrestamo);
            }
        }

        private void gridbtnInventario_MouseEnter(object sender, MouseEventArgs e)
        {
            MouseArriba(gridbtnInventario, btnInventario, iconInventario);
        }

        private void gridbtnInventario_MouseLeave(object sender, MouseEventArgs e)
        {
            if (intClic != 2)
            {
                MouseAfuera(gridbtnInventario, btnInventario, iconInventario);
            }
        }

        private void gridbtnReportes_MouseEnter(object sender, MouseEventArgs e)
        {
            MouseArriba(gridbtnReportes, btnReportes, iconReportes);
        }

        private void gridbtnReportes_MouseLeave(object sender, MouseEventArgs e)
        {
            if (intClic != 3)
            {
                MouseAfuera(gridbtnReportes, btnReportes, iconReportes);
            }
        }

        private void gridbtnBitacora_MouseEnter(object sender, MouseEventArgs e)
        {
            MouseArriba(gridbtnBitacora, btnBitacora, iconBitacora);
        }

        private void gridbtnBitacora_MouseLeave(object sender, MouseEventArgs e)
        {
            if (intClic != 4)
            {
                MouseAfuera(gridbtnBitacora, btnBitacora, iconBitacora);
            }
        }

        private void gridbtnUsuario_MouseEnter(object sender, MouseEventArgs e)
        {
            MouseArriba(gridbtnUsuario, btnUsuario, iconUsuario);
        }

        private void gridbtnUsuario_MouseLeave(object sender, MouseEventArgs e)
        {
            if (intClic != 5)
            {
                MouseAfuera(gridbtnUsuario, btnUsuario, iconUsuario);
            }
        }

        private void gridbtn3d_MouseEnter(object sender, MouseEventArgs e)
        {
            MouseArriba(gridbtn3d, btn3d, icon3d);
        }

        private void gridbtn3d_MouseLeave(object sender, MouseEventArgs e)
        {
            if (intClic != 6)
            {
                MouseAfuera(gridbtn3d, btn3d, icon3d);
            }
        }

        private void gridbtnCerrarSesion_MouseEnter(object sender, MouseEventArgs e)
        {
            MouseArriba(gridbtnCerrarSesion, btnCerrarSesion, iconCerrarSesion);
        }

        private void gridbtnCerrarSesion_MouseLeave(object sender, MouseEventArgs e)
        {
            if (intClic != 7)
            {
                MouseAfuera(gridbtnCerrarSesion, btnCerrarSesion, iconCerrarSesion);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string comando = "INSERT INTO `bd_prestamos_itspp`.`tabla_bitacora` (`bitacora_fecha`, `bitacora_operacion`) VALUES ('Fecha: " + DateTime.Now.ToString() + "', 'Cerro sesión el usuario: " + Usuario.StrNombreUsuario + "');";
            ComandosSQL.ComandoNonquery(comando);
        }
    }
}
