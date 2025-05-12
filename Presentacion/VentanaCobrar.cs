using Entidad;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    internal class VentanaCobrar : Form
    {
        public VentanaCobrar(String nombreMembresia, Decimal precioMembresia)
        {
            this.Text = "Cobro";
            this.Size = new Size(700, 400);
            //this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.DarkGray;


            //Labels necesarias para la ventana de cobro
            Label membresiaLbl, mesLbl, costoLbl, recibidoLbl, cambioLbl;
            //TextBoxs necesarias para la ventana de cobro
            TextBox costoTxt, recibidoTxt, cambioTxt;
            //Botones necesarios para la ventana de cobro
            Button confirmarBtn, cancelarBtn;

            //Configuracion de las labels
            membresiaLbl = new Label();
            membresiaLbl.Text = "MEMBRESIA:";
            membresiaLbl.Font = new Font("Tahoma", 19);
            membresiaLbl.ForeColor = Color.Black;
            membresiaLbl.Location = new Point(230, 30);
            membresiaLbl.AutoSize = true;

            mesLbl = new Label();
            mesLbl.Text = nombreMembresia;
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
            costoTxt.Text = precioMembresia.ToString();
            costoTxt.Location = new Point(300, 112);
            costoTxt.Size = new Size(200, 30);
            costoTxt.Font = new Font("Tahoma", 14);
            costoTxt.ForeColor = Color.Black;
            costoTxt.BorderStyle = BorderStyle.FixedSingle;
            costoTxt.ReadOnly = true;

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
            confirmarBtn.Location = new Point(390, 300);
            confirmarBtn.Size = new Size(160, 40);
            confirmarBtn.BackColor = Color.DarkGreen; //ForestGreen
            confirmarBtn.FlatStyle = FlatStyle.Flat;
            confirmarBtn.FlatAppearance.BorderSize = 0;
            confirmarBtn.Cursor = Cursors.Hand;
            confirmarBtn.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(costoTxt.Text) || string.IsNullOrWhiteSpace(recibidoTxt.Text))
                {
                    MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    validarCobro(cambioTxt.Text);
                    this.Close();
                }
            };

            cancelarBtn = new Button();
            cancelarBtn.Text = "CANCELAR";
            cancelarBtn.Font = new Font("Tahoma", 14, FontStyle.Bold);
            cancelarBtn.ForeColor = Color.White;
            cancelarBtn.Location = new Point(150, 300);
            cancelarBtn.Size = new Size(160, 40);
            cancelarBtn.BackColor = Color.DarkRed; //ForestGreen
            cancelarBtn.FlatStyle = FlatStyle.Flat;
            cancelarBtn.FlatAppearance.BorderSize = 0;
            cancelarBtn.Cursor = Cursors.Hand;
            cancelarBtn.Click += (s, e) => this.Close(); // Cierra la ventana al hacer clic

            //this.Controls.Add(tituloLbl);
            this.Controls.Add(membresiaLbl);
            this.Controls.Add(mesLbl);
            this.Controls.Add(costoLbl);
            this.Controls.Add(recibidoLbl);
            this.Controls.Add(cambioLbl);
            this.Controls.Add(costoTxt);
            this.Controls.Add(recibidoTxt);
            this.Controls.Add(cambioTxt);
            this.Controls.Add(confirmarBtn);
            this.Controls.Add(cancelarBtn);

            //El TextBox de cambio se calcula automaticamente
            recibidoTxt.TextChanged += (s, e) =>
            {
                if (float.TryParse(recibidoTxt.Text, out float recibido) && float.TryParse(costoTxt.Text, out float costo))
                {
                    float cambio = recibido - costo;
                    cambioTxt.Text = cambio.ToString("0.00");
                }
                else
                {
                    cambioTxt.Text = "0.00";
                }
            };
        }
        public void validarCobro(String cambio)
        {
            float validar = float.Parse(cambio);
            if (validar > 0.00)
            {
                MessageBox.Show("Cobro realizado con exito", "Cobro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
