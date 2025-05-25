using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Entidad;
using Negocio;

namespace Presentacion
{
    public class PanelProducto : Panel
    {
        private List<Producto> productos;
        private ProductoServicio productoServicio = new ProductoServicio();
        private DataGridView dgvProductos;
        private Panel tarjetaProducto;

        public PanelProducto()
        {
            productos = productoServicio.ObtenerTodos();
            InicializarComponentes();
        }

        private void InicializarComponentes()
        {
            this.Size = new Size(2000, 1010);
            this.BackColor = Color.WhiteSmoke;

            // Título
            Label tituloLbl = new Label();
            tituloLbl.Text = "PRODUCTOS";
            tituloLbl.Font = new Font("Race Sport", 50, FontStyle.Bold);
            tituloLbl.AutoSize = true;
            tituloLbl.Location = new Point(600, 10);
            this.Controls.Add(tituloLbl);

            // Tabla de productos
            dgvProductos = new DataGridView();
            dgvProductos.Location = new Point(350, 130);
            dgvProductos.Size = new Size(1000, 200);
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.ReadOnly = true;
            dgvProductos.DefaultCellStyle.Font = new Font("Futura", 16);
            dgvProductos.ColumnHeadersDefaultCellStyle.Font = new Font("Futura", 18, FontStyle.Bold);
            dgvProductos.RowTemplate.Height = 50;
            dgvProductos.ColumnHeadersHeight = 40;
            dgvProductos.DataSource = productos
    .OrderBy(p => p.Nombre) // Ordenar por nombre
    .Select(p => new
    {
        Cantidad = p.Cantidad,
        Nombre = p.Nombre,
        Descripción = p.Descripcion,
        Precio = $"${p.Precio:N2}"
    })
    .ToList();

            dgvProductos.CellClick += DgvProductos_CellClick;
            this.Controls.Add(dgvProductos);

            // Tarjeta
            tarjetaProducto = new Panel();
            tarjetaProducto.Location = new Point(350, 450);
            tarjetaProducto.Size = new Size(1000, 300);
            tarjetaProducto.BorderStyle = BorderStyle.FixedSingle;
            tarjetaProducto.Visible = false;
            this.Controls.Add(tarjetaProducto);
        }

        private void DgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string nombre = dgvProductos.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                Producto producto = productos.FirstOrDefault(p => p.Nombre == nombre);

                if (producto != null)
                {
                    tarjetaProducto.Controls.Clear();

                    int xIzq = 150;
                    int xDer = 600;
                    int y = 20;
                    int espacio = 100;

                    //Nombre
                    tarjetaProducto.Controls.Add(CrearTituloLabel("Nombre", xIzq, y));
                    tarjetaProducto.Controls.Add(CrearValorLabel(producto.Nombre, xIzq, y + 45));

                    //Precio
                    tarjetaProducto.Controls.Add(CrearTituloLabel("Precio (MXN)", xDer, y));
                    tarjetaProducto.Controls.Add(CrearValorLabel(producto.Precio.ToString("C"), xDer, y + 45));

                    //Descripción
                    y += espacio;
                    tarjetaProducto.Controls.Add(CrearTituloLabel("Descripción", xIzq, y));
                    tarjetaProducto.Controls.Add(CrearValorLabel(producto.Descripcion, xIzq, y + 45));

                    //Cantidad
                    tarjetaProducto.Controls.Add(CrearTituloLabel("Cantidad", xDer, y));
                    tarjetaProducto.Controls.Add(CrearValorLabel(producto.Cantidad.ToString(), xDer, y + 45));

                    tarjetaProducto.Visible = true;
                }
            }
        }

        private Label CrearValorLabel(string texto, int x, int y)
        {
            return new Label
            {
                Text = texto,
                Location = new Point(x, y),
                AutoSize = true,
                Font = new Font("Futura", 18, FontStyle.Regular),
                ForeColor = Color.Black
            };
        }

        private Label CrearTituloLabel(string texto, int x, int y)
        {
            return new Label
            {
                Text = texto,
                Location = new Point(x, y),
                AutoSize = true,
                Font = new Font("Tahoma", 18, FontStyle.Bold),
                ForeColor = Color.Black
            };
        }
    }
}
