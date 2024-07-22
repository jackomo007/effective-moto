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
    public partial class nuevo_usuario : Form
    {
        public nuevo_usuario()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Usuario.NewUsuario(int.Parse(textBox1.Text), textBox2.Text, textBox3.Text))
                    MessageBox.Show("usuario registrado con exito");
                else
                    MessageBox.Show("error al registrar usuario, intente mas tarde");
            }
            catch(Exception ex)
            {
                MessageBox.Show("error al registrar usuario " + ex.Message);
            }    
        }
    }
}
