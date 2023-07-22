namespace Prueba2_Projecto_integrador.Clases
{
    public static class Usuario
    {
        private static string strNombreUsuario = "";
        private static string strTpoUsuario = "";
        private static int intLabUsuario = 0;
        private static int intIdUsuario = 0;
        public static string StrNombreUsuario { get => strNombreUsuario; set => strNombreUsuario = value; }
        public static string StrTpoUsuario { get => strTpoUsuario; set => strTpoUsuario = value; }
        public static int IntIdUsuario { get => intIdUsuario; set => intIdUsuario = value; }
        public static int IntLabUsuario { get => intLabUsuario; set => intLabUsuario = value; }
    }
}
