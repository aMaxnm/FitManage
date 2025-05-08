using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Entidad;


namespace AccesoDatos
{
    public class MiembroDAO
    {
        private string connectionString = "server=localhost;user=root;password=root;database=fitmanage";

        //  Obtener todos los miembros
        public List<Miembro> ObtenerMiembros()
        {
            List<Miembro> listaMiembros = new List<Miembro>();

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Miembro";

                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        /*listaMiembros.Add(new Miembro
                        {
                            IdMiembro = reader.GetInt32("idMiembro"),
                            IdMembresia = reader.GetInt32("idMembresia"),
                            Nombres = reader.GetString("nombres"),
                            ApellidoPaterno = reader.GetString("apellidoPaterno"),
                            ApellidoMaterno = reader.GetString("apellidoMaterno"),
                            FechaNacimiento = reader.GetDateTime("fechaNacimiento"),
                            NumeroTelefono = reader.GetString("numeroTelefono"),
                            FechaRegistro = reader.GetDateTime("fechaRegistro"),
                            FechaInicio = reader.GetDateTime("fechaInicio"),
                            FechaFin = reader.GetDateTime("fechaFin"),
                            Fotografia = reader["fotografia"] as byte[]  // Para manejar imágenes
                        });*/
                    }
                }
            }
            return listaMiembros;
        }

        // 🔹 Agregar un nuevo miembro
        public void AgregarMiembro(Miembro miembro)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Miembro (idMembresia, nombres, apellidoPaterno, apellidoMaterno, fechaNacimiento, numeroTelefono, fechaRegistro, fechaInicio, fechaFin, fotografia) VALUES (@idMembresia, @nombres, @apellidoPaterno, @apellidoMaterno, @fechaNacimiento, @numeroTelefono, @fechaRegistro, @fechaInicio, @fechaFin, @fotografia)";

                using (var command = new MySqlCommand(query, connection))
                {
                  /*  command.Parameters.AddWithValue("@idMembresia", miembro.IdMembresia);
                    command.Parameters.AddWithValue("@nombres", miembro.Nombres);
                    command.Parameters.AddWithValue("@apellidoPaterno", miembro.ApellidoPaterno);
                    command.Parameters.AddWithValue("@apellidoMaterno", miembro.ApellidoMaterno);
                    command.Parameters.AddWithValue("@fechaNacimiento", miembro.FechaNacimiento);
                    command.Parameters.AddWithValue("@numeroTelefono", miembro.NumeroTelefono);
                    command.Parameters.AddWithValue("@fechaRegistro", miembro.FechaRegistro);
                    command.Parameters.AddWithValue("@fechaInicio", miembro.FechaInicio);
                    command.Parameters.AddWithValue("@fechaFin", miembro.FechaFin);
                    command.Parameters.AddWithValue("@fotografia", miembro.Fotografia);

                    command.ExecuteNonQuery();*/
                }
            }
        }

        // 🔹 Actualizar información de un miembro
        public void ModificarMiembro(Miembro miembro)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Miembro SET idMembresia = @idMembresia, nombres = @nombres, apellidoPaterno = @apellidoPaterno, apellidoMaterno = @apellidoMaterno, fechaNacimiento = @fechaNacimiento, numeroTelefono = @numeroTelefono, fechaRegistro = @fechaRegistro, fechaInicio = @fechaInicio, fechaFin = @fechaFin, fotografia = @fotografia WHERE idMiembro = @idMiembro";

                using (var command = new MySqlCommand(query, connection))
                {
                   /* command.Parameters.AddWithValue("@idMiembro", miembro.IdMiembro);
                    command.Parameters.AddWithValue("@idMembresia", miembro.IdMembresia);
                    command.Parameters.AddWithValue("@nombres", miembro.Nombres);
                    command.Parameters.AddWithValue("@apellidoPaterno", miembro.ApellidoPaterno);
                    command.Parameters.AddWithValue("@apellidoMaterno", miembro.ApellidoMaterno);
                    command.Parameters.AddWithValue("@fechaNacimiento", miembro.FechaNacimiento);
                    command.Parameters.AddWithValue("@numeroTelefono", miembro.NumeroTelefono);
                    command.Parameters.AddWithValue("@fechaRegistro", miembro.FechaRegistro);
                    command.Parameters.AddWithValue("@fechaInicio", miembro.FechaInicio);
                    command.Parameters.AddWithValue("@fechaFin", miembro.FechaFin);
                    command.Parameters.AddWithValue("@fotografia", miembro.Fotografia);

                    command.ExecuteNonQuery();*/
                }
            }
        }

        // 🔹 Eliminar un miembro
        public void EliminarMiembro(int idMiembro)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Miembro WHERE idMiembro = @idMiembro";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idMiembro", idMiembro);
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
