using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Entidad;
using K4os.Compression.LZ4.Internal;

namespace AccesoDatos
{
    public class ProductoDAO
    {
        private string connectionString = "server=localhost;port=8000;user=root;password=root;database=fitmanage;";

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

        public bool Insertar(Producto producto)
        {
            bool insertado = false;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO productos (Nom_producto, Descripcion, Cantidad, Precio) " +
                                   "VALUES (@Nom_producto, @Descripcion, @Cantidad, @Precio)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nom_producto", producto.Nombre);
                        cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                        cmd.Parameters.AddWithValue("@Cantidad", producto.Cantidad);
                        cmd.Parameters.AddWithValue("@Precio", producto.Precio);

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        insertado = filasAfectadas > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al insertar producto: " + ex.Message);
                }
            }
            return insertado; 
        }

        public void EliminarStock(List<Producto> carrito)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                using (MySqlTransaction transaccion = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (var producto in carrito)
                        {
                            string query = "UPDATE productos SET Cantidad = Cantidad - @cantidad WHERE Id_producto = @id";

                            using (MySqlCommand cmd = new MySqlCommand(query, conn, transaccion))
                            {
                                cmd.Parameters.AddWithValue("@cantidad", producto.Cantidad);
                                cmd.Parameters.AddWithValue("@id", producto.IdProducto);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaccion.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaccion.Rollback();
                        //MessageBox.Show("Error al actualizar el stock: " + ex.Message);
                    }
                }
 
            }

        }
public void EditarProducto(Producto p){
               using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"UPDATE fitmanage.productos
                             SET
                             Precio = @Precio, 
                             Nom_producto = @Nom_producto,
                             Descripcion = @Descripcion,
                             Cantidad = @Cantidad
                         WHERE Id_producto = @id_producto";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id_producto", p.IdProducto);
                        command.Parameters.AddWithValue("@Nom_producto", p.Nombre);
                        command.Parameters.AddWithValue("@Descripcion", p.Descripcion);
                        command.Parameters.AddWithValue("Cantidad", p.Cantidad);
                        command.Parameters.AddWithValue("@Precio", p.Precio);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al editar producto: " + ex.Message);

                }
}
    }
}
