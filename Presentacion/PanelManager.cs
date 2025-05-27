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
        DateTimePicker fechaPicker = new DateTimePicker();
        ErrorProvider errorProvider = new ErrorProvider(); // se utiliza para las validaciones de los campos de texto
        Label fechaLbl = new Label();
        public static TextBox nombreTxt, apePaternoTxt, apeMaternoTxt, telefonoTxt;
        Button registrarBtn = new Button();
        MiembroServicio miembroServicio;
        VentanaCobrar ventanaCobrar;

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
            Label tituloLbl, subtituloLbl, nombreLbl, apePaternoLbl, apeMaternoLbl, telefonoLbl, membresiaLbl, fotoLbl;
            Button tomarBtn, retomarBtn, importarBtn, regresarBtn,flechaBtn, cobrarBtn;

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
            nombreTxt.TextChanged += new EventHandler(NombreTxt_TextChanged);
            

            apePaternoTxt = new TextBox();
            apePaternoTxt.Location = new Point(37, 262);
            apePaternoTxt.Size = new Size(360, 30);
            apePaternoTxt.Font = new Font("Tahoma", 12);
            apePaternoTxt.ForeColor = Color.Black;
            apePaternoTxt.BorderStyle = BorderStyle.FixedSingle;
            apePaternoTxt.CharacterCasing = CharacterCasing.Upper;
            apePaternoTxt.TextChanged += new EventHandler(ApePaternoTxt_TextChanged);

            apeMaternoTxt = new TextBox();
            apeMaternoTxt.Location = new Point(37, 362);
            apeMaternoTxt.Size = new Size(360, 30);
            apeMaternoTxt.Font = new Font("Tahoma", 12);
            apeMaternoTxt.ForeColor = Color.Black;
            apeMaternoTxt.BorderStyle = BorderStyle.FixedSingle;
            apeMaternoTxt.CharacterCasing = CharacterCasing.Upper;
            apeMaternoTxt.TextChanged += new EventHandler(ApeMaternoTxt_TextChanged);

            //DatePicker para la fecha de nacimiento
            fechaPicker.Format = DateTimePickerFormat.Short;
            fechaPicker.Location = new Point(37, 462);
            fechaPicker.Width = 360;
            fechaPicker.Font = new Font("Tahoma", 12);
            fechaPicker.ForeColor = Color.Black;
            fechaPicker.ValueChanged += new EventHandler(FechaPicker_ValueChanged);

            telefonoTxt = new TextBox();
            telefonoTxt.Location = new Point(37, 562);
            telefonoTxt.Size = new Size(290, 30);
            telefonoTxt.Font = new Font("Tahoma", 12);
            telefonoTxt.ForeColor = Color.Black;
            telefonoTxt.BorderStyle = BorderStyle.FixedSingle;
            telefonoTxt.TextChanged += new EventHandler(TelefonoTxt_TextChanged);

            //ComboBox para el tipo de membresía
            ComboBox membresiaCombo = new ComboBox();
            membresiaCombo = CargarMembresias(membresiaCombo);
            membresiaCombo.Location = new Point(37, 662);
            membresiaCombo.Size = new Size(290, 30);
            membresiaCombo.Font = new Font("Tahoma", 12);
            membresiaCombo.Text = "Seleccione una opción";

            //Panel para la foto
            fotoMiembroPct.Image = Image.FromFile("Recursos/placeholder.jpg");
            fotoMiembroPct.SizeMode = PictureBoxSizeMode.StretchImage;
            fotoMiembroPct.Size = new Size(380, 350);
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
            int anchoBtn = fotoMiembroPct.Size.Width / 3 - 15;
            tomarBtn = new Button();
            tomarBtn.BackgroundImage = Image.FromFile("Recursos/photo.png");
            tomarBtn.BackgroundImageLayout = ImageLayout.Stretch;
            tomarBtn.BackColor = Color.Gray;
            tomarBtn.FlatStyle = FlatStyle.Flat;
            tomarBtn.ImageAlign = ContentAlignment.MiddleCenter;
            tomarBtn.Location = new Point(fotoMiembroPct.Location.X, 550);
            tomarBtn.Size = new Size(anchoBtn, 55);
            tomarBtn.Cursor = Cursors.Hand;
            tomarBtn.FlatAppearance.BorderSize = 0;
            tomarBtn.Click += Fotografia.TomarBtn_Click;

            retomarBtn = new Button();
            retomarBtn.BackgroundImage = Image.FromFile("Recursos/photoretake.png");
            retomarBtn.BackgroundImageLayout = ImageLayout.Stretch;
            retomarBtn.BackColor = Color.Gray;
            retomarBtn.FlatStyle = FlatStyle.Flat;
            retomarBtn.ImageAlign = ContentAlignment.MiddleCenter;
            retomarBtn.Location = new Point(tomarBtn.Location.X + tomarBtn.Size.Width + 23, 550);
            retomarBtn.Size = new Size(anchoBtn, 55);
            retomarBtn.Cursor = Cursors.Hand;
            retomarBtn.FlatAppearance.BorderSize = 0;
            retomarBtn.Click += Fotografia.RetomarBtn_Click;

            importarBtn = new Button();
            importarBtn.BackgroundImage = Image.FromFile("Recursos/upload.png");
            importarBtn.BackgroundImageLayout = ImageLayout.Stretch;
            importarBtn.BackColor = Color.Gray;
            importarBtn.FlatStyle = FlatStyle.Flat;
            importarBtn.ImageAlign = ContentAlignment.MiddleCenter;
            importarBtn.Location = new Point(fotoMiembroPct.Location.X + fotoMiembroPct.Size.Width - anchoBtn, 550);
            importarBtn.Size = new Size(anchoBtn, 55);
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
            registrarBtn.Enabled = false;
            MiembroServicio servicio = new MiembroServicio();
            DateTime FechaRegistro = DateTime.Now.Date;

            registrarBtn.Click += (s, e) =>
            {
                nombreTxt.TextChanged -= NombreTxt_TextChanged;
                apePaternoTxt.TextChanged -= ApePaternoTxt_TextChanged;
                apeMaternoTxt.TextChanged -= ApeMaternoTxt_TextChanged;
                telefonoTxt.TextChanged -= TelefonoTxt_TextChanged;

                byte[] LeerImagenComoBytes(string ruta)
                {
                    return File.ReadAllBytes(ruta);
                }

                byte[] imagenBytes = LeerImagenComoBytes($"C:/Users/amnm0/OneDrive/Documents/GitHub/FitManage/Presentacion/Recursos/Fotos/{nombreTxt.Text}.jpg");

                int idMembresia;
                if (!int.TryParse(membresiaCombo.SelectedValue?.ToString(), out idMembresia))
                {
                    MessageBox.Show("El ID de la membresía no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Console.WriteLine("ID de membresía convertido: " + idMembresia);

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

                nombreTxt.TextChanged += NombreTxt_TextChanged;
                apePaternoTxt.TextChanged += ApePaternoTxt_TextChanged;
                apeMaternoTxt.TextChanged += ApeMaternoTxt_TextChanged;
                telefonoTxt.TextChanged += TelefonoTxt_TextChanged;
                fechaLbl.Text = "Fecha de nacimiento";
            };


            regresarBtn = new Button();
            regresarBtn.Text = "Regresar";
            regresarBtn.BackColor = Color.WhiteSmoke;
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
            flechaBtn.BackColor = Color.WhiteSmoke;
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
                if (membresiaCombo.SelectedItem is Membresia membresiaSeleccionada && membresiaCombo.SelectedIndex != 0)
                {
                    var nombreMembresia = membresiaSeleccionada.Tipo;
                    var precioMembresia = membresiaSeleccionada.Precio;

                    ventanaCobrar = new VentanaCobrar(nombreMembresia, precioMembresia, 0);
                    ventanaCobrar.FormClosed += (a, E) =>
                    {
                        if (ventanaCobrar.isConfirmacion)
                        {
                            registrarBtn.Enabled = true;
                        }
                    };

                    ventanaCobrar.Show();
                    
                }
                else
                    MessageBox.Show("Por favor, seleccione una membresía válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
        //Panel de acceso
        public Panel AccesoPanel()
        {
            bool isAccesoValido = false;
            Panel nuevoPanel = new Panel();
            nuevoPanel.Name = "AccesoPanel";
            nuevoPanel.BackgroundImage = Image.FromFile("Recursos/laminahomegym.png");
            nuevoPanel.BackgroundImageLayout = ImageLayout.Stretch;
            nuevoPanel.Location = new Point(500, 150);
            nuevoPanel.Size = new Size(800, 400);
            nuevoPanel.BackColor = Color.WhiteSmoke;

            //Label de descripción
            Label descripcionLbl = new Label();
            descripcionLbl.Text = "         INTRODUCIR\n NUMERO DE ACCESO";
            descripcionLbl.Font = new Font("Race Sport", 34, FontStyle.Bold);
            descripcionLbl.ForeColor = Color.Black;
            descripcionLbl.BackColor = Color.Transparent;
            descripcionLbl.Location = new Point(70, 60);
            descripcionLbl.AutoSize = true;

            //TextBox para el número de acceso
            TextBox accesoTxt = new TextBox();
            accesoTxt.Location = new Point(200, 220);
            accesoTxt.Size = new Size(400, 50);
            accesoTxt.Font = new Font("Tahoma", 22);
            accesoTxt.ForeColor = Color.Black;
            accesoTxt.BorderStyle = BorderStyle.FixedSingle;
            accesoTxt.TextChanged += (s, e) =>
            {
                if (ValidarDatos.ValidarSoloNumeros(accesoTxt.Text)){
                    accesoTxt.BackColor = Color.White; // Válido
                    isAccesoValido = true;
                }
                else { 
                    accesoTxt.BackColor = Color.LightPink; // Inválido
                    isAccesoValido = false;
                }
            };

            //Botón de acceso
            Button accesoBtn = new Button();
            accesoBtn.Text = "ACCEDER";
            accesoBtn.Location = new Point(300, 300);
            accesoBtn.Size = new Size(200, 40);
            accesoBtn.Font = new Font("Race Sport", 18, FontStyle.Italic);
            accesoBtn.BackColor = Color.Gray;
            accesoBtn.ForeColor = Color.White;
            accesoBtn.FlatStyle = FlatStyle.Flat;
            accesoBtn.FlatAppearance.BorderSize = 0;
            accesoBtn.Click += (s, e) =>
            {
                miembroServicio = new MiembroServicio();

                if (ValidarDatos.ValidarSoloNumeros(accesoTxt.Text) && Int32.TryParse(accesoTxt.Text, out int id) && (miembroServicio.ObtenerMiembroPorId(id) is Miembro miembro)) { 
                    MostrarPanel(ClienteAcceso(miembro));
                }
                else
                    MessageBox.Show("Número de acceso inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };


            nuevoPanel.Controls.Add(descripcionLbl);
            nuevoPanel.Controls.Add(accesoTxt);
            nuevoPanel.Controls.Add(accesoBtn);

            return nuevoPanel;
        } 
        //Panel de informacion de cliente con el acceso
        public Panel ClienteAcceso(Miembro miembro)
        {
            Panel nuevoPanel = new Panel();
            nuevoPanel.Text = "Acceso al cliente";
            nuevoPanel.AutoSize = true;
            nuevoPanel.BackColor = Color.WhiteSmoke;

            //Labels para loa informacion
            ComboBox membresiaCombo = new ComboBox();
            Label tituloLbl, idLbl, nombreLbl, apePaternoLbl, apeMaternoLbl, telefonoLbl, nacimientoLbl, registroLbl ,vencimientoLbl, membresiaLbl, tipoMemLbl;
            Label estado = new Label();
            //Fotografia del cliente
            PictureBox fotografia = new PictureBox();
            //Botones necesarios para la ventana de cobro
            Button aceptarBtn;

            // Botón de renovar
            Button renovarBtn = new Button();
            renovarBtn.Text = "RENOVAR";
            renovarBtn.Location = new Point(1100, 700);
            renovarBtn.AutoSize = true;
            renovarBtn.Font = new Font("Race Sport", 20);
            renovarBtn.BackColor = Color.Gray;
            renovarBtn.ForeColor = Color.White;
            renovarBtn.FlatStyle = FlatStyle.Flat;
            renovarBtn.FlatAppearance.BorderSize = 0;


            tituloLbl = new Label();
            tituloLbl.Text = "ACCESO";
            tituloLbl.Location = new Point(700, 20);
            tituloLbl.AutoSize = true;
            tituloLbl.Font = new Font("Race Sport", 50, FontStyle.Bold);

            idLbl = new Label();
            idLbl.Text = " ID:\n" + miembro.IdMiembro;
            idLbl.Location = new Point(1300, 120);
            idLbl.AutoSize = true;
            idLbl.Font = new Font("Tahoma", 18, FontStyle.Bold);

            nombreLbl = new Label();
            nombreLbl.Text = "Nombre(s):\n" + miembro.Nombres;
            nombreLbl.Location = new Point(700, 120);
            nombreLbl.AutoSize = true;
            nombreLbl.Font = new Font("Tahoma", 18);

            apePaternoLbl = new Label();
            apePaternoLbl.Text = "Apellido Paterno:\n" + miembro.ApellidoPaterno;
            apePaternoLbl.Location = new Point(700, 210);
            apePaternoLbl.AutoSize = true;
            apePaternoLbl.Font = new Font("Tahoma", 18);

            apeMaternoLbl = new Label();
            apeMaternoLbl.Text = "Apellido Materno:\n" + miembro.ApellidoMaterno;
            apeMaternoLbl.Location = new Point(700, 280);
            apeMaternoLbl.AutoSize = true;
            apeMaternoLbl.Font = new Font("Tahoma", 18);

            telefonoLbl = new Label();
            telefonoLbl.Text = "Teléfono:\n" + miembro.NumeroTelefono;
            telefonoLbl.Location = new Point(700, 360);
            telefonoLbl.AutoSize = true;
            telefonoLbl.Font = new Font("Tahoma", 18);

            nacimientoLbl = new Label();
            nacimientoLbl.Text = "Fecha de Nacimiento:\n" + miembro.FechaNacimiento.ToString("dd/MM/yyyy");
            nacimientoLbl.Location = new Point(700, 440);
            nacimientoLbl.AutoSize = true;
            nacimientoLbl.Font = new Font("Tahoma", 18);

            registroLbl = new Label();
            registroLbl.Text = "Fecha de Registro:\n" + miembro.FechaRegistro.ToString("dd/MM/yyyy");
            registroLbl.Location = new Point(700, 520);
            registroLbl.AutoSize = true;
            registroLbl.Font = new Font("Tahoma", 18);

            vencimientoLbl = new Label();
            vencimientoLbl.Text = "Fecha de Vencimiento:\n" + miembro.FechaVencimiento.ToString("dd/MM/yyyy");
            vencimientoLbl.Location = new Point(700, 600);
            vencimientoLbl.AutoSize = true;
            vencimientoLbl.Font = new Font("Tahoma", 18);

            membresiaLbl = new Label();
            membresiaLbl.Text = "Tipo de membresía";
            membresiaLbl.Location = new Point(700, 680);
            membresiaLbl.AutoSize = true;
            membresiaLbl.Font = new Font("Tahoma", 18);

            if (miembro.Fotografia != null)
            {
                using (var ms = new System.IO.MemoryStream(miembro.Fotografia))
                {
                    fotografia.Image = Image.FromStream(ms);
                }
            }
            else
            {
                fotografia.Image = Image.FromFile("Recursos/placeholder.jpg");
            }

            fotografia.SizeMode = PictureBoxSizeMode.StretchImage;
            fotografia.Size = new Size(280, 350);
            fotografia.Location = new Point(300, 120);
            fotografia.BorderStyle = BorderStyle.FixedSingle;

            //Botón de aceptar
            aceptarBtn = new Button();
            aceptarBtn.Text = "ACEPTAR";
            aceptarBtn.Location = new Point(1100, 700);
            aceptarBtn.AutoSize = true;
            aceptarBtn.Font = new Font("Race Sport", 20);
            aceptarBtn.BackColor = Color.Gray;
            aceptarBtn.ForeColor = Color.White;
            aceptarBtn.FlatStyle = FlatStyle.Flat;
            aceptarBtn.FlatAppearance.BorderSize = 0;
            aceptarBtn.Click += (s, e) => { nuevoPanel.Dispose();
                MostrarPanel(AccesoPanel());
            };

            DateTime fechaActual = DateTime.Today;
            if (miembro.FechaVencimiento < fechaActual)
            {
                nuevoPanel.Controls.Add(renovarBtn);

                //ComboBox para el tipo de membresía
                //ComboBox membresiaCombo = new ComboBox();
                membresiaCombo = CargarMembresias(membresiaCombo);
                membresiaCombo.Location = new Point(700, 710);
                membresiaCombo.Size = new Size(290, 30);
                membresiaCombo.Font = new Font("Tahoma", 12);
                nuevoPanel.Controls.Add(membresiaCombo);

                estado.Text = "VENCIDA";
                estado.BackColor = Color.Red;
            }
            else
            {
                tipoMemLbl = new Label();
                if (miembro.IdMembresia == 1001)
                    tipoMemLbl.Text = "MENSUAL";

                else if (miembro.IdMembresia == 1002)
                    tipoMemLbl.Text = "SEMANA";

                else
                    tipoMemLbl.Text = "DIA";

                tipoMemLbl.Location = new Point(700, 710);
                tipoMemLbl.AutoSize = true;
                tipoMemLbl.Font = new Font("Tahoma", 18);
                nuevoPanel.Controls.Add(tipoMemLbl);

                estado.Text = "VIGENTE";
                estado.BackColor = Color.Green;

                //Calculo de dias restantes
                TimeSpan diasRestantes = miembro.FechaVencimiento - fechaActual;
                Label restantesLbl = new Label();
                restantesLbl.Text = diasRestantes.Days + " Días restantes. ";
                restantesLbl.Location = new Point(310, 580);
                restantesLbl.AutoSize = true;
                restantesLbl.Font = new Font("Tahoma", 20, FontStyle.Bold);
                if (diasRestantes.TotalDays <= 5)
                    restantesLbl.ForeColor = Color.Red;
                else
                    restantesLbl.ForeColor = Color.Black;
                
                nuevoPanel.Controls.Add(restantesLbl);
            }
            renovarBtn.Click += (s, e) =>
            {
                if (membresiaCombo.SelectedItem is Membresia membresiaSeleccionada && membresiaCombo.SelectedIndex != 0)
                {
                    var nombreMembresia = membresiaSeleccionada.Tipo;
                    var precioMembresia = membresiaSeleccionada.Precio;

                    var ventanaCobrar = new VentanaCobrar(nombreMembresia, precioMembresia, miembro.IdMiembro);
                    ventanaCobrar.FormClosed += (a, E) =>
                    {
                        Console.WriteLine("VentanaCobrar se cerró. Recargando ClienteAcceso...");

                        //Recargar contenido de `ClienteAcceso` después del pago
                        miembro = miembroServicio.ObtenerMiembroPorId(miembro.IdMiembro);
                        MostrarPanel(ClienteAcceso(miembro));
                    };

                    ventanaCobrar.Show();
                }
                else
                    MessageBox.Show("Por favor, seleccione una membresía válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
            estado.Location = new Point(270, 500);
            estado.AutoSize = true;
            estado.Font = new Font("Race Sport", 40);    
            estado.ForeColor = Color.White;

            nuevoPanel.Controls.Add(tituloLbl);
            nuevoPanel.Controls.Add(idLbl);
            nuevoPanel.Controls.Add(nombreLbl);
            nuevoPanel.Controls.Add(apePaternoLbl);
            nuevoPanel.Controls.Add(apeMaternoLbl);
            nuevoPanel.Controls.Add(telefonoLbl);
            nuevoPanel.Controls.Add(nacimientoLbl);
            nuevoPanel.Controls.Add(registroLbl);
            nuevoPanel.Controls.Add(vencimientoLbl);
            nuevoPanel.Controls.Add(membresiaLbl);
            nuevoPanel.Controls.Add(estado);
            nuevoPanel.Controls.Add(aceptarBtn);
            nuevoPanel.Controls.Add(fotografia);
            //nuevoPanel.Controls.Add(fotografia);

            return nuevoPanel;
        }
        private ComboBox CargarMembresias(ComboBox combo)
        {
            MembresiaServicio memCombo = new MembresiaServicio();
            List<Membresia> membresias = memCombo.ObtenerMembresias();

            // Agregar opción por defecto
            membresias.Insert(0, new Membresia { Id_membresia = 0, Tipo = "Seleccionar membresía", Precio = 0 });

            // Asignar propiedades al ComboBox recibido
            combo.DataSource = membresias;
            combo.DisplayMember = "DescripcionCompleta"; // Propiedad personalizada para mostrar
            combo.ValueMember = "Id_membresia";

            return combo;
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
        private void NombreTxt_TextChanged(object sender, EventArgs e)
        {
            if (ValidarDatos.ValidarTexto(nombreTxt.Text))
            {
                nombreTxt.BackColor = Color.White; // Válido
            }
            else
            {
                nombreTxt.BackColor = Color.LightPink; // Inválido
            }
        }
        private void ApePaternoTxt_TextChanged(object sender, EventArgs e)
        {
            if (ValidarDatos.ValidarTexto(apePaternoTxt.Text))
            {
                apePaternoTxt.BackColor = Color.White; // Válido
            }
            else
            {
                apePaternoTxt.BackColor = Color.LightPink; // Inválido
            }
        }
        private void ApeMaternoTxt_TextChanged(object sender, EventArgs e)
        {
            if (ValidarDatos.ValidarTexto(apeMaternoTxt.Text))
            {
                apeMaternoTxt.BackColor = Color.White; // Válido
            }
            else
            {
                apeMaternoTxt.BackColor = Color.LightPink; // Inválido
            }
        }
        private void TelefonoTxt_TextChanged(object sender, EventArgs e)
        {
            if (ValidarDatos.ValidarSoloNumeros(telefonoTxt.Text))
            {
                telefonoTxt.BackColor = Color.White;
            }
            else
            {
                telefonoTxt.BackColor = Color.LightPink;
            }
        }
        private void FechaPicker_ValueChanged(object sender, EventArgs e)
        {
            if (ValidarDatos.ValidarFecha(fechaPicker.Value))
            {
                fechaLbl.Text = "✔ Fecha válida";
                fechaLbl.ForeColor = Color.Green;
            }
            else
            {
                fechaLbl.Text = "✖ Fecha inválida";
                fechaLbl.ForeColor = Color.Red;
            }
        }
    }
}