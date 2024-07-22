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
    public partial class buscar_usuario : Form
    {
        public buscar_usuario()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Usuario.UpdateUsuarioData(int.Parse(textBox1.Text), textBox2.Text, textBox3.Text))
                    MessageBox.Show("usuario actualizado con exito");
                else
                    MessageBox.Show("error al actualizar usuario, intente mas tarde");
            }
            catch (Exception ex)
            {
                MessageBox.Show("error al actualizar usuario " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Usuario user = null;
            try
            {
                if (!string.IsNullOrEmpty(textBox1.Text))
                    user = Usuario.GetUsuario(int.Parse(textBox1.Text));

                if (user != null)
                {
                    textBox2.Text = user.Nombre;
                    textBox3.Text = user.Password;
                }

                    
            }
            catch (Exception ex)
            {
                MessageBox.Show("error al buscar usuario " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text)) return;

            try
            {
                if (Usuario.DeleteUsuario(int.Parse(textBox1.Text)))
                    MessageBox.Show("Usuario eliminado con exito");
                else
                    MessageBox.Show("error al eliminar usuario ");




            }
            catch (Exception ex)
            {
                MessageBox.Show("error al eliminar usuario " + ex.Message);
            }
        }
    }
}
