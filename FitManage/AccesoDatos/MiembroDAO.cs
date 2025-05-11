using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Entidad;

namespace AccesoDatos
{
    public class MiembroDAO
    {
        private string connectionString = "server=localhost;port=8000;user=root;password=root;database=fitmanage;";

        // Obtener todos los miembros
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
                        listaMiembros.Add(new Miembro
                        {
                            IdMiembro = reader.GetInt32("Id_miembro"),
                            IdMembresia = reader.GetInt32("Id_membresia"),
                            Nombres = reader.GetString("Nombre"),
                            ApellidoPaterno = reader.GetString("Ap_paterno"),
                            ApellidoMaterno = reader.GetString("Ap_materno"),
                            FechaNacimiento = reader.GetDateTime("Fecha_nacimiento"),
                            NumeroTelefono = reader.GetString("Num_celular"),
                            FechaRegistro = reader.GetDateTime("FechaRegistro"),
                            FechaVencimiento = reader.GetDateTime("Fecha_Vencimiento"),
                            Fotografia = reader["Foto"] as byte[]
                        });
                    }
                }
            }
            return listaMiembros;
        }

        // Agregar un nuevo miembro
        public void AgregarMiembro(Miembro miembro)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO Miembro 
                (Id_membresia, Nombre, Ap_paterno, Ap_materno, Fecha_nacimiento, Num_celular, FechaRegistro, Fecha_Vencimiento, Foto) 
                VALUES 
                (@Id_membresia, @Nombre, @Ap_paterno, @Ap_materno, @Fecha_nacimiento, @Num_celular, @FechaRegistro, @Fecha_Vencimiento, @Foto)";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id_membresia", miembro.IdMembresia);
                    command.Parameters.AddWithValue("@Nombre", miembro.Nombres);
                    command.Parameters.AddWithValue("@Ap_paterno", miembro.ApellidoPaterno);
                    command.Parameters.AddWithValue("@Ap_materno", miembro.ApellidoMaterno);
                    command.Parameters.AddWithValue("@Fecha_nacimiento", miembro.FechaNacimiento);
                    command.Parameters.AddWithValue("@Num_celular", miembro.NumeroTelefono);
                    command.Parameters.AddWithValue("@FechaRegistro", miembro.FechaRegistro);
                    command.Parameters.AddWithValue("@Fecha_Vencimiento", miembro.FechaVencimiento);
                    command.Parameters.AddWithValue("@Foto", miembro.Fotografia);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Modificar un miembro
        public void ModificarMiembro(Miembro miembro)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = @"UPDATE Miembro SET 
                    Id_membresia = @Id_membresia,
                    Nombre = @Nombre,
                    Ap_paterno = @Ap_paterno,
                    Ap_materno = @Ap_materno,
                    Fecha_nacimiento = @Fecha_nacimiento,
                    Num_celular = @Num_celular,
                    FechaRegistro = @FechaRegistro,
                    Fecha_Vencimiento = @Fecha_Vencimiento,
                    Foto = @Foto
                    WHERE Id_miembro = @Id_miembro";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id_miembro", miembro.IdMiembro);
                    command.Parameters.AddWithValue("@Id_membresia", miembro.IdMembresia);
                    command.Parameters.AddWithValue("@Nombre", miembro.Nombres);
                    command.Parameters.AddWithValue("@Ap_paterno", miembro.ApellidoPaterno);
                    command.Parameters.AddWithValue("@Ap_materno", miembro.ApellidoMaterno);
                    command.Parameters.AddWithValue("@Fecha_nacimiento", miembro.FechaNacimiento);
                    command.Parameters.AddWithValue("@Num_celular", miembro.NumeroTelefono);
                    command.Parameters.AddWithValue("@FechaRegistro", miembro.FechaRegistro);
                    command.Parameters.AddWithValue("@Fecha_Vencimiento", miembro.FechaVencimiento);
                    command.Parameters.AddWithValue("@Foto", miembro.Fotografia);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Eliminar un miembro
        public void EliminarMiembro(int idMiembro)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Miembro WHERE Id_miembro = @Id_miembro";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id_miembro", idMiembro);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
