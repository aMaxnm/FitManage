using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Entidad;
using System.Data.SqlClient;

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
        public bool MiembroExiste(string nombre, string apellidoPaterno, string apellidoMaterno, string telefono)
        {
            bool existe = false;

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM fitmanage.miembro WHERE Nombre = @Nombre AND Ap_paterno = @Ap_paterno AND Ap_materno = @Ap_materno AND Num_celular = @Num_celular";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Nombre", nombre);
                        command.Parameters.AddWithValue("@Ap_paterno", apellidoPaterno);
                        command.Parameters.AddWithValue("@Ap_materno", apellidoMaterno);
                        command.Parameters.AddWithValue("@Num_celular", telefono);

                        int count = Convert.ToInt32(command.ExecuteScalar());
                        existe = (count > 0); // Si `count > 0`, el cliente ya existe
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al verificar existencia del cliente: " + ex.Message);
            }

            return existe; //Retorna `true` si el cliente ya existe, `false` si no
        }
        public int AgregarMiembro(Miembro miembro)
        {
            int idGenerado = -1;
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO fitmanage.miembro (Id_membresia, Nombre, Ap_paterno, Ap_materno, Fecha_nacimiento, Num_celular, FechaRegistro, Fecha_vencimiento, Foto) " +
                                   "VALUES (@Id_membresia, @Nombre, @Ap_paterno, @Ap_materno, @Fecha_nacimiento, @Num_celular, @FechaRegistro, @Fecha_vencimiento, @Foto);";

                    string lastIdQuery = "SELECT LAST_INSERT_ID();";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id_membresia", miembro.IdMembresia);
                        command.Parameters.AddWithValue("@Nombre", miembro.Nombres);
                        command.Parameters.AddWithValue("@Ap_paterno", miembro.ApellidoPaterno);
                        command.Parameters.AddWithValue("@Ap_materno", miembro.ApellidoMaterno);
                        command.Parameters.AddWithValue("@Fecha_nacimiento", miembro.FechaNacimiento);
                        command.Parameters.AddWithValue("@Num_celular", miembro.NumeroTelefono);
                        command.Parameters.AddWithValue("@FechaRegistro", miembro.FechaRegistro);
                        command.Parameters.AddWithValue("@Fecha_vencimiento", miembro.FechaVencimiento);
                        command.Parameters.AddWithValue("@Foto", miembro.Fotografia);

                        int filasAfectadas = command.ExecuteNonQuery();
                        if (filasAfectadas > 0)
                        {
                            using (var lastIdCommand = new MySqlCommand(lastIdQuery, connection))
                            {
                                idGenerado = Convert.ToInt32(lastIdCommand.ExecuteScalar());
                            }

                            Console.WriteLine($"Miembro registrado correctamente con ID: {idGenerado}");
                            return idGenerado;
                        }
                        else
                        {
                            Console.WriteLine("Error: No se insertaron datos.");
                            return -1;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar el miembro: " + ex.Message);
                return -1; // Retorna -1 en caso de excepción
            }
        }
        // Actualizar información de un miembro
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
        public bool RenovarMembresia(int idMiembro, int nuevaMembresiaId, DateTime FV)
        {
            bool actualizado = false;
            DateTime nuevaFechaRegistro = DateTime.Now.Date;
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE fitmanage.miembro " +
                                   "SET Id_membresia = @Id_membresia, FechaRegistro = @FechaRegistro, Fecha_Vencimiento = @Fecha_Vencimiento " + 
                                   "WHERE Id_miembro = @Id_miembro";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id_miembro", idMiembro);
                        command.Parameters.AddWithValue("@Id_membresia", nuevaMembresiaId);
                        command.Parameters.AddWithValue("@FechaRegistro", nuevaFechaRegistro);
                        command.Parameters.AddWithValue("@Fecha_Vencimiento", FV);

                        int filasAfectadas = command.ExecuteNonQuery();
                        actualizado = filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al renovar membresía: " + ex.Message);
            }

            return actualizado; // 🔹 Retorna `true` si la actualización fue exitosa
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
        //Actualizar Datos
        public void Actualizar(Miembro miembro)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = @"UPDATE Miembro
                         SET Nombre = @Nombres,
                             Ap_paterno = @ApellidoPaterno,
                             Ap_materno = @ApellidoMaterno,
                             Num_celular = @NumeroTelefono
                         WHERE Id_miembro = @IdMiembro";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nombres", miembro.Nombres);
                cmd.Parameters.AddWithValue("@ApellidoPaterno", miembro.ApellidoPaterno);
                cmd.Parameters.AddWithValue("@ApellidoMaterno", miembro.ApellidoMaterno);
                cmd.Parameters.AddWithValue("@NumeroTelefono", miembro.NumeroTelefono);
                cmd.Parameters.AddWithValue("@IdMiembro", miembro.IdMiembro);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public Miembro ObtenerMiembroPorId(int idMiembro)
        {
            Miembro miembro = null;

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM fitmanage.miembro WHERE Id_miembro = @id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", idMiembro);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // Si encuentra una membresía
                        {
                            miembro = new Miembro
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
                            };
                        }
                    }
                }
            }
            return miembro; // Retorna la membresía encontrada o null si no existe
        }
    }
}

