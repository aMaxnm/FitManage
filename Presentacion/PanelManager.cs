using System.Drawing;
using System.Windows.Forms;

namespace Presentación
{
    internal class PanelManager
    {
        private Panel panelConsulta;
        private readonly Panel mainPanel;

        public PanelManager(Panel mainPanel)
        {
            this.mainPanel = mainPanel;
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
            Label tituloLbl, subtituloLbl, nombreLbl, apePaternoLbl, apeMaternoLbl, fechaLbl, telefonoLbl, membresiaLbl, fotoLbl;
            TextBox nombreTxt, apePaternoTxt, apeMaternoTxt, telefonoTxt;
            Button tomarBtn, retomarBtn, importarBtn, registrarBtn, regresarBtn,flechaBtn, cobrarBtn;

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

            fechaLbl = new Label();
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
            fotoLbl.Font = new Font("Tahoma", 25, FontStyle.Bold);
            fotoLbl.ForeColor = Color.Black;
            fotoLbl.Location = new Point(847, 135);
            fotoLbl.AutoSize = true;

            //TextBoxs para el formulario
            nombreTxt = new TextBox();
            nombreTxt.Location = new Point(37, 162);
            nombreTxt.Size = new Size(360, 30);
            nombreTxt.Font = new Font("Tahoma", 12);
            nombreTxt.ForeColor = Color.Black;
            nombreTxt.BorderStyle = BorderStyle.FixedSingle;

            apePaternoTxt = new TextBox();
            apePaternoTxt.Location = new Point(37, 262);
            apePaternoTxt.Size = new Size(360, 30);
            apePaternoTxt.Font = new Font("Tahoma", 12);
            apePaternoTxt.ForeColor = Color.Black;
            apePaternoTxt.BorderStyle = BorderStyle.FixedSingle;

            apeMaternoTxt = new TextBox();
            apeMaternoTxt.Location = new Point(37, 362);
            apeMaternoTxt.Size = new Size(360, 30);
            apeMaternoTxt.Font = new Font("Tahoma", 12);
            apeMaternoTxt.ForeColor = Color.Black;
            apeMaternoTxt.BorderStyle = BorderStyle.FixedSingle;

            //DatePicker para la fecha de nacimiento
            DateTimePicker fechaPicker = new DateTimePicker();
            fechaPicker.Format = DateTimePickerFormat.Short;
            fechaPicker.Location = new Point(37, 462);
            fechaPicker.Width = 360;
            fechaPicker.Font = new Font("Tahoma", 12);
            fechaPicker.ForeColor = Color.Black;

            telefonoTxt = new TextBox();
            telefonoTxt.Location = new Point(37, 562);
            telefonoTxt.Size = new Size(290, 30);
            telefonoTxt.Font = new Font("Tahoma", 12);
            telefonoTxt.ForeColor = Color.Black;
            telefonoTxt.BorderStyle = BorderStyle.FixedSingle;

            //ComboBox para el tipo de membresía
            ComboBox membresiaCombo = new ComboBox();
            membresiaCombo.Location = new Point(37, 662);
            membresiaCombo.Size = new Size(290, 30);
            membresiaCombo.Font = new Font("Tahoma", 12);
            membresiaCombo.Text = "Seleccione una opción";
            membresiaCombo.Items.Add("Dia");
            membresiaCombo.Items.Add("Semanal");
            membresiaCombo.Items.Add("Mensual");

            //Panel para la foto
            Panel fotoPanel = new Panel();
            fotoPanel.BackColor = Color.White;
            fotoPanel.Size = new Size(300, 350);
            fotoPanel.Location = new Point(750, 180);
            fotoPanel.BorderStyle = BorderStyle.FixedSingle;

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

            registrarBtn = new Button();
            registrarBtn.Text = "Registrar";
            registrarBtn.BackColor = Color.DarkGreen; //ForestGreen
            registrarBtn.Font = new Font("Race Sport", 16, FontStyle.Bold);
            registrarBtn.ForeColor = Color.White;
            registrarBtn.Location = new Point(855, 660);
            registrarBtn.Size = new Size(200, 35);
            registrarBtn.FlatStyle = FlatStyle.Flat;
            registrarBtn.Cursor = Cursors.Hand;
            registrarBtn.FlatAppearance.BorderSize = 0;

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
            nuevoPanel.Controls.Add(fotoPanel);
            nuevoPanel.Controls.Add(tomarBtn);
            nuevoPanel.Controls.Add(retomarBtn);
            nuevoPanel.Controls.Add(importarBtn);
            nuevoPanel.Controls.Add(registrarBtn);
            nuevoPanel.Controls.Add(fechaPicker);
            nuevoPanel.Controls.Add(regresarBtn);
            nuevoPanel.Controls.Add(flechaBtn);
            nuevoPanel.Controls.Add(cobrarBtn);

            return nuevoPanel;
        }

        public void MostrarPanel(Panel panel)
        {
            if (panelConsulta != null)
                {
                    panelConsulta.Visible = false;
                    mainPanel.Controls.Remove(panelConsulta);
                } // Oculta el panel anterior

            panelConsulta = panel;
            panelConsulta.Visible = true; // Muestra el nuevo panel
            mainPanel.Controls.Add(panelConsulta); // Agrega el panel al mainPanel
            panelConsulta.BringToFront();
        }

    }
}

