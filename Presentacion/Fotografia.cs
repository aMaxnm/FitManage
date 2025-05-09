using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using AForge.Video.DirectShow;
using System.Drawing.Imaging;
using System.Drawing;
using AForge.Video;
using System.Windows.Forms;
using Presentación;


namespace Presentacion
{
    internal class Fotografia
    { 
        public static string Ruta = "C:/Users/amnm0/OneDrive/Documents/GitHub/FitManage/Presentacion/Recursos/Fotos";
        private static bool hayDispositivos;
        public static FilterInfoCollection misDispositivos;
        public static VideoCaptureDevice miCamara;
        
        public static void CargarDispositivos()
        {
            misDispositivos = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (misDispositivos.Count > 0)
            {
                hayDispositivos = true;
                for (int i = 0; i < misDispositivos.Count; i++)
                {
                    PanelManager.dispositivosCombo.Items.Add(misDispositivos[i].Name.ToString());
                    PanelManager.dispositivosCombo.Text = misDispositivos[0].Name.ToString();
                }
            }
            else
            {
                hayDispositivos = false;
            }
        }
        public static void AbrirCamara_Click(object sender, EventArgs e)
        {
            // Buscar la resolución deseada
            int i = PanelManager.dispositivosCombo.SelectedIndex;
            string nombreVideo = misDispositivos[i].MonikerString;
            miCamara = new VideoCaptureDevice(nombreVideo);
            miCamara.NewFrame += new NewFrameEventHandler(Capturando);
            miCamara.Start();
        }
        public static void Capturando(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap foto = (Bitmap)eventArgs.Frame.Clone();
            PanelManager.fotoMiembroPct.Image = foto;
        }
        public static void CerrarCamara()
        {
            if (miCamara != null && miCamara.IsRunning)
            {
                miCamara.SignalToStop();
                miCamara = null;
            }
        }
        public static void TomarBtn_Click(object sender, EventArgs e)
        {
            string nombre = PanelManager.nombreTxt.Text;
            if (miCamara != null && miCamara.IsRunning)
            {

                PanelManager.fotoMiembroPct.Image = PanelManager.fotoMiembroPct.Image;
                string rutaCompleta = Path.Combine(Ruta, $"{nombre}.jpg");
                PanelManager.fotoMiembroPct.Image.Save(rutaCompleta, ImageFormat.Jpeg);
                CerrarCamara();
            }
        }
        public static void RetomarBtn_Click(object sender, EventArgs e)
        {
            int i = PanelManager.dispositivosCombo.SelectedIndex;
            string nombreVideo = misDispositivos[i].MonikerString;
            miCamara = new VideoCaptureDevice(nombreVideo);
            miCamara.NewFrame += new NewFrameEventHandler(Capturando);
            miCamara.Start();
        }
        public static void ImportarBtn_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Archivos JPG (*.jpg)|*.jpg|Archivos PNG (*.png)|*.png";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string imageLocation = dialog.FileName;
                    string nombre = PanelManager.nombreTxt.Text.Trim();
                    using (System.Drawing.Image imagen = System.Drawing.Image.FromFile(imageLocation))
                    {
                        // Mostrarla en el panel
                        PanelManager.fotoMiembroPct.Image = (System.Drawing.Image)imagen.Clone();

                        // Guardarla en la ruta
                        string rutaCompleta = Path.Combine(Ruta, $"{nombre}.jpg");

                        // Asegúrate de que la carpeta existe
                        if (!Directory.Exists(Ruta))
                            Directory.CreateDirectory(Ruta);

                        imagen.Save(rutaCompleta, ImageFormat.Jpeg);
                    }

                    MessageBox.Show("Imagen importada y guardada exitosamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
