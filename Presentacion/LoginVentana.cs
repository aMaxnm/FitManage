using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Negocio;

namespace Presentacion
{
    public class LoginVentana : Form
    {
        private TextBox txtUsuario;
        private TextBox txtContraseña;
        private Button btnLogin;
        private Label lblMensaje;

        public LoginVentana()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Login - Home Gym";
            this.Size = new Size(900, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Imagen de fondo
            string rutaImagen = Path.Combine(Application.StartupPath, "Recursos", "login.PNG");
            if (File.Exists(rutaImagen))
            {
                this.BackgroundImage = Image.FromFile(rutaImagen);
                this.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else
            {
                MessageBox.Show("No se encontró la imagen de fondo en: " + rutaImagen);
            }
 
            // Etiqueta Usuario
            Label lblUsuario = new Label();
            lblUsuario.Text = "Usuario";
            lblUsuario.ForeColor = Color.Black;
            lblUsuario.BackColor = Color.Transparent;
            lblUsuario.Font = new Font("Race sport", 10, FontStyle.Bold);
            lblUsuario.Location = new Point(400, 245);
            lblUsuario.Size = new Size(110, 15);
            this.Controls.Add(lblUsuario);

            // TextBox Usuario
            txtUsuario = new TextBox();
            txtUsuario.Location = new Point(345, 270);
            txtUsuario.Size = new Size(200, 25);
            this.Controls.Add(txtUsuario);

            // Etiqueta Contraseña
            Label lblContraseña = new Label();
            lblContraseña.Text = "Contraseña";
            lblContraseña.ForeColor = Color.Black;
            lblContraseña.BackColor = Color.Transparent;
            lblContraseña.Font = new Font("Race sport", 10, FontStyle.Bold);
            lblContraseña.Location = new Point(375, 295);
            lblContraseña.Size = new Size(155, 15);
            this.Controls.Add(lblContraseña);

            // TextBox Contraseña
            txtContraseña = new TextBox();
            txtContraseña.Location = new Point(345, 320);
            txtContraseña.Size = new Size(200, 25);
            txtContraseña.UseSystemPasswordChar = true;
            this.Controls.Add(txtContraseña);

            // Label de mensaje
            lblMensaje = new Label();
            lblMensaje.ForeColor = Color.Black;
            lblMensaje.BackColor = Color.Transparent;
            lblMensaje.Font = new Font("Futura", 12, FontStyle.Bold);
            lblMensaje.Location = new Point(305, 395);
            lblMensaje.Size = new Size(320, 30);
            lblMensaje.Text = "";  // Vacío inicialmente
            this.Controls.Add(lblMensaje);


            // Botón Login
            btnLogin = new Button();
            btnLogin.Text = "Iniciar Sesión";
            btnLogin.Font = new Font("Race Sport", 10, FontStyle.Bold);
            btnLogin.BackColor = Color.DarkRed;
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(395, 350);
            btnLogin.Size = new Size(100, 35);
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Click += BtnLogin_Click;
            this.Controls.Add(btnLogin);
        }
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text;
            string contraseña = txtContraseña.Text;

            bool isVacios = String.IsNullOrEmpty(usuario) || String.IsNullOrEmpty(contraseña);

            try
            {
                bool verificar = new UsuarioServicio().VerificarUsuario(usuario, contraseña);
                if (!isVacios)
                {
                    if (verificar)
                    {
                        VentanaPrincipal ventanaPrincipal = new VentanaPrincipal();
                        ventanaPrincipal.Show();
                        this.Hide();
                    }
                    else
                    {
                        lblMensaje.Text = "Usuario o contraseña incorrectos.";
                    }
                }
                else
                {
                    lblMensaje.Text = "Complete todos los Campos.";
                }
                
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error de conexión: " + ex.Message;
            }
        }
    }
}
