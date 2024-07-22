using Gma.QrCodeNet.Encoding.Windows.Render;
using Gma.QrCodeNet.Encoding;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaMotos.Clases.entidades;
using DocumentFormat.OpenXml.Presentation;

namespace SistemaMotos.aux_forms
{
    public partial class credenciales : Form
    {
        DataTable datos = null;
        string path = string.Empty;
        public credenciales( string matricula)
        {
            InitializeComponent();
            datos = Persona.GetDatosPersonaVehiculo(matricula);
            MostarCredencial();
        }

        private System.Drawing.Image GenerarQR(string noCredencial, Size tam)
        {
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            GraphicsRenderer renderer = new GraphicsRenderer(new FixedCodeSize(400, QuietZoneModules.Zero), Brushes.Black, Brushes.White);
            MemoryStream ms = new MemoryStream();

            qrEncoder.TryEncode(noCredencial, out QrCode qrCode);
            renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);
            Bitmap imagenTemporal = new Bitmap(ms);
            Bitmap imagen = new Bitmap(imagenTemporal, tam);
            return imagen;
        }
        public void GenerarPDF(Bitmap[] credTutores)
        {
            iTextSharp.text.Image[] imgTutores = new iTextSharp.text.Image[credTutores.Length];

            Document doc = new Document(PageSize.LETTER);
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(path, FileMode.OpenOrCreate));

            doc.AddTitle("Credencial PDF");
            doc.Open();

            for (int i = 0; i < imgTutores.Length; i++)
            {
                imgTutores[i] = Imagenes.ImgToPDF(credTutores[i]);
                imgTutores[i].Alignment = Element.ALIGN_CENTER;
                imgTutores[i].ScalePercent(80);
                doc.Add(new Paragraph(""));

                doc.Add(imgTutores[i]);
            }

            doc.Close();
            writer.Close();
            try
            {
                if (File.Exists(path))
                {
                    var p = new Process
                    {
                        StartInfo = new ProcessStartInfo(path)
                        {
                            UseShellExecute = true
                        }
                    };

                    p.Start();
                }
                else
                {
                    MessageBox.Show("Error al generar PDF, intente mas tarde", "Archivo no generado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show("Error al generar PDF, intente mas tarde" + e.Message, "Archivo no generado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MostarCredencial()
        {
            if (datos == null) return;

            pb_QR.Image = GenerarQR((string)datos.Rows[0]["MATRICULA"], pb_QR.Size);
            lb_noCredencial.Text = (string)datos.Rows[0]["MATRICULA"];
            lb_nom.Text = (string)datos.Rows[0]["NOM_PERSONA"];
            lb_grado.Text = (string)datos.Rows[0]["GRAD_GRUPO"]; 

            lb_vehiculo.Text = (string)datos.Rows[0]["NOM_VEHICULO"];
            lb_placa.Text = (string)datos.Rows[0]["PLACAS"];
            lb_noEst.Text = (string)datos.Rows[0]["NO_EST"];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap[] credenciales = new Bitmap[1];

            credenciales[0] = new Bitmap(panel1.Width, panel1.Height);

            panel1.DrawToBitmap(credenciales[0], new System.Drawing.Rectangle(0, 0, panel1.Width, panel1.Height));

            //pedimos en que ruta es donde vamos a guardar las credenciales generadas
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Filter = "PDF document|*.pdf",
                Title = "Guardando credencial generada",
                FileName = "Credencial de motociclista",
                RestoreDirectory = true
            };

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(saveFileDialog1.FileName))
                {
                    path = saveFileDialog1.FileName;
                    GenerarPDF(credenciales);
                }
            }
        }
    }
}
