using SistemaMotos.aux_forms;
using System;
using System.Windows.Forms;

namespace SistemaMotos.main_forms
{
    public partial class main_entradas_salidas : Form
    {
        private Form currentChildForm;
        public main_entradas_salidas()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new reg_entradas());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new reg_salida());
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

        private void button3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new reg_nueva_salida());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenChildForm(new reg_nueva_entrada());
        }
    }
}
