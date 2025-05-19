using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidad;
using Negocio;

namespace Presentacion
{
    internal class ConsultaMiembro : Panel
    {
        public List<Miembro> listaMiembros;
        public Panel tarjetaMiembro;
        public Label tituloMiembroLbl;
        public DataGridView listaMiembro;
        MiembroServicio MiembroServicio = new MiembroServicio();
        public ConsultaMiembro() : base()
        {
            listaMiembros = MiembroServicio.ObtenerTodos();
            InicializarComponentes();
            this.BackColor = Color.WhiteSmoke;
        }
        private void InicializarComponentes()
        {
            this.Text = "Consulta de Miembros";
            this.Size = new Size(2000, 1010);

            // Título
            tituloMiembroLbl = new Label();
            tituloMiembroLbl.Text = "MIEMBROS";
            tituloMiembroLbl.Font = new Font("Race Sport", 50, FontStyle.Bold);
            this.Controls.Add(tituloMiembroLbl);
            tituloMiembroLbl.Location = new Point(600, 10);
            tituloMiembroLbl.AutoSize = true;
            tituloMiembroLbl.ForeColor = Color.Black;       

            // Lista de miembros
            Dictionary<int, string> memDict = new Dictionary<int, string>
            {
                {1003, "Dia"},
                {1002, "Semanal"},
                {1001, "Mensual"}
            };
            listaMiembro = new DataGridView();
            listaMiembro.DataSource = listaMiembros.Select(m => new
            {
                Nombre = m.Nombres,
                Paterno = m.ApellidoPaterno,
                Materno = m.ApellidoMaterno,
                Membresía = memDict[m.IdMembresia],
            }).ToList();
            listaMiembro.Location = new Point(350, 130);
            listaMiembro.Size = new Size(1000, 200);
            listaMiembro.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            listaMiembro.ReadOnly = true;
            listaMiembro.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            listaMiembro.CellClick += ListaMiembro_CellClick;
            listaMiembro.DefaultCellStyle.Font = new Font("Futura", 18);
            listaMiembro.ColumnHeadersDefaultCellStyle.Font = new Font("Futura", 18, FontStyle.Bold);
            listaMiembro.RowTemplate.Height = 50;
            listaMiembro.ColumnHeadersHeight = 40;
            this.Controls.Add(listaMiembro);



            // Tarjeta detalle
            tarjetaMiembro = new Panel();
            tarjetaMiembro.Location = new Point(350, 350);
            tarjetaMiembro.Size = new Size(1000, 400);
            tarjetaMiembro.BorderStyle = BorderStyle.FixedSingle;
            tarjetaMiembro.Visible = false;
            this.Controls.Add(tarjetaMiembro);


            //Datos Tarjeta
        }
        public void CargarDatos()
        {
            // Aquí recargas los datos de la base de datos y los asignas al DataGridView
            var miembros = MiembroServicio.ObtenerTodos(); // Ejemplo
            Dictionary<int, string> memDict = new Dictionary<int, string>
            {
                {1003, "Dia"},
                {1002, "Semanal"},
                {1001, "Mensual"}
            };
            listaMiembro.DataSource = listaMiembros.Select(m => new
            {
                Nombre = m.Nombres,
                Paterno = m.ApellidoPaterno,
                Materno = m.ApellidoMaterno,
                Membresía = memDict[m.IdMembresia],
            }).ToList();
        }
        private void ListaMiembro_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string nombre = listaMiembro.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                var miembro = listaMiembros.FirstOrDefault(m => m.Nombres == nombre);

                if (miembro != null)
                {
                    tarjetaMiembro.Controls.Clear();

                    // ----- FOTO -----
                    PictureBox foto = new PictureBox();
                    foto.Size = new Size(400, 400);
                    foto.Location = new Point(20, 20);
                    foto.SizeMode = PictureBoxSizeMode.StretchImage;


                    // Botón editar (como Label clickeable)
                    Label editarLbl = new Label();
                    editarLbl.Text = "Editar";
                    editarLbl.Font = new Font("Futura", 22, FontStyle.Underline);
                    editarLbl.ForeColor = Color.Blue;
                    editarLbl.AutoSize = true;
                    editarLbl.Location = new Point(1100, 20);
                    editarLbl.Cursor = Cursors.Hand;
                    editarLbl.Click += (s, ev) => ActivarModoEdicion(miembro);
                    tarjetaMiembro.Controls.Add(editarLbl);


                    if (miembro.Fotografia != null)
                    {
                        using (var ms = new System.IO.MemoryStream(miembro.Fotografia))
                        {
                            foto.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        foto.Image = null;
                    }
                    tarjetaMiembro.Controls.Add(foto);

                    // ----- DATOS -----
                    int baseX = 450; // posición a la derecha de la imagen
                    int baseY = 20;
                    int salto = 65;

                    tarjetaMiembro.Controls.Add(CrearLabel("ID:", baseX, baseY, 55));
                    tarjetaMiembro.Controls.Add(CrearLabel(miembro.IdMiembro.ToString(), baseX + 55, baseY, 300));

                    tarjetaMiembro.Controls.Add(CrearLabel("Nombre(s):", baseX, baseY += salto, 180));
                    tarjetaMiembro.Controls.Add(CrearLabel(miembro.Nombres, baseX + 175, baseY, 300));

                    tarjetaMiembro.Controls.Add(CrearLabel("Apellido Paterno:", baseX, baseY += salto, 265));
                    tarjetaMiembro.Controls.Add(CrearLabel(miembro.ApellidoPaterno, baseX + 265, baseY, 300));

                    tarjetaMiembro.Controls.Add(CrearLabel("Apellido Materno:", baseX, baseY += salto, 270));
                    tarjetaMiembro.Controls.Add(CrearLabel(miembro.ApellidoMaterno, baseX + 265, baseY, 300));

                    tarjetaMiembro.Controls.Add(CrearLabel("Teléfono:", baseX, baseY += salto, 148));
                    tarjetaMiembro.Controls.Add(CrearLabel(miembro.NumeroTelefono, baseX + 148, baseY, 300));

                    tarjetaMiembro.Controls.Add(CrearLabel("Fecha Nacimiento:", baseX, baseY += salto, 280));
                    tarjetaMiembro.Controls.Add(CrearLabel(miembro.FechaNacimiento.ToShortDateString(), baseX + 280, baseY, 300));


                    tarjetaMiembro.Visible = true;
                }
            }
        }
        //Edicion de Datos
        private void ActivarModoEdicion(Miembro miembro)
        {
            tarjetaMiembro.Controls.Clear();
            PictureBox foto = new PictureBox();
            foto.Size = new Size(500, 500);
            foto.Location = new Point(20, 20);
            foto.SizeMode = PictureBoxSizeMode.StretchImage;

            if (miembro.Fotografia != null)
            {
                using (var ms = new System.IO.MemoryStream(miembro.Fotografia))
                {
                    foto.Image = Image.FromStream(ms);
                }
            }

            tarjetaMiembro.Controls.Add(foto);

            int baseX = 580;
            int baseY = 20;
            int salto = 90;

            tarjetaMiembro.Controls.Add(CrearLabel("ID:", baseX, baseY, 55));
            tarjetaMiembro.Controls.Add(CrearLabel(miembro.IdMiembro.ToString(), baseX + 55, baseY, 300));

            // TextBoxes editables
            tarjetaMiembro.Controls.Add(CrearLabel("Nombre(s):", baseX, baseY += salto, 180));
            TextBox nomTxt = CrearTextBox(miembro.Nombres, baseX + 175, baseY, 300);
            tarjetaMiembro.Controls.Add(nomTxt);

            tarjetaMiembro.Controls.Add(CrearLabel("Apellido Paterno:", baseX, baseY += salto, 265));
            TextBox apePaternoTxt = CrearTextBox(miembro.ApellidoPaterno, baseX + 265, baseY, 300);
            tarjetaMiembro.Controls.Add(apePaternoTxt);

            tarjetaMiembro.Controls.Add(CrearLabel("Apellido Materno:", baseX, baseY += salto, 270));
            TextBox apeMaternoTxt = CrearTextBox(miembro.ApellidoMaterno, baseX + 265, baseY, 300);
            tarjetaMiembro.Controls.Add(apeMaternoTxt);

            tarjetaMiembro.Controls.Add(CrearLabel("Teléfono:", baseX, baseY += salto, 148));
            TextBox telefonoTxt = CrearTextBox(miembro.NumeroTelefono, baseX + 148, baseY, 300);
            tarjetaMiembro.Controls.Add(telefonoTxt);

            tarjetaMiembro.Controls.Add(CrearLabel("Fecha Nacimiento:", baseX, baseY += salto, 280));
            tarjetaMiembro.Controls.Add(CrearLabel(miembro.FechaNacimiento.ToShortDateString(), baseX + 280, baseY, 300));

            // Botón Guardar
            Button btnGuardar = new Button();
            btnGuardar.Text = "Guardar";
            btnGuardar.Font = new Font("Race Sport", 16, FontStyle.Bold);
            btnGuardar.Size = new Size(180, 50);
            btnGuardar.Location = new Point(1100, 20);
            btnGuardar.BackColor = Color.ForestGreen;
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Click += (s, ev) =>
            {
                if (string.IsNullOrWhiteSpace(nomTxt.Text) || string.IsNullOrWhiteSpace(apePaternoTxt.Text) ||
                    string.IsNullOrWhiteSpace(apeMaternoTxt.Text) || string.IsNullOrWhiteSpace(telefonoTxt.Text))
                {
                    MessageBox.Show("Por favor, rellene todos los campos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                miembro.Nombres = nomTxt.Text.Trim();
                miembro.ApellidoPaterno = apePaternoTxt.Text.Trim();
                miembro.ApellidoMaterno = apeMaternoTxt.Text.Trim();
                miembro.NumeroTelefono = telefonoTxt.Text.Trim();

                MiembroServicio.Actualizar(miembro); // Asume que tienes un método Actualizar en MiembroServicio
                MessageBox.Show("Datos actualizados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Recargar lista
                listaMiembros = MiembroServicio.ObtenerTodos();
                listaMiembro.DataSource = listaMiembros.Select(m => new
                {
                    Nombre = m.Nombres,
                    Paterno = m.ApellidoPaterno,
                    Materno = m.ApellidoMaterno,
                    Membresía = m.IdMembresia, // Puedes mapearlo si es necesario
                }).ToList();

                tarjetaMiembro.Visible = false;
            };

            tarjetaMiembro.Controls.Add(btnGuardar);
        }
        private TextBox CrearTextBox(string texto, int x, int y, int width)
        {
            return new TextBox
            {
                Text = texto,
                Location = new Point(x, y),
                Size = new Size(width, 30),
                Font = new Font("Futura", 18, FontStyle.Regular)
            };
        }
        private Label CrearLabel(string texto, int x, int y, int width)
        {
            return new Label
            {
                Text = texto,
                Location = new Point(x, y),
                Size = new Size(width, 30),
                Font = new Font("Futura", 20, FontStyle.Bold)
            };
        }
    }
}