using SistemaMotos.Clases.entidades;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;

namespace SistemaMotos.aux_forms
{
    public partial class reg_nueva_salida : Form
    {
        private FilterInfoCollection captureDevice;
        private VideoCaptureDevice finalFrame;

        public reg_nueva_salida()
        {
            InitializeComponent();
            textBox1.Select();
            this.ActiveControl = textBox1;
            textBox1.Focus();

            captureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            finalFrame = new VideoCaptureDevice(captureDevice[0].MonikerString);
            finalFrame.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);
        }

        private void btnStartScan_Click(object sender, EventArgs e)
        {
            finalFrame.Start();
        }

        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            var barcodeReader = new BarcodeReader();
            var result = barcodeReader.Decode((Bitmap)eventArgs.Frame.Clone());

            if (result != null)
            {
                textBox1.Invoke(new MethodInvoker(delegate ()
                {
                    textBox1.Text = result.Text;
                    HandleQrCodeScanned(result.Text);
                }));

                finalFrame.SignalToStop();
            }
        }

        private void HandleQrCodeScanned(string matricula)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(HandleQrCodeScanned), matricula);
                return;
            }

            textBox1.Text = string.Empty;
            textBox1.Enabled = false;

            try
            {
                DateTime fechaHora = DateTime.Now;
                if (RegistroSalida.NewRegistroSalida(fechaHora, matricula))
                {
                    DataTable reg = Persona.GetDatosPersonaVehiculo(matricula);

                    lb_msg.Text = "Salida registrada con éxito";

                    lb_nom.Text = (string)reg.Rows[0]["NOM_PERSONA"];
                    lb_matricula.Text = (string)reg.Rows[0]["MATRICULA"];
                    lb_grado.Text = (string)reg.Rows[0]["GRAD_GRUPO"];
                    lb_vehiculo.Text = (string)reg.Rows[0]["NOM_VEHICULO"];
                    lb_placa.Text = (string)reg.Rows[0]["PLACAS"];
                    lb_noEst.Text = (string)reg.Rows[0]["NO_EST"];
                    lb_hora.Text = fechaHora.ToString("dd - MM - yyyy HH:mm:ss");

                    textBox1.Enabled = true;
                    textBox1.Focus();
                }
                else
                {
                    lb_msg.Text = "Error al registrar la salida";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar salida: " + ex.Message);
            }
        }

        private void reg_nueva_salida_Load(object sender, EventArgs e)
        {
            ActiveControl = textBox1;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
