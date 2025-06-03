using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Entidad;
using Negocio;

namespace Presentacion
{
    public class EditarMembresiaPanel : Panel
    {
        private DataGridView tablaMembresias;
        private Button editarBtn;
        private Button guardarBtn;
        private Button cancelarBtn;

        // Tarjeta detalle
        private Panel tarjetaDetalle;
        private Label lblTipo;
        private TextBox txtPrecio;
        private Button btnEditarTarjeta;
        private Button btnGuardarTarjeta;
        private Button btnCancelarTarjeta;

        MembresiaServicio crud = new MembresiaServicio();

        private Membresia membresiaSeleccionada;

        public EditarMembresiaPanel() : base()
        {
            this.BackColor = Color.White;
            this.Location = new Point(208, 10);
            this.Size = new Size(1320, 776);
            this.Visible = true;

            InicializarComponentes();
        }

        private void InicializarComponentes()
        {
            // Título
            Label titulo = new Label
            {
                Text = "TIPOS DE MEMBRESÍAS",
                Font = new Font("Race sport", 40, FontStyle.Bold),
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(250, 50)
            };

            // Tabla
            tablaMembresias = new DataGridView
            {
                Location = new Point(250, 150),
                Size = new Size(800, 173),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing,
                ColumnHeadersHeight = 50,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Arial Black", 16, FontStyle.Bold),
                    BackColor = Color.Gray,
                    ForeColor = Color.White
                },
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Arial", 14),
                    BackColor = Color.White
                },
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };

            tablaMembresias.RowTemplate.Height = 40;

            tablaMembresias.Columns.Add("Id_membresia", "Id_membresia");
            tablaMembresias.Columns.Add("Membresía", "Membresía");

            Label lblSelecciona = new Label();
            lblSelecciona.Text = "Selecciona una celda para editar";
            lblSelecciona.Font = new Font("Race Sport", 12, FontStyle.Bold);
            lblSelecciona.ForeColor = Color.Black;
            lblSelecciona.AutoSize = true;

            // Posición justo debajo del DataGridView (ajústala si es necesario)
            lblSelecciona.Location = new Point(tablaMembresias.Location.X, tablaMembresias.Location.Y + tablaMembresias.Height + 10);

            // Agrega al formulario o al contenedor donde está la tabla
            this.Controls.Add(lblSelecciona); 

            DataGridViewTextBoxColumn precioCol = new DataGridViewTextBoxColumn
            {
                Name = "Precio",
                HeaderText = "Precio",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            };
            tablaMembresias.Columns.Add(precioCol);

            // Cargar datos
            List<Membresia> membresias = crud.ObtenerMembresias();

            foreach (var m in membresias)
            {
                tablaMembresias.Rows.Add(m.Id_membresia, m.Tipo, m.Precio);
            }

            // Evento para permitir solo números decimales en edición
            tablaMembresias.EditingControlShowing += (s, e) =>
            {
                if (tablaMembresias.CurrentCell.ColumnIndex == 2) // Columna "Precio"
                {
                    TextBox tb = e.Control as TextBox;
                    if (tb != null)
                    {
                        tb.KeyPress -= SoloDecimal;
                        tb.KeyPress += SoloDecimal;
                    }
                }
            };

            // Botones para modo tabla
            editarBtn = new Button
            {
                Text = "Editar",
                Font = new Font("Race Sport", 12, FontStyle.Bold),
                Location = new Point(850, 350),
                Size = new Size(150, 40),
                BackColor = Color.Orange,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Enabled = false  // Inicialmente deshabilitado porque no hay selección
            };

            editarBtn.Click += (s, e) =>
            {
                if (tablaMembresias.CurrentRow == null)
                {
                    MessageBox.Show("Debe seleccionar una fila para editar el precio.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                tablaMembresias.ReadOnly = false;
                tablaMembresias.Columns["Membresía"].ReadOnly = true; // Solo editar precio

                guardarBtn.Visible = true;
                cancelarBtn.Visible = true;
                editarBtn.Visible = false;
            };

            guardarBtn = new Button
            {
                Text = "Guardar",
                Font = new Font("Race Sport", 12, FontStyle.Bold),
                Location = new Point(475, 600),
                Size = new Size(150, 40),
                BackColor = Color.ForestGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Visible = false
            };

            guardarBtn.Click += (s, e) =>
            {
                if (membresiaSeleccionada == null)
                    return;

                if (decimal.TryParse(txtPrecio.Text, out decimal nuevoPrecio))
                {
                    membresiaSeleccionada.Precio = nuevoPrecio;
                    crud.EditarMembresia(membresiaSeleccionada);

                    // Actualizar tabla
                    foreach (DataGridViewRow row in tablaMembresias.Rows)
                    {
                        if ((int)row.Cells["Id_membresia"].Value == membresiaSeleccionada.Id_membresia)
                        {
                            row.Cells["Precio"].Value = nuevoPrecio;
                            break;
                        }
                    }

                    txtPrecio.Enabled = false;
                    guardarBtn.Visible = false;
                    cancelarBtn.Visible = false;
                    editarBtn.Visible = true;

                    MessageBox.Show("Precio actualizado correctamente.");
                }
                else
                {
                    MessageBox.Show("Ingrese un precio válido.");
                }
            };

            cancelarBtn = new Button
            {
                Text = "Cancelar",
                Font = new Font("Race Sport", 12, FontStyle.Bold),
                Location = new Point(700, 600),
                Size = new Size(150, 40),
                BackColor = Color.DarkRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Visible = false
            };

            cancelarBtn.Click += (s, e) =>
            {
                if (membresiaSeleccionada != null)
                    txtPrecio.Text = membresiaSeleccionada.Precio.ToString("N2");

                txtPrecio.Enabled = false;
                guardarBtn.Visible = false;
                cancelarBtn.Visible = false;
                editarBtn.Visible = true;

                MessageBox.Show("Edición cancelada.");
            };

            // Tarjeta detalle para mostrar datos de la membresía seleccionada
            tarjetaDetalle = new Panel
            {
                Location = new Point(tablaMembresias.Left, tablaMembresias.Bottom + 100), // Debajo de la tabla con un pequeño margen
                Size = new Size(tablaMembresias.Width, 250), // Mismo ancho que la tabla
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false,
                BackColor = Color.White,
            };

            Label lblTituloTipo = new Label
            {
                Text = "Tipo",
                Location = new Point(175, 25),
                AutoSize = true,
                Font = new Font("Tahoma", 25, FontStyle.Bold)
            };

            lblTipo = new Label
            {
                Location = new Point(170, 75),
                Size = new Size(180, 30),
                Font = new Font("Tahoma", 20),
            };

            Label lblTituloPrecio = new Label
            {
                Text = "Precio",
                Location = new Point(500, 20),
                AutoSize = true,
                Font = new Font("Tahoma", 25, FontStyle.Bold)
            };

            txtPrecio = new TextBox
            {
                Location = new Point(470, 70),
                Size = new Size(180, 30),
                Font = new Font("Tahoma", 20),
                Enabled = false
            };


            editarBtn.Click += (s, e) =>
            {
                txtPrecio.Enabled = true;
                guardarBtn.Visible = true;
                cancelarBtn.Visible = true;
                editarBtn.Visible = false;
            };



            // Evento para selección completa de fila
            tablaMembresias.SelectionChanged += (s, e) =>
            {
                bool filaSeleccionada = tablaMembresias.CurrentRow != null;

                editarBtn.Enabled = filaSeleccionada;

                if (filaSeleccionada && tablaMembresias.CurrentRow.Index >= 0)
                {
                    int fila = tablaMembresias.CurrentRow.Index;

                    membresiaSeleccionada = new Membresia
                    {
                        Id_membresia = Convert.ToInt32(tablaMembresias.Rows[fila].Cells["Id_membresia"].Value),
                        Tipo = tablaMembresias.Rows[fila].Cells["Membresía"].Value.ToString(),
                        Precio = Convert.ToDecimal(tablaMembresias.Rows[fila].Cells["Precio"].Value)
                    };

                    lblTipo.Text = membresiaSeleccionada.Tipo;
                    txtPrecio.Text = membresiaSeleccionada.Precio.ToString("N2");

                    tarjetaDetalle.Visible = true;

                    txtPrecio.Enabled = false;
                    editarBtn.Visible = true;
                    guardarBtn.Visible = false;
                    cancelarBtn.Visible = false;
                }
                else
                {
                    tarjetaDetalle.Visible = false;
                    membresiaSeleccionada = null;
                }
            };

            // Agregar controles
            this.Controls.Add(titulo);
            this.Controls.Add(tablaMembresias);
            this.Controls.Add(editarBtn);
            this.Controls.Add(guardarBtn);
            this.Controls.Add(cancelarBtn);

            tarjetaDetalle.Controls.Add(lblTituloTipo);
            tarjetaDetalle.Controls.Add(lblTipo);
            tarjetaDetalle.Controls.Add(lblTituloPrecio);
            tarjetaDetalle.Controls.Add(txtPrecio);
            this.Controls.Add(tarjetaDetalle);
        }

        private void SoloDecimal(object sender, KeyPressEventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.' || (tb != null && tb.Text.Contains('.'))))
            {
                e.Handled = true;
            }
        }
    }
}
