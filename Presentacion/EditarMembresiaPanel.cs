using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Entidad;
using Negocio;

//permite visualizar, editar y guardar precios de distintos tipos de
//membresías (Día, Semanal, Mensual). Usa una tabla (DataGridView) y
//botones para realizar estas acciones.

namespace Presentacion
{
    //Clase: EditarMembresiaPanel hereda de Panel.
    public class EditarMembresiaPanel : Panel
    {
        private DataGridView tablaMembresias;
        private Button editarBtn;
        private Button guardarBtn;
        private Button cancelarBtn;

        MembresiaServicio crud = new MembresiaServicio();
        
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
                Font = new Font("Arial Black", 20, FontStyle.Bold),
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(460, 100)
            };


            // Tabla
            tablaMembresias = new DataGridView
            {
                Location = new Point(250, 250),
                Size = new Size(800, 173),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing,
                ColumnHeadersHeight = 50,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,  // <<< IMPORTANTE
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
                }
            };
           

            // Hacer filas más altas
            tablaMembresias.RowTemplate.Height = 40;

            tablaMembresias.Columns.Add("Id", "Id");

            // Columna de precio con formato decimal
            tablaMembresias.Columns.Add("Membresía", "Membresía");


            DataGridViewTextBoxColumn precioCol = new DataGridViewTextBoxColumn
            {
                Name = "Precio",
                HeaderText = "Precio",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2", // Mostrar con dos decimales
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            };
            tablaMembresias.Columns.Add(precioCol);
            //consultar desde la base de datos los tipos de membrecia
            List<Membresia> membresias = crud.ObtenerMembresias();

            foreach (var m in membresias)
            {
                tablaMembresias.Rows.Add(m.Id, m.Tipo, m.Precio);
            }


            tablaMembresias.EditingControlShowing += (s, e) =>
            {
                if (tablaMembresias.CurrentCell.ColumnIndex == 1) // Columna "Precio"
                {
                    TextBox tb = e.Control as TextBox;
                    if (tb != null)
                    {
                        tb.KeyPress -= SoloDecimal;
                        tb.KeyPress += SoloDecimal;
                    }
                }
            };

            // Botón editar
            editarBtn = new Button
            {
                Text = "editar",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Location = new Point(850, 500),
                Size = new Size(100, 40),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            editarBtn.Click += (s, e) =>
            {
                tablaMembresias.ReadOnly = false;
                tablaMembresias.Columns["Membresía"].ReadOnly = true;

                guardarBtn.Visible = true;
                cancelarBtn.Visible = true;
                editarBtn.Visible = false;
            };

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
                tablaMembresias.ReadOnly = true;
                editarBtn.Visible = true;
                guardarBtn.Visible = false;
                cancelarBtn.Visible = false;
                if (tablaMembresias.CurrentRow != null)
                {
                    int fila = tablaMembresias.CurrentCell.RowIndex;

                    Membresia m = new Membresia
                    {
                        Id = Convert.ToInt32(tablaMembresias.Rows[fila].Cells["Id"].Value),
                        Tipo = tablaMembresias.Rows[fila].Cells["Membresía"].Value.ToString(),
                        Precio = Convert.ToDecimal(tablaMembresias.Rows[fila].Cells["Precio"].Value)
                    };

                    crud.EditarMembresia(m);
                }
                else
                {
                    MessageBox.Show("No se ha seleccionado ninguna fila.");
                }
            };

            // Botón cancelar
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
                tablaMembresias.ReadOnly = true;
                editarBtn.Visible = true;
                guardarBtn.Visible = false;
                cancelarBtn.Visible = false;
                MessageBox.Show("Cambios cancelados.");
            };

            // Agregar controles al panel
            this.Controls.Add(titulo);
            this.Controls.Add(tablaMembresias);
            this.Controls.Add(editarBtn);
            this.Controls.Add(guardarBtn);
            this.Controls.Add(cancelarBtn);
        }

        private void SoloDecimal(object sender, KeyPressEventArgs e)
        {
            TextBox tb = sender as TextBox;

            // Permitir solo números, control keys y un solo punto decimal
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.' || (tb != null && tb.Text.Contains('.'))))
            {
                e.Handled = true;
            }
        }
    }
}

