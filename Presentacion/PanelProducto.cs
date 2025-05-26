using System;
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
        private Button editarBtn;
        private Button guardarBtn;
        private Button cancelarBtn;


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


            // Botón editar
            editarBtn = new Button
            {
                Text = "editar",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Location = new Point(1100, 500),
                Size = new Size(100, 40),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            editarBtn.Click += (s, e) =>
            {
                dgvProductos.ReadOnly = false;
                

                guardarBtn.Visible = true;
                cancelarBtn.Visible = true;
                editarBtn.Visible = false;
            };
            this.Controls.Add(editarBtn);

            // Botón guardar
            guardarBtn = new Button
            {
                Text = "guardar",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Location = new Point(780, 500),
                Size = new Size(100, 40),
                BackColor = Color.Green,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Visible = false
            };

            guardarBtn.Click += (s, e) =>
            {
                dgvProductos.ReadOnly = true;
                editarBtn.Visible = true;
                guardarBtn.Visible = false;
                cancelarBtn.Visible = false;
                if (dgvProductos.CurrentRow != null)
                {
                    int fila = dgvProductos.CurrentCell.RowIndex;

                    Producto p = new Producto
                    {
                        IdProducto = Convert.ToInt32(dgvProductos.Rows[fila].Cells["IdProducto"].Value),
                        Nombre = dgvProductos.Rows[fila].Cells["Nombre"].Value.ToString(),
                        Descripcion = dgvProductos.Rows[fila].Cells["Descripcion"].Value.ToString(),
                        Precio = Convert.ToDecimal(dgvProductos.Rows[fila].Cells["Precio"].Value),
                        Cantidad = Convert.ToInt32(dgvProductos.Rows[fila].Cells["Precio"].Value)
                    };

                    productoServicio.EditarProducto(p);

                    MessageBox.Show("Cambios guardados correctamente.");

                }
                else
                {
                    MessageBox.Show("No se ha seleccionado ninguna fila.");
                }
            };
            this.Controls.Add(guardarBtn);

            //boton cancelar
            cancelarBtn = new Button
            {
                Text = "cancelar",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Location = new Point(960, 500),
                Size = new Size(100, 40),
                BackColor = Color.Red,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Visible = false
            };

            cancelarBtn.Click += (s, e) =>
            {
                dgvProductos.ReadOnly = true;
                editarBtn.Visible = true;
                guardarBtn.Visible = false;
                cancelarBtn.Visible = false;
                MessageBox.Show("Cambios cancelados.");

                dgvProductos.Rows.Clear();
                dgvProductos.DataSource = productos;
                dgvProductos.Refresh();

            };
            this.Controls.Add(cancelarBtn);
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
