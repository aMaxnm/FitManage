using System.Drawing;
using System.Runtime.InteropServices.WindowsRuntime;
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

        public Panel PanelRegistro(string nombre, Color colorFondo)
        {
            Panel nuevoPanel = new Panel();
            nuevoPanel.Name = nombre;
            nuevoPanel.Location = new Point(208, 10);
            nuevoPanel.Size = new Size(1320, 776);
            nuevoPanel.BackColor = colorFondo;
            nuevoPanel.Visible = false;


            //Labels necesarias para el formulario
            Label tituloLbl, subtituloLbl, nombreLbl, apePaternoLbl, apeMaternoLbl, fechaLbl, telefonoLbl, membresiaLbl, fotoLbl;
            TextBox nombreTxt, apePaternoTxt, apeMaternoTxt, telefonoTxt;
            Button tomarBtn, retomarBtn, importarBtn, registrarBtn, regresarBtn,flechaBtn, cobrarBtn;

            //Labels para encabezado
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
            //Muestra el panel de cobro al hacer click
            cobrarBtn.Click += (s, e) =>
            {
                CobroPanel();
            };

            //Agregar componentes a la interfaz
            nuevoPanel.Controls.Add(tituloLbl);
            nuevoPanel.Controls.Add(subtituloLbl);
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

        //Método para crear el panel de cobro
        public void CobroPanel()
        {
            panelConsulta.Visible = false; // Oculta el panel anterior
            Panel cobroP = new Panel();
            cobroP.Name = "Cobro";
            cobroP.Location = new Point(550, 200);
            cobroP.Size = new Size(700, 400);
            cobroP.BackColor = Color.DarkGray;
            cobroP.Visible = true;
            cobroP.BorderStyle = BorderStyle.FixedSingle;
            mainPanel.Controls.Add(cobroP); // Agrega el nuevo panel al mainPanel
            cobroP.BringToFront();

            //Labels necesarias para el panel de cobro
            Label membresiaLbl, mesLbl, costoLbl, recibidoLbl, cambioLbl;
            //TextBoxs necesarias para el panel de cobro
            TextBox costoTxt, recibidoTxt, cambioTxt;
            //Botones necesarios para el panel de cobro
            Button confirmarBtn, cancelarBtn;


            //Configuracion de las labels
            membresiaLbl = new Label();
            membresiaLbl.Text = "MEMBRESIA:";
            membresiaLbl.Font = new Font("Tahoma", 19);
            membresiaLbl.ForeColor = Color.Black;
            membresiaLbl.Location = new Point(230, 30);
            membresiaLbl.AutoSize = true;

            mesLbl = new Label();
            mesLbl.Text = "MES";
            mesLbl.Font = new Font("Tahoma", 19);
            mesLbl.ForeColor = Color.Black;
            mesLbl.Location = new Point(390, 30);
            mesLbl.AutoSize = true;

            costoLbl = new Label();
            costoLbl.Text = "COSTO";
            costoLbl.Font = new Font("Tahoma", 19);
            costoLbl.ForeColor = Color.Black;
            costoLbl.Location = new Point(200, 110);
            costoLbl.AutoSize = true;

            recibidoLbl = new Label();
            recibidoLbl.Text = "EFECTIVO RECIBIDO";
            recibidoLbl.Font = new Font("Tahoma", 19);
            recibidoLbl.ForeColor = Color.Black;
            recibidoLbl.Location = new Point(40, 175);
            recibidoLbl.AutoSize = true;

            cambioLbl = new Label();
            cambioLbl.Text = "CAMBIO";
            cambioLbl.Font = new Font("Tahoma", 19);
            cambioLbl.ForeColor = Color.Black;
            cambioLbl.Location = new Point(190, 240);
            cambioLbl.AutoSize = true;

            //Configuracion para los TextBoxs
            costoTxt = new TextBox();
            costoTxt.Location = new Point(300, 112);
            costoTxt.Size = new Size(200, 30);
            costoTxt.Font = new Font("Tahoma", 14);
            costoTxt.ForeColor = Color.Black;
            costoTxt.BorderStyle = BorderStyle.FixedSingle;
            //costoTxt.ReadOnly = true;

            recibidoTxt = new TextBox();
            recibidoTxt.Location = new Point(300, 175);
            recibidoTxt.Size = new Size(200, 30);
            recibidoTxt.Font = new Font("Tahoma", 14);
            recibidoTxt.ForeColor = Color.Black;
            recibidoTxt.BorderStyle = BorderStyle.FixedSingle;

            cambioTxt = new TextBox();
            cambioTxt.Location = new Point(300, 242);
            cambioTxt.Size = new Size(200, 30);
            cambioTxt.Font = new Font("Tahoma", 14);
            cambioTxt.ForeColor = Color.Black;
            cambioTxt.BorderStyle = BorderStyle.FixedSingle;

            //Configuracion de los botones
            confirmarBtn = new Button();
            confirmarBtn.Text = "CONFIRMAR";
            confirmarBtn.Font = new Font("Tahoma", 14, FontStyle.Bold);
            confirmarBtn.ForeColor = Color.White;
            confirmarBtn.Location = new Point(390, 320);
            confirmarBtn.Size = new Size(160, 40);
            confirmarBtn.BackColor = Color.DarkGreen; //ForestGreen
            confirmarBtn.FlatStyle = FlatStyle.Flat;
            confirmarBtn.FlatAppearance.BorderSize = 0;
            confirmarBtn.Cursor = Cursors.Hand;

            cancelarBtn = new Button();
            cancelarBtn.Text = "CANCELAR";
            cancelarBtn.Font = new Font("Tahoma", 14, FontStyle.Bold);
            cancelarBtn.ForeColor = Color.White;
            cancelarBtn.Location = new Point(150, 320);
            cancelarBtn.Size = new Size(160, 40);
            cancelarBtn.BackColor = Color.DarkRed; //ForestGreen
            cancelarBtn.FlatStyle = FlatStyle.Flat;
            cancelarBtn.FlatAppearance.BorderSize = 0;
            cancelarBtn.Cursor = Cursors.Hand;
            cancelarBtn.Click += (s, e) =>
            {
                // Regresar al panel anterior
                MostrarPanel(panelConsulta);
                cobroP.Visible = false;
            };
           
            //Se agregan los componentes al panel
            cobroP.Controls.Add(membresiaLbl);
            cobroP.Controls.Add(mesLbl);
            cobroP.Controls.Add(costoLbl);
            cobroP.Controls.Add(recibidoLbl);
            cobroP.Controls.Add(cambioLbl);
            cobroP.Controls.Add(costoTxt);
            cobroP.Controls.Add(recibidoTxt);
            cobroP.Controls.Add(cambioTxt);
            cobroP.Controls.Add(confirmarBtn);
            cobroP.Controls.Add(cancelarBtn);
        }

        public void MostrarPanel(Panel panel)
        {
            if (panelConsulta != null)
                panelConsulta.Visible = false; // Oculta el panel anterior

            panelConsulta = panel;
            panelConsulta.Visible = true; // Muestra el nuevo panel
            mainPanel.Controls.Add(panelConsulta); // Agrega el panel al mainPanel
            panelConsulta.BringToFront();
        }

    }
}

