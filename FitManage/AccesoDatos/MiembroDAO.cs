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
                string query = "SELECT * FROM fitmanage.miembro";

                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listaMiembros.Add(new Miembro
                        {
                            IdMiembro = reader.GetInt32("Id_miembro"),
                            IdMembresia = reader.GetInt32("Id_membresia"),
                            Nombre = reader.GetString("Nombre"),
                            ApellidoPaterno = reader.GetString("Ap_paterno"),
                            ApellidoMaterno = reader.GetString("Ap_materno"),
                            FechaNacimiento = reader.GetDateTime("Fecha_nacimiento"),
                            NumeroCelular = reader.GetString("Num_celular"),
                            FechaRegistro = reader.GetDateTime("FechaReggistro"),
                            FechaVencimiento = reader.GetDateTime("Fecha_vencimiento"),
                            Foto = reader["Foto"] as byte[]  // Para manejar imágenes
                        });
                    }
                }
            }
            return listaMiembros;
        }

        // 🔹 Agregar un nuevo miembro
        public void AgregarMiembro(Miembro miembro)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO fitmanage.miembro (Id_miembro, Id_membresia, Nombre, Ap_paterno, Ap_materno, Fecha_nacimiento, Num_celular, FechaRegistro, Fecha_vencimiento, Foto) VALUES (@Id_miembro, @Id_membresia, @Nombre, @Ap_paterno, @Ap_materno, @Fecha_nacimiento, @Num_celular, @FechaRegistro, @Fecha_vencimiento, @Foto)";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id_miembro", miembro.IdMiembro);
                        command.Parameters.AddWithValue("@Id_membresia", miembro.IdMembresia);
                        command.Parameters.AddWithValue("@Nombre", miembro.Nombre);
                        command.Parameters.AddWithValue("@Ap_paterno", miembro.ApellidoPaterno);
                        command.Parameters.AddWithValue("@Ap_materno", miembro.ApellidoMaterno);
                        command.Parameters.AddWithValue("@Fecha_nacimiento", miembro.FechaNacimiento);
                        command.Parameters.AddWithValue("@Num_celular", miembro.NumeroCelular);
                        command.Parameters.AddWithValue("@FechaRegistro", miembro.FechaRegistro);
                        command.Parameters.AddWithValue("@Fecha_Vencimiento", miembro.FechaVencimiento);
                        command.Parameters.AddWithValue("@Foto", miembro.Foto);

                        int filasAfectadas = command.ExecuteNonQuery();
                        if (filasAfectadas > 0)
                        {
                            Console.WriteLine("Miembro registrado correctamente.");
                        }
                        else
                        {
                            Console.WriteLine("Error: No se insertaron datos.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar el miembro: " + ex.Message);
            }

        }

        // 🔹 Actualizar información de un miembro
        public void ModificarMiembro(Miembro miembro)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE fitmanage.miembro SET idMembresia = @idMembresia, nombres = @nombres, apellidoPaterno = @apellidoPaterno, apellidoMaterno = @apellidoMaterno, fechaNacimiento = @fechaNacimiento, numeroTelefono = @numeroTelefono, fechaRegistro = @fechaRegistro, fechaInicio = @fechaInicio, fechaFin = @fechaFin, fotografia = @fotografia WHERE idMiembro = @idMiembro";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idMiembro", miembro.IdMiembro);
                    command.Parameters.AddWithValue("@idMembresia", miembro.IdMembresia);
                    command.Parameters.AddWithValue("@nombres", miembro.Nombre);
                    command.Parameters.AddWithValue("@apellidoPaterno", miembro.ApellidoPaterno);
                    command.Parameters.AddWithValue("@apellidoMaterno", miembro.ApellidoMaterno);
                    command.Parameters.AddWithValue("@fechaNacimiento", miembro.FechaNacimiento);
                    command.Parameters.AddWithValue("@numeroTelefono", miembro.NumeroCelular);
                    command.Parameters.AddWithValue("@fechaRegistro", miembro.FechaRegistro);
                    command.Parameters.AddWithValue("@fechaFin", miembro.FechaVencimiento);
                    command.Parameters.AddWithValue("@fotografia", miembro.Foto);

                    command.ExecuteNonQuery();
                }
            }
        }

        // 🔹 Eliminar un miembro
        public void EliminarMiembro(int idMiembro)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM fitmanage.miembro WHERE idMiembro = @idMiembro";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idMiembro", idMiembro);
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}

