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
        private Panel tarjetaProducto, agregarProducto;
        private Button agregarBtn;
        private Button editarBtn;
        private Button guardarBtn;
        private Button cancelarBtn;
        private Producto productoSeleccionado;

        public PanelProducto()
        {
            productos = productoServicio.ObtenerTodos();
            InicializarComponentes();
        }

        private void InicializarComponentes()
        {
            this.Size = new Size(2000, 1010);
            this.BackColor = Color.WhiteSmoke;

            // Botón Agregar
            agregarBtn = new Button();
            agregarBtn.Text = "Agregar";
            agregarBtn.Font = new Font("Race Sport", 16, FontStyle.Bold);
            agregarBtn.ForeColor = Color.White;
            agregarBtn.Size = new Size(250, 50);
            agregarBtn.Location = new Point(1100, 350);
            agregarBtn.BackColor = Color.Gray;
            agregarBtn.Click += agregarBtn_Click;
            this.Controls.Add(agregarBtn);

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
            agregarBtn.Click += AgregarBtn_Click;
            this.Controls.Add(agregarBtn);

            // Tabla de productos
            // Tabla
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
            dgvProductos.DataSource = productos.OrderBy(p => p.Nombre).Select(p => new
            {
                Cantidad = p.Cantidad,
                Nombre = p.Nombre,
                Descripción = p.Descripcion,
                Precio = $"${p.Precio:N2}"
            }).ToList();
            dgvProductos.CellClick += DgvProductos_CellClick;
            this.Controls.Add(dgvProductos);

            // Tarjeta Producto
            tarjetaProducto = new Panel();
            tarjetaProducto.Location = new Point(350, 450);
            tarjetaProducto.Size = new Size(1000, 350);
            tarjetaProducto.BorderStyle = BorderStyle.FixedSingle;
            tarjetaProducto.Visible = false;
            this.Controls.Add(tarjetaProducto);
        }

        private void DgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (agregarProducto != null && agregarProducto.Visible)
                    agregarProducto.Visible = false;

                string nombre = dgvProductos.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                productoSeleccionado = productos.FirstOrDefault(p => p.Nombre == nombre);

                if (productoSeleccionado != null)
                {
                    tarjetaProducto.Controls.Clear();

                    int xIzq = 150;
                    int xDer = 600;
                    int y = 20;
                    int espacio = 100;

                    tarjetaProducto.Controls.Add(CrearTituloLabel("Nombre", xIzq, y));
                    tarjetaProducto.Controls.Add(CrearValorLabel(productoSeleccionado.Nombre, xIzq, y + 45));

                    tarjetaProducto.Controls.Add(CrearTituloLabel("Precio (MXN)", xDer, y));
                    tarjetaProducto.Controls.Add(CrearValorLabel(productoSeleccionado.Precio.ToString("C"), xDer, y + 45));

                    y += espacio;
                    tarjetaProducto.Controls.Add(CrearTituloLabel("Descripción", xIzq, y));
                    tarjetaProducto.Controls.Add(CrearValorLabel(productoSeleccionado.Descripcion, xIzq, y + 45));

                    tarjetaProducto.Controls.Add(CrearTituloLabel("Cantidad", xDer, y));
                    tarjetaProducto.Controls.Add(CrearValorLabel(productoSeleccionado.Cantidad.ToString(), xDer, y + 45));

                    // Botón Editar
                    Button btnEditar = new Button
                    {
                        Text = "Editar",
                        Font = new Font("Race Sport", 14, FontStyle.Bold),
                        Size = new Size(150, 50),
                        BackColor = Color.Orange,
                        ForeColor = Color.White,
                        Location = new Point(800, 20)
                    };
                    btnEditar.Click += (s, ev) => ActivarModoEdicion();
                    tarjetaProducto.Controls.Add(btnEditar);

                    tarjetaProducto.Visible = true;
                }
            }
        }

        private void ActivarModoEdicion()
        {
            tarjetaProducto.Controls.Clear();

            int xIzq = 150;
            int xDer = 600;
            int y = 20;
            int espacio = 100;

            tarjetaProducto.Controls.Add(CrearTituloLabel("Nombre", xIzq, y));
            TextBox txtNombre = CrearTextBox(productoSeleccionado.Nombre, xIzq, y + 45);
            tarjetaProducto.Controls.Add(txtNombre);

            tarjetaProducto.Controls.Add(CrearTituloLabel("Precio (MXN)", xDer, y));
            TextBox txtPrecio = CrearTextBox(productoSeleccionado.Precio.ToString(), xDer, y + 45);
            tarjetaProducto.Controls.Add(txtPrecio);

            y += espacio;
            tarjetaProducto.Controls.Add(CrearTituloLabel("Descripción", xIzq, y));
            TextBox txtDesc = CrearTextBox(productoSeleccionado.Descripcion, xIzq, y + 45);
            tarjetaProducto.Controls.Add(txtDesc);

            tarjetaProducto.Controls.Add(CrearTituloLabel("Cantidad", xDer, y));
            TextBox txtCantidad = CrearTextBox(productoSeleccionado.Cantidad.ToString(), xDer, y + 45);
            tarjetaProducto.Controls.Add(txtCantidad);

            Button btnGuardar = new Button
            {
                Text = "Guardar",
                Font = new Font("Race Sport", 14, FontStyle.Bold),
                Size = new Size(150, 50),
                BackColor = Color.Green,
                ForeColor = Color.White,
                Location = new Point(800, 250)
            };

            btnGuardar.Click += (s, e) =>
            {
                if (decimal.TryParse(txtPrecio.Text, out decimal precio) &&
                    int.TryParse(txtCantidad.Text, out int cantidad))
                {
                    productoSeleccionado.Nombre = txtNombre.Text.Trim();
                    productoSeleccionado.Precio = precio;
                    productoSeleccionado.Descripcion = txtDesc.Text.Trim();
                    productoSeleccionado.Cantidad = cantidad;

                    productoServicio.Actualizar(productoSeleccionado);
                    MessageBox.Show("Producto actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    productos = productoServicio.ObtenerTodos();
                    dgvProductos.DataSource = productos.OrderBy(p => p.Nombre).Select(p => new
                    {
                        Cantidad = p.Cantidad,
                        Nombre = p.Nombre,
                        Descripción = p.Descripcion,
                        Precio = $"${p.Precio:N2}"
                    }).ToList();

                    tarjetaProducto.Visible = false;
                }
                else
                {
                    MessageBox.Show("Verifique los valores de precio y cantidad.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };

            tarjetaProducto.Controls.Add(btnGuardar);
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

        private TextBox CrearTextBox(string texto, int x, int y)
        {
            return new TextBox
            {
                Text = texto,
                Location = new Point(x, y),
                Size = new Size(300, 30),
                Font = new Font("Futura", 18, FontStyle.Regular)
            };
        }

        private void agregarBtn_Click(object sender, System.EventArgs e)
        {
            MostrarPanelAgregar();
        }

        private void MostrarPanelAgregar()
        {
            if (tarjetaProducto.Visible)
                tarjetaProducto.Visible = false;

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

            agregarProducto.Controls.Add(CrearTituloLabel("Nombre", xIzq + 140, y));
            TextBox txtNombre = new TextBox { Location = new Point(xIzq, y + 30), Width = 400, Font = new Font("Futura", 14) };
            agregarProducto.Controls.Add(txtNombre);

            y += espacio;
            agregarProducto.Controls.Add(CrearTituloLabel("Descripción", xIzq + 120, y + 20));
            TextBox txtDescripcion = new TextBox { Location = new Point(xIzq, y + 50), Width = 400, Font = new Font("Futura", 14) };
            agregarProducto.Controls.Add(txtDescripcion);

            agregarProducto.Controls.Add(CrearTituloLabel("Precio", xIzq + 640, 20));
            TextBox txtPrecio = new TextBox { Location = new Point(xIzq + 550, 50), Width = 250, Font = new Font("Futura", 14) };
            agregarProducto.Controls.Add(txtPrecio);

            agregarProducto.Controls.Add(CrearTituloLabel("Cantidad", xIzq + 620, 110));
            TextBox txtCantidad = new TextBox { Location = new Point(xIzq + 550, 140), Width = 250, Font = new Font("Futura", 14) };
            agregarProducto.Controls.Add(txtCantidad);

            Button btnGuardar = new Button
            {
                Text = "Guardar",
                Font = new Font("Race Sport", 14, FontStyle.Bold),
                Size = new Size(200, 50),
                ForeColor = Color.White,
                BackColor = Color.Green,
                Location = new Point(750, 250)
            };

            btnGuardar.Click += (s, e) =>
            {
                try
                {
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

                        productos = productoServicio.ObtenerTodos();
                        dgvProductos.DataSource = productos.OrderBy(p => p.Nombre).Select(p => new
                        {
                            Cantidad = p.Cantidad,
                            Nombre = p.Nombre,
                            Descripción = p.Descripcion,
                            Precio = $"${p.Precio:N2}"
                        }).ToList();

                        agregarProducto.Visible = false;
                        MessageBox.Show("Producto agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Precio o cantidad inválidos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };


            agregarProducto.Controls.Add(btnGuardar);
            this.Controls.Add(agregarProducto);
        }
    }
}