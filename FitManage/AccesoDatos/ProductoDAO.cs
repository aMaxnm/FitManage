// Capa AccesoDatos
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Entidad;

namespace AccesoDatos
{
    public class ProductoDAO
    {
        private string connectionString = "server=localhost;user=root;password=root;database=fitmanage;";

        public List<Producto> ObtenerProductos()
        {
            List<Producto> productos = new List<Producto>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM productos";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        productos.Add(new Producto
                        {
                            IdProducto = reader.GetInt32("Id_producto"),
                            Nombre = reader.GetString("Nom_producto"),
                            Descripcion = reader.GetString("Descripcion"),
                            Cantidad = reader.GetInt32("Cantidad"),
                            Precio = reader.GetDecimal("Precio")
                        });
                    }
                }
            }
            return productos;
        }

        public void EditarProducto(Producto p)
        {
        
        }

    }
}
