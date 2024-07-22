using SistemaMotos.Clases.entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaMotos.aux_forms.nuevo
{
    public partial class nuevo_vehiculo : Form
    {
        public nuevo_vehiculo()
        {
            InitializeComponent();
            CB_docs();
        }

        private void CB_docs()
        {
            try
            {
                DataTable res = Documento.getAllDocumentos();

                if (res == null)
                    return;


                comboBox1.DataSource = res;
                comboBox1.ValueMember = "ID_DOC";
                comboBox1.DisplayMember = "DESCRIPCION";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error con la base de datos: " + ex.Message, "Error de Base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Vehiculo.NewVehiculo(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, int.Parse(comboBox1.SelectedValue.ToString())))
                    MessageBox.Show("vehiculo registrado con exito");
                else
                    MessageBox.Show("error al vehiculo usuario, intente mas tarde");
            }
            catch (Exception ex)
            {
                MessageBox.Show("error al registrar vehiculo " + ex.Message);
            }
        }
    }
}
