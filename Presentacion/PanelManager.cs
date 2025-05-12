using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Negocio;
using Presentacion;
using Entidad;

namespace Presentación
{
    internal class PanelManager
    {
        private Panel panelConsulta;
        private readonly Panel mainPanel;
        public static PictureBox fotoMiembroPct = new PictureBox();
        public static ComboBox dispositivosCombo = new ComboBox();
        Button abrirCamara = new Button();
        ComboBox membresiaCombo = new ComboBox();
        DateTimePicker fechaPicker = new DateTimePicker();
        ErrorProvider errorProvider = new ErrorProvider(); // se utiliza para las validaciones de los campos de texto
        Label fechaLbl = new Label();
        public static TextBox nombreTxt, apePaternoTxt, apeMaternoTxt, telefonoTxt;
        Button registrarBtn = new Button();

        public PanelManager(Panel mainPanel)
        {
            this.mainPanel = mainPanel;
            CargarMembresias();
        }

        public Panel CrearPanel(string nombre, Color colorFondo)
        {
            Panel nuevoPanel = new Panel();
            nuevoPanel.Name = nombre;
            nuevoPanel.Location = new Point(208, 10);
            nuevoPanel.Size = new Size(1320, 776);
            nuevoPanel.BackColor = colorFondo;
            nuevoPanel.Visible = false;
           

            //Barra gris oscuro sobre el formulario
            Panel barraGris = new Panel();
            barraGris.BackColor = Color.Gray;
            barraGris.Dock = DockStyle.Top;
            nuevoPanel.Controls.Add(barraGris);

            //Labels necesarias para el formulario
            Label tituloLbl, subtituloLbl, nombreLbl, apePaternoLbl, apeMaternoLbl, telefonoLbl, membresiaLbl, fotoLbl;
            Button tomarBtn, retomarBtn, importarBtn, regresarBtn,flechaBtn, cobrarBtn;

            //Labels dentro de la barra gris oscuro
            tituloLbl = new Label();
            tituloLbl.Text = "Registrar nuevo cliente";
            tituloLbl.Font = new Font("Race Sport", 35);
            tituloLbl.ForeColor = Color.Black;
            tituloLbl.Location = new Point(230, 10);
            tituloLbl.AutoSize = true;

            subtituloLbl = new Label();
            subtituloLbl.Text = "Ingrese los datos del cliente nuevo.";
            subtituloLbl.Font = new Font("Tahoma", 16);
            subtituloLbl.ForeColor = Color.Black;
            subtituloLbl.Location = new Point(460, 72);
            subtituloLbl.AutoSize = true;
            barraGris.Controls.Add(tituloLbl);
            barraGris.Controls.Add(subtituloLbl);

            //Labels para el formulario
            nombreLbl = new Label();
            nombreLbl.Text = "Nombre(s)";
            nombreLbl.Font = new Font("Tahoma", 19, FontStyle.Bold);
            nombreLbl.ForeColor = Color.Black;
            nombreLbl.Location = new Point(35, 130);
            nombreLbl.AutoSize = true;

            apePaternoLbl = new Label();
            apePaternoLbl.Text = "Apellido Paterno";
            apePaternoLbl.Font = new Font("Tahoma", 19, FontStyle.Bold);
            apePaternoLbl.ForeColor = Color.Black;
            apePaternoLbl.Location = new Point(35, 230);
            apePaternoLbl.AutoSize = true;

            apeMaternoLbl = new Label();
            apeMaternoLbl.Text = "Apellido Materno";
            apeMaternoLbl.Font = new Font("Tahoma", 19, FontStyle.Bold);
            apeMaternoLbl.ForeColor = Color.Black;
            apeMaternoLbl.Location = new Point(35, 330);
            apeMaternoLbl.AutoSize = true;

            fechaLbl.Text = "Fecha de Nacimiento";
            fechaLbl.Font = new Font("Tahoma", 19, FontStyle.Bold);
            fechaLbl.ForeColor = Color.Black;
            fechaLbl.Location = new Point(35, 430);
            fechaLbl.AutoSize = true;

            telefonoLbl = new Label();
            telefonoLbl.Text = "Teléfono";
            telefonoLbl.Font = new Font("Tahoma", 19, FontStyle.Bold);
            telefonoLbl.ForeColor = Color.Black;
            telefonoLbl.Location = new Point(35, 530);
            telefonoLbl.AutoSize = true;

            membresiaLbl = new Label();
            membresiaLbl.Text = "Tipo de Membresía";
            membresiaLbl.Font = new Font("Tahoma", 19, FontStyle.Bold);
            membresiaLbl.ForeColor = Color.Black;
            membresiaLbl.Location = new Point(35, 630);
            membresiaLbl.AutoSize = true;

            fotoLbl = new Label();
            fotoLbl.Text = "Foto";
            fotoLbl.Font = new Font("Tahoma", 20, FontStyle.Bold);
            fotoLbl.ForeColor = Color.Black;
            fotoLbl.Location = new Point(847, 100);
            fotoLbl.AutoSize = true;

            //TextBoxs para el formulario
            nombreTxt = new TextBox();
            nombreTxt.Location = new Point(37, 162);
            nombreTxt.Size = new Size(360, 30);
            nombreTxt.Font = new Font("Tahoma", 12);
            nombreTxt.ForeColor = Color.Black;
            nombreTxt.BorderStyle = BorderStyle.FixedSingle;
            nombreTxt.CharacterCasing = CharacterCasing.Upper;
            nombreTxt.TextChanged += new EventHandler(nombreTxt_TextChanged);
            

            apePaternoTxt = new TextBox();
            apePaternoTxt.Location = new Point(37, 262);
            apePaternoTxt.Size = new Size(360, 30);
            apePaternoTxt.Font = new Font("Tahoma", 12);
            apePaternoTxt.ForeColor = Color.Black;
            apePaternoTxt.BorderStyle = BorderStyle.FixedSingle;
            apePaternoTxt.CharacterCasing = CharacterCasing.Upper;
            apePaternoTxt.TextChanged += new EventHandler(apePaternoTxt_TextChanged);

            apeMaternoTxt = new TextBox();
            apeMaternoTxt.Location = new Point(37, 362);
            apeMaternoTxt.Size = new Size(360, 30);
            apeMaternoTxt.Font = new Font("Tahoma", 12);
            apeMaternoTxt.ForeColor = Color.Black;
            apeMaternoTxt.BorderStyle = BorderStyle.FixedSingle;
            apeMaternoTxt.CharacterCasing = CharacterCasing.Upper;
            apeMaternoTxt.TextChanged += new EventHandler(apeMaternoTxt_TextChanged);

            //DatePicker para la fecha de nacimiento
            fechaPicker.Format = DateTimePickerFormat.Short;
            fechaPicker.Location = new Point(37, 462);
            fechaPicker.Width = 360;
            fechaPicker.Font = new Font("Tahoma", 12);
            fechaPicker.ForeColor = Color.Black;
            fechaPicker.ValueChanged += new EventHandler(fechaPicker_ValueChanged);

            telefonoTxt = new TextBox();
            telefonoTxt.Location = new Point(37, 562);
            telefonoTxt.Size = new Size(290, 30);
            telefonoTxt.Font = new Font("Tahoma", 12);
            telefonoTxt.ForeColor = Color.Black;
            telefonoTxt.BorderStyle = BorderStyle.FixedSingle;
            telefonoTxt.TextChanged += new EventHandler(telefonoTxt_TextChanged);

            //ComboBox para el tipo de membresía
            membresiaCombo.Location = new Point(37, 662);
            membresiaCombo.Size = new Size(290, 30);
            membresiaCombo.Font = new Font("Tahoma", 12);
            membresiaCombo.Text = "Seleccione una opción";

            //Panel para la foto
            fotoMiembroPct.Image = Image.FromFile("Recursos/placeholder.jpg");
            fotoMiembroPct.SizeMode = PictureBoxSizeMode.StretchImage;
            fotoMiembroPct.Size = new Size(300, 350);
            fotoMiembroPct.Location = new Point(750, 180);
            fotoMiembroPct.BorderStyle = BorderStyle.FixedSingle;

            //ComboBox para selección de camara
            dispositivosCombo.Location = new Point(750, 150);
            dispositivosCombo.Size = new Size(fotoMiembroPct.Width*2/3 - 5, 30);
            abrirCamara.Location = new Point(dispositivosCombo.Location.X + dispositivosCombo.Width+5, 145);
            abrirCamara.Size = new Size(fotoMiembroPct.Width/3 , 30);
            abrirCamara.BackColor = Color.White;
            abrirCamara.Text = "Seleccionar";
            Fotografia.CargarDispositivos();
            abrirCamara.Click += Fotografia.AbrirCamara_Click;

            //Botones para el formulario
            tomarBtn = new Button();
            tomarBtn.BackgroundImage = Image.FromFile("Recursos/photo.png");
            tomarBtn.BackgroundImageLayout = ImageLayout.Stretch;
            tomarBtn.BackColor = Color.Gray;
            tomarBtn.FlatStyle = FlatStyle.Flat;
            tomarBtn.ImageAlign = ContentAlignment.MiddleCenter;
            tomarBtn.Location = new Point(750, 550);
            tomarBtn.Size = new Size(80, 50);
            tomarBtn.Cursor = Cursors.Hand;
            tomarBtn.FlatAppearance.BorderSize = 0;
            tomarBtn.Click += Fotografia.TomarBtn_Click;

            retomarBtn = new Button();
            retomarBtn.BackgroundImage = Image.FromFile("Recursos/photoretake.png");
            retomarBtn.BackgroundImageLayout = ImageLayout.Stretch;
            retomarBtn.BackColor = Color.Gray;
            retomarBtn.FlatStyle = FlatStyle.Flat;
            retomarBtn.ImageAlign = ContentAlignment.MiddleCenter;
            retomarBtn.Location = new Point(860, 550);
            retomarBtn.Size = new Size(80, 50);
            retomarBtn.Cursor = Cursors.Hand;
            retomarBtn.FlatAppearance.BorderSize = 0;
            retomarBtn.Click += Fotografia.RetomarBtn_Click;

            importarBtn = new Button();
            importarBtn.BackgroundImage = Image.FromFile("Recursos/upload.png");
            importarBtn.BackgroundImageLayout = ImageLayout.Stretch;
            importarBtn.BackColor = Color.Gray;
            importarBtn.FlatStyle = FlatStyle.Flat;
            importarBtn.ImageAlign = ContentAlignment.MiddleCenter;
            importarBtn.Location = new Point(970, 550);
            importarBtn.Size = new Size(80, 50);
            importarBtn.Cursor = Cursors.Hand;
            importarBtn.FlatAppearance.BorderSize = 0;
            importarBtn.Click += Fotografia.ImportarBtn_Click;

            registrarBtn.Text = "Registrar";
            registrarBtn.BackColor = Color.DarkGreen; //ForestGreen
            registrarBtn.Font = new Font("Race Sport", 16, FontStyle.Bold);
            registrarBtn.ForeColor = Color.White;
            registrarBtn.Location = new Point(855, 660);
            registrarBtn.Size = new Size(200, 35);
            registrarBtn.FlatStyle = FlatStyle.Flat;
            registrarBtn.Cursor = Cursors.Hand;
            registrarBtn.FlatAppearance.BorderSize = 0;
            MiembroServicio servicio = new MiembroServicio();
            DateTime FechaRegistro = DateTime.Now.Date;

            registrarBtn.Click += (s, e) =>
            {
                nombreTxt.TextChanged -= nombreTxt_TextChanged;
                apePaternoTxt.TextChanged -= apePaternoTxt_TextChanged;
                apeMaternoTxt.TextChanged -= apeMaternoTxt_TextChanged;
                telefonoTxt.TextChanged -= telefonoTxt_TextChanged;

                byte[] LeerImagenComoBytes(string ruta)
                {
                    return File.ReadAllBytes(ruta);
                }

                byte[] imagenBytes = LeerImagenComoBytes($"C:/Users/amnm0/OneDrive/Documents/GitHub/FitManage/Presentacion/Recursos/Fotos/{nombreTxt.Text}.jpg");

                var idMembresia = (int)membresiaCombo.SelectedValue;
                if (idMembresia == 0)
                {
                    registrarBtn.Enabled = false;
                    return;
                }

                int idAsignado = servicio.RegistrarMiembro(
                    idMembresia,
                    nombreTxt.Text.Trim(),
                    apePaternoTxt.Text.Trim(),
                    apeMaternoTxt.Text.Trim(),
                    DateTime.Parse(fechaPicker.Text),
                    telefonoTxt.Text.Trim(),
                    FechaRegistro,
                    imagenBytes
                );

                if (idAsignado == -2)
                {
                    MessageBox.Show("Ya existe un miembro con esos datos.", "Registro duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    nombreTxt.Text = "";
                    apeMaternoTxt.Text = "";
                    apePaternoTxt.Text = "";
                    fechaPicker.Value = DateTime.Now;
                    telefonoTxt.Text = "";
                    fotoMiembroPct.Image = Image.FromFile("Recursos/placeholder.jpg");
                }
                else if (idAsignado == -1)
                {
                    MessageBox.Show("Ocurrió un error al registrar el miembro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Cliente registrado correctamente.\nID asignado: {idAsignado}", "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    nombreTxt.Text = "";
                    apeMaternoTxt.Text = "";
                    apePaternoTxt.Text = "";
                    fechaPicker.Value = DateTime.Now;
                    telefonoTxt.Text = "";
                    fotoMiembroPct.Image = Image.FromFile("Recursos/placeholder.jpg");
                }

                nombreTxt.TextChanged += nombreTxt_TextChanged;
                apePaternoTxt.TextChanged += apePaternoTxt_TextChanged;
                apeMaternoTxt.TextChanged += apeMaternoTxt_TextChanged;
                telefonoTxt.TextChanged += telefonoTxt_TextChanged;
                fechaLbl.Text = "Fecha de nacimiento";
            };


            regresarBtn = new Button();
            regresarBtn.Text = "Regresar";
            regresarBtn.BackColor = Color.DarkGray;
            regresarBtn.Font = new Font("Race Sport", 16);
            regresarBtn.Location = new Point(60, 730);
            regresarBtn.Size = new Size(200, 35);
            regresarBtn.FlatStyle = FlatStyle.Flat;
            regresarBtn.Cursor = Cursors.Hand;
            regresarBtn.FlatAppearance.BorderSize = 0;

            flechaBtn = new Button();
            flechaBtn.BackgroundImage = Image.FromFile("Recursos/regresar.png");
            flechaBtn.Location = new Point(27, 733);
            flechaBtn.Size = new Size(30, 35);
            flechaBtn.BackgroundImageLayout = ImageLayout.Stretch;
            flechaBtn.BackColor = Color.DarkGray;
            flechaBtn.FlatStyle = FlatStyle.Flat;
            flechaBtn.ImageAlign = ContentAlignment.MiddleCenter;
            flechaBtn.Cursor = Cursors.Hand;
            flechaBtn.FlatAppearance.BorderSize = 0;

            //Botón de cobrar
            cobrarBtn = new Button();
            cobrarBtn.BackgroundImage = Image.FromFile("Recursos/cobrar.png");
            cobrarBtn.Location = new Point(750, 645);
            cobrarBtn.Size = new Size(80, 60);
            cobrarBtn.BackgroundImageLayout = ImageLayout.Stretch;
            cobrarBtn.BackColor = Color.Gray;
            cobrarBtn.FlatStyle = FlatStyle.Flat;
            cobrarBtn.ImageAlign = ContentAlignment.MiddleCenter;
            cobrarBtn.Cursor = Cursors.Hand;
            cobrarBtn.FlatAppearance.BorderSize = 0;

            //Agregar componentes a la interfaz
            nuevoPanel.Controls.Add(nombreLbl);
            nuevoPanel.Controls.Add(apePaternoLbl);
            nuevoPanel.Controls.Add(apeMaternoLbl);
            nuevoPanel.Controls.Add(fechaLbl);
            nuevoPanel.Controls.Add(telefonoLbl);
            nuevoPanel.Controls.Add(membresiaLbl);
            nuevoPanel.Controls.Add(fotoLbl);
            nuevoPanel.Controls.Add(nombreTxt);
            nuevoPanel.Controls.Add(apePaternoTxt);
            nuevoPanel.Controls.Add(apeMaternoTxt);
            nuevoPanel.Controls.Add(telefonoTxt);
            nuevoPanel.Controls.Add(membresiaCombo);
            nuevoPanel.Controls.Add(fotoMiembroPct);
            nuevoPanel.Controls.Add(tomarBtn);
            nuevoPanel.Controls.Add(retomarBtn);
            nuevoPanel.Controls.Add(importarBtn);
            nuevoPanel.Controls.Add(registrarBtn);
            nuevoPanel.Controls.Add(fechaPicker);
            nuevoPanel.Controls.Add(regresarBtn);
            nuevoPanel.Controls.Add(flechaBtn);
            nuevoPanel.Controls.Add(cobrarBtn);
            nuevoPanel.Controls.Add(dispositivosCombo);
            nuevoPanel.Controls.Add(abrirCamara);

            return nuevoPanel;
        }

        private void CargarMembresias()
        {
            MembresiaServicio memCombo = new MembresiaServicio();
            List<Membresia> membresias = memCombo.ObtenerMembresias();

            membresias.Insert(0, new Membresia { Id_membresia = 0, Tipo_membresia = "Seleccionar membresía", Precio = 0 });

            // Modificar lo que se muestra en el ComboBox
            membresiaCombo.DataSource = membresias;
            membresiaCombo.DisplayMember = "DescripcionCompleta"; // Usamos una propiedad personalizada
            membresiaCombo.ValueMember = "Id_membresia";
        }
        public void MostrarPanel(Panel nuevoPanel)
        {
            if (panelConsulta != null)
            {
                mainPanel.Controls.Remove(panelConsulta);
                panelConsulta.Controls.Remove(panelConsulta); // 🔸 Libera recursos y destruye el panel viejo
                panelConsulta = null;
            }

            panelConsulta = nuevoPanel;
            panelConsulta.Visible = true;
            mainPanel.Controls.Add(panelConsulta);
            panelConsulta.BringToFront();
        }
        private void nombreTxt_TextChanged(object sender, EventArgs e)
        {
            if (ValidarDatos.ValidarTexto(nombreTxt.Text))
            {
                nombreTxt.BackColor = Color.White; // Válido
                registrarBtn.Enabled = true;
            }
            else
            {
                nombreTxt.BackColor = Color.LightPink; // Inválido
                registrarBtn.Enabled = false;
                
            }
        }
        private void apePaternoTxt_TextChanged(object sender, EventArgs e)
        {
            if (ValidarDatos.ValidarTexto(apePaternoTxt.Text))
            {
                apePaternoTxt.BackColor = Color.White; // Válido
                registrarBtn.Enabled = true;
            }
            else
            {
                apePaternoTxt.BackColor = Color.LightPink; // Inválido
                registrarBtn.Enabled = false;
            }
        }
        private void apeMaternoTxt_TextChanged(object sender, EventArgs e)
        {
            if (ValidarDatos.ValidarTexto(apeMaternoTxt.Text))
            {
                apeMaternoTxt.BackColor = Color.White; // Válido
                registrarBtn.Enabled = true;
            }
            else
            {
                apeMaternoTxt.BackColor = Color.LightPink; // Inválido
                registrarBtn.Enabled = false;
            }
        }
        private void telefonoTxt_TextChanged(object sender, EventArgs e)
        {
            if (ValidarDatos.ValidarTelefono(telefonoTxt.Text))
            {
                telefonoTxt.BackColor = Color.White;
                registrarBtn.Enabled = true;
            }
            else
            {
                telefonoTxt.BackColor = Color.LightPink;
                registrarBtn.Enabled = false;
            }
        }
        private void fechaPicker_ValueChanged(object sender, EventArgs e)
        {
            if (ValidarDatos.ValidarFecha(fechaPicker.Value))
            {
                fechaLbl.Text = "✔ Fecha válida";
                fechaLbl.ForeColor = Color.Green;
                registrarBtn.Enabled = true;
            }
            else
            {
                fechaLbl.Text = "✖ Fecha inválida";
                fechaLbl.ForeColor = Color.Red;
                registrarBtn.Enabled = false;
            }
        }
    }
}

