using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Entidad;

namespace AccesoDatos
{
    public class VentaDAO
    {
        public int InsertarVenta(Venta venta)
            {
                int idVenta = 0;
                using (var conn = new MySqlConnection("server=localhost;user=root;password=root;database=fitmanage;"))
                {
                    conn.Open();
                    using (var tran = conn.BeginTransaction())
                    {
                        try
                        {
                            var cmdVenta = new MySqlCommand("INSERT INTO venta (Fecha, Tipo_Venta, Id_membresia) VALUES (@fecha, @tipo, @id_membresia); SELECT LAST_INSERT_ID();", conn, tran);
                            cmdVenta.Parameters.AddWithValue("@fecha", venta.Fecha);
                            cmdVenta.Parameters.AddWithValue("@tipo", venta.TipoVenta);
                            cmdVenta.Parameters.AddWithValue("@id_membresia", venta.IdMembresia > 0 ? venta.IdMembresia : (object)DBNull.Value);
                            idVenta = Convert.ToInt32(cmdVenta.ExecuteScalar());

                            foreach (var detalle in venta.Detalles)
                            {
                            var cmdDetalle = new MySqlCommand("INSERT INTO detalles (Id_venta, Id_producto, Cantidad) VALUES (@id_venta, @id_producto, @cantidad)", conn, tran); cmdDetalle.Parameters.AddWithValue("@id_venta", idVenta);
                                cmdDetalle.Parameters.AddWithValue("@id_producto", detalle.IdProducto);
                                cmdDetalle.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                                cmdDetalle.ExecuteNonQuery();
                            }

                            tran.Commit();
                        }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        Console.WriteLine("Error al insertar la venta: " + ex.Message);
                    }
                }
                }

                return idVenta;
            }
        public List<Venta> ObtenerVentasConDetalles()
        {
            var ventas = new List<Venta>();

            using (var conn = new MySqlConnection("server=localhost;user=root;password=root;database=fitmanage;"))
            {
                conn.Open();
                string query = @"
                    SELECT 
                    v.Id_Venta, v.Fecha, v.Tipo_Venta, v.Id_membresia,
                    d.Id_producto, d.Cantidad,
                    p.Nom_producto,
                    p.Precio
                    FROM venta v
                    LEFT JOIN detalles d ON v.Id_Venta = d.Id_Venta
                    LEFT JOIN productos p ON d.Id_producto = p.Id_Producto;";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idVenta = reader.GetInt32(0);
                        var venta = ventas.FirstOrDefault(v => v.IdVenta == idVenta);

                        if (venta == null)
                        {
                            venta = new Venta
                            {
                                IdVenta = idVenta,
                                Fecha = reader.GetDateTime(1),
                                TipoVenta = reader.GetString(2),
                                IdMembresia = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                                Detalles = new List<DetalleVenta>()
                            };

                            ventas.Add(venta);
                        }

                        if (!reader.IsDBNull(4)) // Si hay detalles
                        {
                            venta.Detalles.Add(new DetalleVenta
                            {
                                IdProducto = reader.GetInt32(4),
                                Cantidad = reader.GetInt32(5),
                                NombreProducto = reader["Nom_producto"].ToString(),
                                PrecioUnitario = Convert.ToDecimal(reader["Precio"])
                            });
                        }
                    }
                }
            }

            return ventas;
        }

    }
}
