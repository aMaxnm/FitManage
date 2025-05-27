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
        private Panel tarjetaProducto, agregarProducto, contenidoPanel;
        private Button agregarBtn;
        private Button editarBtn;
        private Button guardarBtn;
        private Button cancelarBtn;
        private TextBox txtNombreEdicion;
        private TextBox txtPrecioEdicion;
        private TextBox txtDescripcionEdicion;
        private TextBox txtCantidadEdicion;
        private bool modoEdicion = false;
        private Producto producto;
        private Producto productoEditar;



        public PanelProducto()
        {
            productos = productoServicio.ObtenerTodos();
            InicializarComponentes();
        }
        private void InicializarComponentes()
        {
            this.Size = new Size(2000, 1010);
            this.BackColor = Color.WhiteSmoke;
            contenidoPanel = new Panel();
            contenidoPanel.Visible = true;
            this.Controls.Add(contenidoPanel);

            // Título
            Label tituloLbl = new Label();
            tituloLbl.Text = "PRODUCTOS";
            tituloLbl.Font = new Font("Race Sport", 50, FontStyle.Bold);
            tituloLbl.AutoSize = true;
            tituloLbl.Location = new Point(600, 10);
            this.Controls.Add(tituloLbl);

            //Boton Agregar
            agregarBtn = new Button();
            agregarBtn.Text = "Agregar";
            agregarBtn.Font = new Font("Arial", 10, FontStyle.Bold); 
            agregarBtn.ForeColor = Color.White;
            agregarBtn.Size = new Size(100, 40);
            agregarBtn.Location = new Point(1100, 350); 
            agregarBtn.BackColor = Color.DarkGray;
            agregarBtn.Click += agregarBtn_Click;
            this.Controls.Add(agregarBtn);

            //AQUI
            guardarBtn = new Button
            {
                Text = "Guardar",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Size = new Size(100, 40),
                BackColor = Color.Green,
                ForeColor = Color.White,
                Location = new Point(650, 250),
                Visible = false
            };
            guardarBtn.Click += (s, ev) =>
            {
                if (producto != null)
                {
                    productoEditar = new Producto
                    {
                        IdProducto = producto.IdProducto, // Aquí obtienes el ID
                        Nombre = txtNombreEdicion.Text,
                        Descripcion = txtDescripcionEdicion.Text,
                        Precio = Convert.ToDecimal(txtPrecioEdicion.Text),
                        Cantidad = Convert.ToInt32(txtCantidadEdicion.Text)
                    };

                    productoServicio.EditarProducto(productoEditar);

                    // Refrescar lista y tabla
                    productos = productoServicio.ObtenerTodos();
                    dgvProductos.DataSource = productos.OrderBy(p => p.Nombre).Select(p => new
                    {
                        Id = p.IdProducto,
                        Cantidad = p.Cantidad,
                        Nombre = p.Nombre,
                        Descripción = p.Descripcion,
                        Precio = $"${p.Precio:N2}"
                    }).ToList();

                    // Volver a la vista principal
                    contenidoPanel.Visible = false;
                    editarBtn.Visible = true;
                    modoEdicion = false;
                }
                else
                {
                    MessageBox.Show("No se ha seleccionado ningún producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };

            cancelarBtn = new Button
            {
                Text = "Cancelar",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Size = new Size(100, 40),
                BackColor = Color.Red,
                ForeColor = Color.White,
                Location = new Point(770, 250),
                Visible = false
            };

            // Botón editar
            editarBtn = new Button
            {
                Text = "editar",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Location = new Point(agregarBtn.Location.X + agregarBtn.Width + 20, agregarBtn.Location.Y),
                Size = new Size(100, 40),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            editarBtn.Click += (s, ev) =>
            {
                modoEdicion = true; // Activar modo edición
                tarjetaProducto.Controls.Clear();

                Label lblInstruccion = new Label
                {
                    Text = "Selecciona el producto a editar",
                    Font = new Font("Arial", 16, FontStyle.Bold),
                    ForeColor = Color.DarkBlue,
                    AutoSize = true,
                    Location = new Point(20, 20)
                };

                tarjetaProducto.Controls.Add(lblInstruccion);

              
                this.Controls.Remove(contenidoPanel);
                contenidoPanel = tarjetaProducto;
                contenidoPanel.Refresh();
                contenidoPanel.Visible = true;
                this.Controls.Add(contenidoPanel);
            };


            this.Controls.Add(editarBtn);

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
                    Id = p.IdProducto,
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
            tarjetaProducto.Location = new Point(dgvProductos.Location.X, dgvProductos.Location.Y + 70 + dgvProductos.Size.Height);
            tarjetaProducto.Size = new Size(dgvProductos.Width, 350);
            tarjetaProducto.BorderStyle = BorderStyle.FixedSingle;
        }
        private void DgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string nombre = dgvProductos.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                producto = productos.FirstOrDefault(p => p.Nombre == nombre);

                if (producto != null)
                {
                    tarjetaProducto.Controls.Clear();

                    int xIzq = 150;
                    int xDer = 600;
                    int y = 20;
                    int espacio = 100;

                    // Crear campos
                    tarjetaProducto.Controls.Add(CrearTituloLabel("Nombre", xIzq, y));
                    txtNombreEdicion = CrearValorTextBox(producto.Nombre, xIzq, y + 45);
                    tarjetaProducto.Controls.Add(txtNombreEdicion);

                    tarjetaProducto.Controls.Add(CrearTituloLabel("Precio (MXN)", xDer, y));
                    txtPrecioEdicion = CrearValorTextBox(producto.Precio.ToString(), xDer, y + 45);
                    tarjetaProducto.Controls.Add(txtPrecioEdicion);

                    y += espacio;
                    tarjetaProducto.Controls.Add(CrearTituloLabel("Descripción", xIzq, y));
                    txtDescripcionEdicion = CrearValorTextBox(producto.Descripcion, xIzq, y + 45);
                    tarjetaProducto.Controls.Add(txtDescripcionEdicion);

                    tarjetaProducto.Controls.Add(CrearTituloLabel("Cantidad", xDer, y));
                    txtCantidadEdicion = CrearValorTextBox(producto.Cantidad.ToString(), xDer, y + 45);
                    tarjetaProducto.Controls.Add(txtCantidadEdicion);

                    // Solo habilitar campos si está en modo edición
                    if (modoEdicion)
                    {
                        txtNombreEdicion.Enabled = true;
                        txtPrecioEdicion.Enabled = true;
                        txtDescripcionEdicion.Enabled = true;
                        txtCantidadEdicion.Enabled = true;

                        // Mostrar botones
                        guardarBtn.Visible = true;
                        cancelarBtn.Visible = true;
                        editarBtn.Visible = false;
                    }

                    tarjetaProducto.Controls.Add(guardarBtn);
                    tarjetaProducto.Controls.Add(cancelarBtn);

                    this.Controls.Remove(contenidoPanel);
                    contenidoPanel = tarjetaProducto;
                    contenidoPanel.Refresh();
                    contenidoPanel.Visible = true;
                    this.Controls.Add(contenidoPanel);
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

        private TextBox CrearValorTextBox(string texto, int x, int y)
        {
            return new TextBox
            {
                Text = texto,
                Location = new Point(x, y),
                Width = 400,
                AutoSize = true,
                Font = new Font("Futura", 18, FontStyle.Regular),
                ForeColor = Color.Black,
                Enabled = false
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
        /// <summary>
        /// ////////////////////////////////////////////////////////////////7777
        /// </summary>
        private void MostrarPanelAgregar()
        {

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
                Font = new Font("Arial", 10, FontStyle.Bold),
                Size = new Size(100, 40),
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
                        Id_producto = p.IdProducto,
                        Cantidad = p.Cantidad,
                        Nom_producto = p.Nombre,
                        Descripción = p.Descripcion,
                        Precio = $"${p.Precio:N2}"
                    }).ToList();

                    contenidoPanel.Visible = false;
                }
                else
                {
                    MessageBox.Show("Precio o cantidad inválidos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };

            Button cancelarBtn = new Button
            {
                Text = "Cancelar",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Size = new Size(100, 40),
                ForeColor = Color.White,
                BackColor = Color.Red,
                Location = new Point(xIzq + 750, y + 50)
            };


            btnGuardar.BringToFront();
            cancelarBtn.BringToFront();
            agregarProducto.Controls.Add(btnGuardar);
            agregarProducto.Controls.Add(cancelarBtn);

            this.Controls.Remove(contenidoPanel);
            contenidoPanel = agregarProducto;
            contenidoPanel.Refresh();
            contenidoPanel.Visible = true;
            this.Controls.Add(contenidoPanel);
        }

    }
}
