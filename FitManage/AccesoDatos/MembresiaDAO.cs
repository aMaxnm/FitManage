using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Entidad;

namespace AccesoDatos
{
    public class MembresiaDAO
    {
        private string connectionString = "server=localhost;user=root;password=root;database=fitmanage";

        public List<Membresia> ObtenerMembresias()
        {
            List<Membresia> listaMembresias = new List<Membresia>();

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM fitmanage.membresia";

                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listaMembresias.Add(new Membresia
                        {
                            Id = reader.GetInt32("Id_membresia"),
                            Tipo = reader.GetString("Tipo_Membresia"),
                            Precio = reader.GetDecimal("Precio")
                        });
                    }
                }
            }
            return listaMembresias;
        }

        // 🔹 Nuevo método para obtener un miembro específico por ID
        public Membresia ObtenerMembresiaPorId(int idMembresia)
        {
            Membresia membresia = null;

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM fitmanage.miembresia WHERE Id_membresi = @id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", idMembresia);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // Si encuentra una membresía
                        {
                            membresia = new Membresia
                            {
                                Id = reader.GetInt32("Id_membresia"),
                                Duracion = reader.GetInt32("Duración"),
                                Precio = reader.GetDecimal("Precio"),
                                Tipo = reader.GetString("Tipo_Membresia")
                            };
                        }
                    }
                }
            }
            return membresia; // Retorna la membresía encontrada o null si no existe
        }

        public void EditarMembresia(Membresia mem)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = @"UPDATE fitmanage.membresia 
                             SET
                             Precio = @precio, 
                             Tipo_Membresia = @tipo 
                         WHERE Id_membresia = @id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", mem.Id);

                    command.Parameters.AddWithValue("@precio", mem.Precio);
                    command.Parameters.AddWithValue("@tipo", mem.Tipo);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

