using HelixToolkit.Wpf;
using Microsoft.Win32;
using Prueba2_Projecto_integrador.Clases;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace Prueba2_Projecto_integrador
{
    /// <summary>
    /// Interaction logic for UserControl_3D.xaml
    /// </summary>
    public partial class UserControl_3D : UserControl
    {


        public UserControl_3D()
        {
            InitializeComponent();
        }

        private Model3D Display3d(string model)
        {
            Model3D device = null;
            try
            {
                //Adding a gesture here
                viewPort3d.RotateGesture = new MouseGesture(MouseAction.LeftClick);

                //Import 3D model file
                ModelImporter import = new();

                //Load the 3D model file
                device = import.Load(model);
            }
            catch (Exception e)
            {
                // Handle exception in case can not file 3D model
                MessageBox.Show("Exception Error : " + e.StackTrace);
            }

            return device;
        }

        public void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            string StrCarpetaBase = AppDomain.CurrentDomain.BaseDirectory;

            string ruta = Path.Combine(StrCarpetaBase, "Modelos3D");

            string fileName;
            var myDialog = new OpenFileDialog
            {
                InitialDirectory = ruta
            };

            try
            {
                myDialog.Filter = "SE Model File|*.par;*.psm;*.asm;*.stl;*.obj";

                if ((bool)myDialog.ShowDialog())
                {

                    Mouse.OverrideCursor = Cursors.Wait;

                    fileName = myDialog.FileName;

                    Model3D newModel3D;

                    var newMOdelImporter = new ModelImporter();

                    var fileInf = new FileInfo(fileName);

                    string extn = fileInf.Extension;

                    if (extn == ".stl" | extn == ".STL")
                    {

                        newModel3D = newMOdelImporter.Load(fileName);
                    }
                    else
                    {


                    }

                    // Now launch the model in Viewport.
                    var device3D = new ModelVisual3D();

                    var lights = new DefaultLights();

                    device3D.Content = Display3d(fileName);

                    this.viewPort3d.RotateGesture = new MouseGesture(MouseAction.LeftClick);

                    this.viewPort3d.Children.Clear();
                    this.viewPort3d.Children.Add(device3D);
                    this.viewPort3d.Children.Add(lights);
                    this.viewPort3d.ShowCoordinateSystem = true;
                    this.viewPort3d.ZoomAroundMouseDownPoint = true;
                    this.viewPort3d.ZoomExtentsWhenLoaded = true;
                    this.viewPort3d.ResetCamera();

                    // viewPort3d.ShowTriangleCountInfo = True

                    Mouse.OverrideCursor = Cursors.Arrow;
                }

            }

            catch (Exception ex)
            {
                Mouse.OverrideCursor = Cursors.Arrow;
                MessageBox.Show(ex.Message);
            }

        }

        private void btnAñadir_Click(object sender, RoutedEventArgs e)
        {

            if (Usuario.StrTpoUsuario == "Admin")
            {
                string StrCarpetaBase = AppDomain.CurrentDomain.BaseDirectory;
                string ruta = Path.Combine(StrCarpetaBase, "Modelos3D");


                System.Diagnostics.Process.Start("explorer.exe", ruta);
            }
            else
            {
                MessageBox.Show("Lamentablemente, no dispones de los permisos adecuados en este momento. :(", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
    }
}
