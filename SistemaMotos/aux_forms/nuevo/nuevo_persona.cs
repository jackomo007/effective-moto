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
    public partial class nuevo_persona : Form
    {
        public nuevo_persona()
        {
            InitializeComponent();
            CB_vehiculos();
        }
        private void CB_vehiculos()
        {
            try
            {
                DataTable res = Vehiculo.GetAllVehiculo();

                if (res == null)
                    return;


                comboBox1.DataSource = res;
                comboBox1.ValueMember = "ID_VEHICULO";
                comboBox1.DisplayMember = "NOM_VEHICULO";

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
                if (Persona.NewPersona(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text, textBox8.Text, int.Parse(comboBox1.SelectedValue.ToString())))
                {
                    credenciales form = new credenciales(textBox2.Text);
                    form.ShowDialog();
                }
                else
                    MessageBox.Show("error al registar persona, intente mas tarde");
            }
            catch (Exception ex)
            {
                MessageBox.Show("error al registrar persona " + ex.Message);
            }
        }
    }
}
