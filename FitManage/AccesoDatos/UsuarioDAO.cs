using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class UsuarioDAO
    {
        string connectionString = "server=localhost;port=8000;database=fitmanage;user=root;password=root"; // ajusta según tu config

        private void AgregarUsuario(string usuario, string contraseña)
        {

            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();

                    string query = "INSERT INTO log_in (Usuario, Contracena) VALUES (@usuario, @contracena)";
                    MySqlCommand comando = new MySqlCommand(query, conexion);
                    comando.Parameters.AddWithValue("@usuario", usuario);
                    comando.Parameters.AddWithValue("@contracena", contraseña);

                    comando.ExecuteNonQuery();
                    
                }
                catch (Exception ex)
                {
                    
                }
            }
        }
        public bool VerificarUsuario(string usuario, string contraseña)
        {

            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();

                    string query = "SELECT COUNT(*) FROM fitmanage.log_in WHERE Usuario = @usuario AND Contracena = @contracena";
                    MySqlCommand comando = new MySqlCommand(query, conexion);
                    comando.Parameters.AddWithValue("@usuario", usuario);
                    comando.Parameters.AddWithValue("@contracena", contraseña);

                    int count = Convert.ToInt32(comando.ExecuteScalar());

                    return count > 0;
                }
                catch (Exception ex)
                
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
    }
}
