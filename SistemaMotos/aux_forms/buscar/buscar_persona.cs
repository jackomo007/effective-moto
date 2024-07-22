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
    public partial class buscar_persona : Form
    {
        public buscar_persona()
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
        private void button2_Click(object sender, EventArgs e)
        {
            Persona persona = null;
            try
            {
                if (!string.IsNullOrEmpty(textBox2.Text))
                    persona = Persona.GetPersona(textBox2.Text);

                if (persona != null)
                {
                    textBox1.Text = persona.Nombre;
                    textBox3.Text = persona.NSS;
                    textBox4.Text = persona.CURP;
                    textBox5.Text = persona.TEL;
                    textBox6.Text = persona.DIR;
                    textBox7.Text = persona.GRAD_GRUPO;
                    textBox8.Text = persona.NO_EST;
                    comboBox1.SelectedValue = persona.ID_VEHICULO;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error al buscar persona " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (Persona.UpdatePersonaData(textBox2.Text, textBox1.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text,textBox8.Text, int.Parse(comboBox1.SelectedValue.ToString())))
                    MessageBox.Show("persona actualizado con exito");
                else
                    MessageBox.Show("error al actualizar persona, intente mas tarde");
            }
            catch (Exception ex)
            {
                MessageBox.Show("error al actualizar persona " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text)) return;

            try
            {
                if (Persona.DeletePersona(textBox2.Text))
                    MessageBox.Show("Persona eliminado con exito");
                else
                    MessageBox.Show("error al eliminar Persona ");

            }
            catch (Exception ex)
            {
                MessageBox.Show("error al eliminar Persona " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text)) return;

            credenciales form = new credenciales(textBox2.Text);
            form.ShowDialog();
        }
    }
}
