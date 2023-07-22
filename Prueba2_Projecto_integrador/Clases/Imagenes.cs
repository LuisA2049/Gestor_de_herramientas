using System;
using System.IO;

namespace Prueba2_Projecto_integrador.Clases
{
    public static class Imagenes
    {
        public static string ImagenEscuela()
        {
            string StrCarpetaBase = AppDomain.CurrentDomain.BaseDirectory;
            string ImagenEscuela = Path.Combine(StrCarpetaBase, "Imagenes", "Instituto-Tecnologico-Superior-de-Puerto-Penasco-logo-white.png");

            return ImagenEscuela;
        }

        public static string ImagenEscuelaMenu()
        {
            string StrCarpetaBase = AppDomain.CurrentDomain.BaseDirectory;
            string ImagenEscuela = Path.Combine(StrCarpetaBase, "Imagenes", "Instituto-Tecnologico-Superior-de-Puerto-Penasco-logo.png");

            return ImagenEscuela;
        }

        public static string ImagenUsuario(string usuario)
        {
            string StrCarpetaBase = AppDomain.CurrentDomain.BaseDirectory;
            string ImagenUsuario = Path.Combine(StrCarpetaBase, "Imagenes", "Usuarios", usuario + ".png");

            return ImagenUsuario;
        }

    }
}
