using SistemaMotos.Properties;
using System;
using System.Windows.Forms;

namespace SistemaMotos.aux_forms
{
    public partial class Form_config : Form
    {
        public Form_config()
        {
            InitializeComponent();
            textBox1.Text = Settings.Default.dbFilePath;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Settings.Default.dbFilePath = textBox1.Text;
            Settings.Default.Save();

            Application.Restart();
        }
    }
}
