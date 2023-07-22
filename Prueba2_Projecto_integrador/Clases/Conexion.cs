using MySql.Data.MySqlClient;

namespace Prueba2_Projecto_integrador.Clases
{
    class Conexion
    {
        public static MySqlConnection getConexion()
        {
            string servidor = "localhost";
            string puerto = "3306";
            string usuario = "root";
            string password = "itspp";
            string bd = "bd_prestamos_itspp";

            string cadenaConexion = "server=" + servidor + "; port=" + puerto + "; user id=" + usuario + "; password=" + password + "; database=" + bd;
            MySqlConnection conexion = new(cadenaConexion);

            return conexion;
        }
    }
}
