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

namespace SistemaMotos.aux_forms.buscar
{
    public partial class buscar_vehiculo : Form
    {
        public buscar_vehiculo()
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

        private void button3_Click(object sender, EventArgs e)
        {
            Vehiculo vehiculo = null;
            try
            {
                if (!string.IsNullOrEmpty(textBox4.Text))
                    vehiculo = Vehiculo.GetVehiculo(textBox4.Text);

                if (vehiculo != null)
                {
                    textBox1.Text = vehiculo.NOM_VEHICULO;
                    textBox2.Text = vehiculo.MODELO_VEHICULO;
                    textBox3.Text = vehiculo.MARCA;
                    textBox5.Text = vehiculo.NIV;
                    comboBox1.SelectedValue = vehiculo.ID_DOC;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("error al buscar vehiculo " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox4.Text)) return;

            try
            {
                if (Vehiculo.DeleteVehiculo(textBox4.Text))
                    MessageBox.Show("Vehiculo eliminado con exito");
                else
                    MessageBox.Show("error al eliminar Vehiculo ");

            }
            catch (Exception ex)
            {
                MessageBox.Show("error al eliminar Vehiculo " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Vehiculo.UpdateVehiculoData(textBox4.Text, textBox1.Text, textBox2.Text, textBox3.Text, textBox5.Text, int.Parse(comboBox1.SelectedValue.ToString())))
                    MessageBox.Show("usuario actualizado con exito");
                else
                    MessageBox.Show("error al actualizar usuario, intente mas tarde");
            }
            catch (Exception ex)
            {
                MessageBox.Show("error al actualizar usuario " + ex.Message);
            }
        }
    }
}
