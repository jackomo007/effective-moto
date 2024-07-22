using DocumentFormat.OpenXml.Bibliography;
using Org.BouncyCastle.Bcpg.OpenPgp;
using SistemaMotos.Clases.entidades;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SistemaMotos.aux_forms
{
    public partial class reg_entradas : Form
    {
        public reg_entradas()
        {
            InitializeComponent();
            cargarTabla();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path;
            DateTime fecha = dateTimePicker1.Value;
            if (dataGridView1.Rows.Count == 0) return;

            DataTable reporte = RegistroEntrada.AllRegistroEntrada(fecha);
            //pedimos en que ruta es donde vamos a guardar las credenciales generadas
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Filter = "Libro de Excel (*.xlsx)|*.xlsx",
                Title = "Guardando archivo de registro",
                FileName = string.Format("Reporte de asistencia {0}", fecha.ToString("yyyy-MM-dd")),
                RestoreDirectory = true
            };

            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
            if (string.IsNullOrEmpty(saveFileDialog1.FileName)) return;
            path = saveFileDialog1.FileName;

            //pasar las tablas al excel
            SLDocument doc = new SLDocument();

            doc.AddWorksheet("Reporte");

            //ordenamos tabla en base al nombre del infante
            doc.ImportDataTable("A4", reporte, true);

            doc.DeleteWorksheet("Sheet1");
            doc.SaveAs(path);

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
                    MessageBox.Show("Error al generar Excel, intente mas tarde", "Archivo no generado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("Error al generar Excel, intente mas tarde" + ex.Message, "Archivo no generado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void cargarTabla()
        {
            try
            {
                DataTable res = RegistroEntrada.AllRegistroEntrada(DateTime.Today);

                if (res == null)
                {
                    dataGridView1.DataSource = null;
                    MessageBox.Show("no hay registros de entrada el dia de hoy");
                    return;
                }

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = res;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error con la base de datos: " + ex.Message, "Error de Base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime fecha = dateTimePicker1.Value;

                DataTable res = RegistroEntrada.AllRegistroEntrada(fecha);

                if (res == null)
                {
                    dataGridView1.DataSource = null;
                    MessageBox.Show("no hay registros de entrada el dia de hoy");
                    return;
                }

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = res;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error con la base de datos: " + ex.Message, "Error de Base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
