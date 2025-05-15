using Presentación;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class VentanaPrincipal : Form
    {
        Timer checarCursor; // se utiliza para manejar el despliegue de los botones de inventario
        ErrorProvider errorProvider = new ErrorProvider(); // se utiliza para las validaciones de los campos de texto
        Panel sidePanel = new Panel(); //  barra de navegacion
        Panel mainPanel = new Panel(); // panel principal donde se muesttran las funciones
        Panel logoPanel = new Panel();
        Color negro = Color.FromArgb(0,0,0);
        Font fuenteRace =  new Font("Race Sport", 15, FontStyle.Bold);
        Button entradaBtn, consultaMiembroBtn, registroClienteBtn, ventaBtn, inventarioBtn;
        LinkLabel cerrarSesionLbl = new LinkLabel();
        PanelManager panelManager;

        //panel desplegable con botones
        Panel subBotonesPanel = new Panel();

        private void VentanaPrincipal_Load(object sender, EventArgs e)
        {

        }

        Button tiposMembresiaBtn, productosBtn, registroVentaBtn;

        public VentanaPrincipal()
        {
            checarCursor = new Timer();
            InitializeComponent();
            InicializarComponentes();
            SubBotones();
            panelManager = new PanelManager(mainPanel);
            //ConsultaMiembro consultaMiembroPanel = new ConsultaMiembro();
            Panel panelRegistro = panelManager.CrearPanel("Registro", Color.DarkGray);
            registroClienteBtn.Click += (s, e) => panelManager.MostrarPanel(panelRegistro);
            consultaMiembroBtn.Click += (s, e) => panelManager.MostrarPanel(consultaMiembroPanel);
            EditarMembresiaPanel editarMembresiaPanel = new EditarMembresiaPanel();
            tiposMembresiaBtn.Click += (s, e) => panelManager.MostrarPanel(editarMembresiaPanel);

        }
        private void InicializarComponentes()
        {
            this.WindowState = FormWindowState.Maximized; // ventana completa
            this.MinimumSize = new Size(800, 600);
            this.FormBorderStyle = FormBorderStyle.Sizable;

            //SidePanel
            sidePanel.BackColor = Color.Gray;
            sidePanel.BackgroundImage = Image.FromFile("Recursos/fondoSide.png");
            sidePanel.BackgroundImageLayout = ImageLayout.Stretch;
            sidePanel.Dock = DockStyle.Left;
            sidePanel.Width = 200;
            this.Controls.Add(sidePanel);

            //mainPanel
            mainPanel.BackColor = Color.WhiteSmoke;
            mainPanel.Dock = DockStyle.Fill;
            this.Controls.Add(mainPanel);

            //logoPanel
            logoPanel.BackgroundImage = Image.FromFile("Recursos/logo.png");
            logoPanel.BackgroundImageLayout = ImageLayout.Stretch;
            logoPanel.Dock = DockStyle.Top;
            logoPanel.Height = 100;
            sidePanel.Controls.Add(logoPanel);

            //Panel de subBotones
            subBotonesPanel.Size = new Size(150, 150);
            subBotonesPanel.Location = new Point(sidePanel.Width + 1, 440);
            subBotonesPanel.BackColor = Color.WhiteSmoke;
            subBotonesPanel.Visible = false;

            //Botón de entrada
            entradaBtn = new Button();
            entradaBtn.BackColor = negro;
            entradaBtn.BackgroundImage = Image.FromFile("Recursos/entradas.png");
            entradaBtn.BackgroundImageLayout = ImageLayout.Stretch;
            entradaBtn.ImageAlign = ContentAlignment.MiddleCenter;
            entradaBtn.Location = new Point(0, 120);
            entradaBtn.Size = new Size(sidePanel.Width, 65);
            entradaBtn.FlatStyle = FlatStyle.Flat;
            entradaBtn.Cursor = Cursors.Hand;
            //Botón de consulta
            consultaMiembroBtn = new Button();
            consultaMiembroBtn.BackColor = negro;
            consultaMiembroBtn.BackgroundImage = Image.FromFile("Recursos/miembros.png");
            consultaMiembroBtn.BackgroundImageLayout = ImageLayout.Stretch;
            consultaMiembroBtn.Location = new Point(0, 200);
            consultaMiembroBtn.Size = new Size(sidePanel.Width, 65);
            consultaMiembroBtn.FlatStyle = FlatStyle.Flat;
            consultaMiembroBtn.Cursor = Cursors.Hand;
            //Botón de registro
            registroClienteBtn = new Button();
            registroClienteBtn.BackColor = negro;
            registroClienteBtn.BackgroundImage = Image.FromFile("Recursos/agregarMiembro.png");
            registroClienteBtn.BackgroundImageLayout = ImageLayout.Stretch;
            registroClienteBtn.Location = new Point(0, 280);
            registroClienteBtn.Size = new Size(sidePanel.Width, 65);
            registroClienteBtn.FlatStyle = FlatStyle.Flat;
            registroClienteBtn.Cursor = Cursors.Hand;
            //Botón de ventas
            ventaBtn = new Button();
            ventaBtn.BackColor = negro;
            ventaBtn.BackgroundImage = Image.FromFile("Recursos/venta.png");
            ventaBtn.BackgroundImageLayout = ImageLayout.Stretch;
            ventaBtn.Location = new Point(0, 360);
            ventaBtn.Size = new Size(sidePanel.Width, 65);
            ventaBtn.FlatStyle = FlatStyle.Flat;
            ventaBtn.Cursor = Cursors.Hand;
            //Botón de inventario
            inventarioBtn = new Button();
            inventarioBtn.BackColor = negro;
            inventarioBtn.BackgroundImage = Image.FromFile("Recursos/inventario.png");
            inventarioBtn.BackgroundImageLayout = ImageLayout.Stretch;
            inventarioBtn.Location = new Point(0, 440);
            inventarioBtn.Size = new Size(sidePanel.Width, 65);
            inventarioBtn.FlatStyle = FlatStyle.Flat;
            inventarioBtn.Cursor = Cursors.Hand;
            //Cierre de sesion
            cerrarSesionLbl.Text = "Cerrar" + Environment.NewLine + "Sesión";
            cerrarSesionLbl.LinkBehavior = LinkBehavior.NeverUnderline;
            cerrarSesionLbl.Font = fuenteRace;
            cerrarSesionLbl.LinkColor = Color.White;
            cerrarSesionLbl.BackColor = Color.Transparent;
            cerrarSesionLbl.Location = new Point(0, 600);
            cerrarSesionLbl.Size = new Size(sidePanel.Width, 65);

            //Agregar los componentes al sidePanel
            sidePanel.Controls.Add(entradaBtn);
            sidePanel.Controls.Add(consultaMiembroBtn);
            sidePanel.Controls.Add(registroClienteBtn);
            sidePanel.Controls.Add(ventaBtn);
            sidePanel.Controls.Add(inventarioBtn);
            sidePanel.Controls.Add(cerrarSesionLbl);
        }
        private void SubBotones()
        {
            tiposMembresiaBtn = new Button();
            tiposMembresiaBtn.Text = "TIPO MEMBRESÍA";
            tiposMembresiaBtn.Size = new Size(150, 50);
            tiposMembresiaBtn.Location = new Point(0, 0);
            tiposMembresiaBtn.Font = new Font("Race Sport", 10, FontStyle.Bold);
            tiposMembresiaBtn.ForeColor = Color.White;
            tiposMembresiaBtn.FlatStyle = FlatStyle.Flat;
            tiposMembresiaBtn.Cursor = Cursors.Hand;
            tiposMembresiaBtn.BackColor = negro;

            productosBtn = new Button();
            productosBtn.Text = "PRODUCTOS";
            productosBtn.Size = new Size(150, 50);
            productosBtn.Location = new Point(0, 50);
            productosBtn.Font = new Font("Race Sport", 10, FontStyle.Bold);
            productosBtn.ForeColor = Color.White;
            productosBtn.FlatStyle = FlatStyle.Flat;
            productosBtn.Cursor = Cursors.Hand;
            productosBtn.BackColor = negro;

            registroVentaBtn = new Button();
            registroVentaBtn.Text = "Registro Ventas";
            registroVentaBtn.Size = new Size(150, 50);
            registroVentaBtn.Location = new Point(0, 100);
            registroVentaBtn.Font = new Font("Race Sport", 10, FontStyle.Bold);
            registroVentaBtn.ForeColor = Color.White;
            registroVentaBtn.FlatStyle= FlatStyle.Flat;
            registroVentaBtn.Cursor = Cursors.Hand;
            registroVentaBtn.BackColor = negro;

            subBotonesPanel.Controls.Add(tiposMembresiaBtn);
            subBotonesPanel.Controls.Add(productosBtn);
            subBotonesPanel.Controls.Add(registroVentaBtn);
            this.Controls.Add(subBotonesPanel);

            inventarioBtn.MouseEnter += (s, e) => {
                subBotonesPanel.Visible = true;
                subBotonesPanel.BringToFront();
                checarCursor.Start();
            };
            checarCursor.Interval = 100;
            checarCursor.Tick += (s, e) =>
            {
                if (!inventarioBtn.Bounds.Contains(this.PointToClient(Cursor.Position)) &&
                        !subBotonesPanel.Bounds.Contains(this.PointToClient(Cursor.Position)))
                {
                    subBotonesPanel.Visible = false;
                }
            };
        }

    }
}
