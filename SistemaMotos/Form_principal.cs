using SistemaMotos.aux_forms;
using SistemaMotos.main_forms;
using System;
using System.Windows.Forms;

namespace SistemaMotos
{
    public partial class Form_principal : Form
    {
        private Form currentChildForm;
        public Form_principal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new main_usuarios());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new main_vehiculos());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenChildForm(new main_personas());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenChildForm(new main_entradas_salidas());
        }

        public void OpenChildForm(Form childForm)
        {
            //open only form
            currentChildForm?.Close();

            currentChildForm = childForm;
            //End
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("¿Esta seguro que quiere cerrar sesion?", "Precaucion", MessageBoxButtons.YesNo);

            if (res == DialogResult.Yes)
                Close();
        }

        private void Form_principal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.Alt | Keys.C))
            {
                Form_config form = new Form_config();
                form.ShowDialog();
            }
        }
    }
}
