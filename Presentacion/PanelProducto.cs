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
        private Panel tarjetaProducto, agregarProducto;
        private Button agregarBtn;

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

            //Boton
            agregarBtn = new Button();
            agregarBtn.Text = "Agregar";
            agregarBtn.Font = new Font("Race Sport", 16, FontStyle.Bold); 
            agregarBtn.ForeColor = Color.DarkGray;
            agregarBtn.Size = new Size(250, 50);
            agregarBtn.Location = new Point(1100, 350); 
            agregarBtn.BackColor = Color.DarkGray;
            agregarBtn.Click += agregarBtn_Click;
            this.Controls.Add(agregarBtn);

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
            }).ToList();

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
        private void agregarBtn_Click(object sender, System.EventArgs e)
        {
            MostrarPanelAgregar();
        }
        private void MostrarPanelAgregar()
        {
            if (agregarProducto != null)
            {
                agregarProducto.Visible = true;
                return;
            }

            agregarProducto = new Panel();
            agregarProducto.Location = new Point(350, 420);
            agregarProducto.Size = new Size(1000, 350);
            agregarProducto.BorderStyle = BorderStyle.FixedSingle;
            agregarProducto.BackColor = Color.WhiteSmoke;

            int xIzq = 50;
            int y = 20;
            int espacio = 70;

            // Nombre
            agregarProducto.Controls.Add(CrearTituloLabel("Nombre", xIzq+140, y));
            TextBox txtNombre = new TextBox { Location = new Point(xIzq, y + 30), Width = 400, Font = new Font("Futura", 14) };
            agregarProducto.Controls.Add(txtNombre);

            // Descripción
            y += espacio;
            agregarProducto.Controls.Add(CrearTituloLabel("Descripción", xIzq+120, y+20));
            TextBox txtDescripcion = new TextBox { Location = new Point(xIzq, y + 50), Width = 400, Font = new Font("Futura", 14) };
            agregarProducto.Controls.Add(txtDescripcion);

            // Precio
            y += espacio;
            agregarProducto.Controls.Add(CrearTituloLabel("Precio", xIzq+640, 20));
            TextBox txtPrecio = new TextBox { Location = new Point(xIzq+550, 50), Width = 250, Font = new Font("Futura", 14) };
            agregarProducto.Controls.Add(txtPrecio);

            // Cantidad
            //y += espacio;
            agregarProducto.Controls.Add(CrearTituloLabel("Cantidad", xIzq+620, 110));
            TextBox txtCantidad = new TextBox { Location = new Point(xIzq+550, 140), Width = 250, Font = new Font("Futura", 14) };
            agregarProducto.Controls.Add(txtCantidad);

            // Botón Guardar
            Button btnGuardar = new Button
            {
                Text = "Guardar",
                Font = new Font("Race Sport", 14, FontStyle.Bold),
                Size = new Size(200, 50),
                ForeColor = Color.White,
                BackColor = Color.Green,
                Location = new Point(xIzq+600, y + 50)
            };
            btnGuardar.Click += (s, e) =>
            {
                // Validar y guardar producto (opcional: validar campos vacíos)
                if (decimal.TryParse(txtPrecio.Text, out decimal precio) &&
                    int.TryParse(txtCantidad.Text, out int cantidad))
                {
                    Producto nuevo = new Producto
                    {
                        Nombre = txtNombre.Text,
                        Descripcion = txtDescripcion.Text,
                        Precio = precio,
                        Cantidad = cantidad
                    };

                    productoServicio.Guardar(nuevo);
                    productos = productoServicio.ObtenerTodos(); // Refrescar lista
                    dgvProductos.DataSource = productos.OrderBy(p => p.Nombre).Select(p => new
                    {
                        Cantidad = p.Cantidad,
                        Nombre = p.Nombre,
                        Descripción = p.Descripcion,
                        Precio = $"${p.Precio:N2}"
                    }).ToList();

                    agregarProducto.Visible = false;
                }
                else
                {
                    MessageBox.Show("Precio o cantidad inválidos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
            agregarProducto.Controls.Add(btnGuardar);

            this.Controls.Add(agregarProducto);
        }

    }
}
