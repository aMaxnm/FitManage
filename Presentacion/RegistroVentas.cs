using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Negocio;

namespace Presentacion
{
    internal class RegistroVentas : Panel
    {
        private VentaServicio ventaServicio = new VentaServicio();
        public RegistroVentas()
        {
            InicializarComponentes();
        }
        private void InicializarComponentes()
        {
            this.Size = new Size(2000, 1010);
            this.BackColor = Color.WhiteSmoke;

            //Titulo
            Label tituloLbl = new Label();
            tituloLbl.Text = "REGISTRO VENTAS";
            tituloLbl.Font = new Font("Race Sport", 50, FontStyle.Bold);
            tituloLbl.AutoSize = true;
            tituloLbl.Location = new Point(475, 10);
            this.Controls.Add(tituloLbl);

            //TextBox
            TextBox registroTxt = new TextBox();
            registroTxt.Multiline = true;
            registroTxt.Size = new Size(700, 500);
            registroTxt.Location = new Point(500, 150);
            registroTxt.ScrollBars = ScrollBars.Vertical;  // Agrega una barra de desplazamiento vertical
            registroTxt.ReadOnly = true;
            CargarRegistroVentas(registroTxt);
            this.Controls.Add(registroTxt);
        }
        private void CargarRegistroVentas(TextBox registroTxt)
        {
            var ventas = ventaServicio.ObtenerTodasLasVentas(); // Método en capa negocio
            if (ventas == null || ventas.Count == 0)
            {
                MessageBox.Show("No hay registros de ventas disponibles.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            StringBuilder sb = new StringBuilder();
            foreach (var venta in ventas)
            {
                sb.AppendLine($"--- Ticket #{venta.IdVenta} ---");
                sb.AppendLine($"Fecha: {venta.Fecha}");
                sb.AppendLine($"Tipo: {venta.TipoVenta}");
                sb.AppendLine($"Membresía: {venta.TipoMembresia}");

                decimal totalVenta = 0;

                if (venta.TipoVenta == "Registro de Membresía")
                {
                    // Si la venta es una membresía, mostrar solo su precio
                    totalVenta = venta.PrecioMembresia; // Asegúrate de que esta propiedad existe en Venta
                    sb.AppendLine($"Precio de la membresía: ${totalVenta}");
                }
                else
                {
                    // Si la venta es de productos, mostrar detalles
                    sb.AppendLine("Productos vendidos:");
                    foreach (var detalle in venta.Detalles)
                    {
                        decimal subtotal = detalle.Cantidad * detalle.PrecioUnitario;
                        totalVenta += subtotal;
                        sb.AppendLine($"  - Producto: {detalle.NombreProducto}, Cantidad: {detalle.Cantidad}, Precio: ${detalle.PrecioUnitario}, Subtotal: ${subtotal}");
                    }
                }

                sb.AppendLine($"TOTAL: ${totalVenta}");
                sb.AppendLine("------------------------------\n");
            }

            registroTxt.Text = sb.ToString();
        }

    }
}
