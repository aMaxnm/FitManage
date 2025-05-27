using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Entidad;
using Negocio;


namespace Presentacion
{
    internal class PanelRegistroVenta : Panel
    {
        private List<Producto> productos;
        List<Producto> carrito = new List<Producto>();
        private DataGridView dgvProductos, dgvCarrito;
        private ProductoServicio productoServicio = new ProductoServicio();
        private Label totalLbl;
        public PanelRegistroVenta()
        {
            productos = productoServicio.ObtenerTodos();
            InicializarComponentes();
        }
        private void InicializarComponentes()
        {
            this.Size = new Size(2000, 1010);
            this.BackColor = Color.WhiteSmoke;

            //Labels para el panel
            Label tituloLbl, carritoLbl, carritoImagen;
            //Botones
            Button eliminarBtn, cobrarBtn;

            //Configuracion de Labels
            tituloLbl = new Label();
            tituloLbl.Text = "VENTA";
            tituloLbl.Font = new Font("Race Sport", 38);
            tituloLbl.ForeColor = Color.Black;
            tituloLbl.Location = new Point(750, 40);
            tituloLbl.AutoSize = true;

            carritoLbl = new Label();
            carritoLbl.Text = "CARRITO";
            carritoLbl.Font = new Font("Race Sport", 30);
            carritoLbl.ForeColor = Color.Black;
            carritoLbl.Location = new Point(350, 450);
            carritoLbl.AutoSize = true;       

            totalLbl = new Label();
            totalLbl.Text = "Total: $0.00";
            totalLbl.Font = new Font("Tahoma", 20, FontStyle.Bold);
            totalLbl.ForeColor = Color.Black;
            totalLbl.Location = new Point(650, 730);
            totalLbl.AutoSize = true;

            //Configuracion de Botones
            eliminarBtn = new Button();
            eliminarBtn.Text = "Eliminar";
            eliminarBtn.Font = new Font("Race Sport", 15, FontStyle.Bold);
            eliminarBtn.ForeColor = Color.White;
            eliminarBtn.Location = new Point(400, 570);
            eliminarBtn.BackColor = Color.Gray;
            eliminarBtn.FlatStyle = FlatStyle.Flat;
            eliminarBtn.AutoSize = true;
            eliminarBtn.Click += (s, e) =>
            {
                // Devolver el stock al inventario
                foreach (var productoEnCarrito in carrito)
                {
                    var productoOriginal = productos.FirstOrDefault(p => p.Nombre == productoEnCarrito.Nombre);
                    if (productoOriginal != null)
                    {
                        productoOriginal.Cantidad += productoEnCarrito.Cantidad;
                    }
                }

                // Vaciar el carrito
                carrito.Clear();

                // Refrescar dgvCarrito
                dgvCarrito.DataSource = null;
                dgvCarrito.DataSource = carrito
                    .Select(p => new
                    {
                        p.IdProducto,
                        p.Nombre,
                        p.Precio,
                        p.Cantidad
                    }).ToList();

                // Refrescar dgvProductos
                dgvProductos.DataSource = null;
                dgvProductos.DataSource = productos
                    .OrderBy(p => p.Nombre)
                    .Select(p => new
                    {
                        p.IdProducto,
                        p.Nombre,
                        p.Descripcion,
                        p.Precio,
                        p.Cantidad
                    }).ToList();

                // Actualizar el total
                ActualizarTotal();
            };

            cobrarBtn = new Button();
            cobrarBtn.BackgroundImage = Image.FromFile("Recursos/cobrar.png");
            cobrarBtn.Location = new Point(435, 630);
            cobrarBtn.Size = new Size(90,70);
            cobrarBtn.BackgroundImageLayout = ImageLayout.Stretch;
            cobrarBtn.BackColor = Color.Gray;
            cobrarBtn.FlatStyle = FlatStyle.Flat;
            cobrarBtn.ImageAlign = ContentAlignment.MiddleCenter;
            cobrarBtn.Cursor = Cursors.Hand;
            cobrarBtn.FlatAppearance.BorderSize = 0;
            cobrarBtn.Click += (s, e) =>
            {
                if (carrito.Any()) {
                    MessageBox.Show("Recuerde cobrar.\n" + totalLbl.Text, "Cobrar", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Codigo de archivo .txt
                    // Ruta a la carpeta (solo la carpeta)
                    string carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\ArchivoTxt");

                    // Asegura que la carpeta exista
                    Directory.CreateDirectory(carpeta);

                    // Ruta completa al archivo (archivo .txt dentro de la carpeta)
                    string rutaArchivo = Path.Combine(carpeta, "ventas.txt");

                    // Construir contenido del ticket
                    string contenido = $"--- TICKET DE COMPRA ---\nFecha: {DateTime.Now}\n\n";

                    foreach (var producto in carrito)
                    {
                        contenido += $"-Producto: {producto.Nombre}\n";
                        contenido += $"Cantidad: {producto.Cantidad}\n";
                        contenido += $"Precio unitario: ${producto.Precio}\n";
                        contenido += $"Subtotal: ${producto.Precio * producto.Cantidad}\n\n";
                    }

                    decimal total = carrito.Sum(p => p.Precio * p.Cantidad);
                    contenido += $"TOTAL: ${total}\n";
                    contenido += "--------------------------\n";

                    // Si el archivo existe, se agrega contenido; si no, se crea
                    File.AppendAllText(rutaArchivo, contenido);

                    // Actualizar el stock de los productos
                    productoServicio.EliminarStock(carrito);
                    productos = productoServicio.ObtenerTodos();

                    // Vaciar el carrito
                    carrito.Clear();
                    // Refrescar dgvCarrito
                    dgvCarrito.DataSource = null;
                    dgvCarrito.DataSource = carrito
                        .Select(p => new
                        {
                            p.IdProducto,
                            p.Nombre,
                            p.Precio,
                            p.Cantidad
                        }).ToList();
                }
                else
                {
                    MessageBox.Show("No hay productos en el carrito.", "Cobrar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            //Configuracion de DataGridView para productos
            dgvProductos = new DataGridView();
            dgvProductos.Location = new Point(350, 130);
            dgvProductos.Size = new Size(1000, 250);
            dgvProductos.ScrollBars = ScrollBars.Vertical;
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
                    p.IdProducto,
                    p.Nombre,
                    p.Descripcion,
                    p.Precio,
                    p.Cantidad
                }).ToList();
            dgvProductos.CellClick += DgvProductos_CellClick;

            dgvCarrito = new DataGridView();
            dgvCarrito.Location = new Point(650, 460);
            dgvCarrito.Size = new Size(650, 260);
            dgvCarrito.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCarrito.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCarrito.ReadOnly = true;
            dgvCarrito.DefaultCellStyle.Font = new Font("Futura", 16);
            dgvCarrito.ColumnHeadersDefaultCellStyle.Font = new Font("Futura", 18, FontStyle.Bold);
            dgvCarrito.RowTemplate.Height = 50;
            dgvCarrito.ColumnHeadersHeight = 40;
            dgvCarrito.DataSource = carrito.ToList().Select(p => new
            {
                p.IdProducto,
                p.Nombre,
                p.Precio,
                p.Cantidad
            }).ToList();

            this.Controls.Add(tituloLbl);
            this.Controls.Add(carritoLbl);
            this.Controls.Add(totalLbl);
            this.Controls.Add(eliminarBtn);
            this.Controls.Add(cobrarBtn);
            this.Controls.Add(dgvProductos);
            this.Controls.Add(dgvCarrito);
        }
        private void DgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Obtener el producto seleccionado del DataGridView
                var fila = dgvProductos.Rows[e.RowIndex];
                string nombre = fila.Cells["Nombre"].Value.ToString();
                decimal precio = Convert.ToDecimal(fila.Cells["Precio"].Value);
                int id = Convert.ToInt32(fila.Cells["IdProducto"].Value);

                // Buscar el producto real en la lista de productos
                var productoEnStock = productos.FirstOrDefault(p => p.Nombre == nombre);

                // Verificar si hay stock
                if (productoEnStock != null && productoEnStock.Cantidad > 0)
                {
                    // Disminuir la cantidad del producto en stock
                    productoEnStock.Cantidad--;

                    // Verificar si ya está en el carrito
                    var productoEnCarrito = carrito.FirstOrDefault(p => p.Nombre == nombre);

                    if (productoEnCarrito != null)
                    {
                        productoEnCarrito.Cantidad++;
                    }
                    else
                    {
                        carrito.Add(new Producto
                        {
                            IdProducto = id,
                            Nombre = nombre,
                            Precio = precio,
                            Cantidad = 1
                        });
                    }

                    // Refrescar dgvProductos
                    dgvProductos.DataSource = null;
                    dgvProductos.DataSource = productos
                        .OrderBy(p => p.Nombre)
                        .Select(p => new
                        {
                            p.IdProducto,
                            p.Nombre,
                            p.Descripcion,
                            p.Precio,
                            p.Cantidad
                        }).ToList();

                    // Refrescar dgvCarrito
                    dgvCarrito.DataSource = null;
                    dgvCarrito.DataSource = carrito.Select(p => new
                        {
                            p.IdProducto,
                            p.Nombre,
                            p.Precio,
                            p.Cantidad
                        }).ToList();
                    
                    ActualizarTotal();
                }
                else
                {
                    MessageBox.Show("No hay más stock de este producto.");
                }
            }
        }
        private void ActualizarTotal()
        {
            decimal total = carrito.Sum(p => p.Precio * p.Cantidad);
            totalLbl.Text = $"Total: ${total:F2}";
        }

    }
}
