using SistemaMotos.aux_forms;
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

namespace SistemaMotos
{
    public partial class Form_login : Form
    {
        public Form_login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Usuario usuario;

            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
                return;

            string nom = textBox1.Text;
            string password = textBox2.Text;

            try
            {
                usuario = Usuario.GetUsuario(nom, password);

                if (usuario == null)
                {
                    MessageBox.Show("Usuario o contraseña incorrecta", "Error de credenciales", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Form_principal form = new Form_principal();
                form.FormClosed += Logout;
                form.Show();
                Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error con la base de datos: " + ex.Message, "Error de Base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Logout(object sender, FormClosedEventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;

            ActiveControl = textBox1;
            Show();
        }

        private void Form_login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.Alt | Keys.C))
            {
                Form_config form = new Form_config();
                form.ShowDialog();
            }
        }
    }
}
