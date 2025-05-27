using System.Drawing;
using System.Windows.Forms;

namespace Presentacion
{
    internal class RegistroVentas : Panel
    {
        private TextBox reporteVentas;
        public RegistroVentas()
        {
            InicializarComponentes();
        }
        private void InicializarComponentes()
        {
            this.Size = new Size(2000, 1010);
            this.BackColor = Color.WhiteSmoke;

            //Titulo
            Label tituloLbl = new Label();
            tituloLbl.Text = "REGISTRO VENTAS";
            tituloLbl.Font = new Font("Race Sport", 50, FontStyle.Bold);
            tituloLbl.AutoSize = true;
            tituloLbl.Location = new Point(475, 10);
            this.Controls.Add(tituloLbl);

            //TextBox
            TextBox registroTxt = new TextBox();
            registroTxt.Multiline = true;
            registroTxt.Size = new Size(700, 500);
            registroTxt.Location = new Point(500,150);
            registroTxt.ScrollBars = ScrollBars.Vertical;  // Agrega una barra de desplazamiento vertical
            registroTxt.ReadOnly = true;
            this.Controls.Add(registroTxt);
        }
    }
}
