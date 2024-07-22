﻿using SistemaMotos.aux_forms.buscar;
using SistemaMotos.aux_forms.nuevo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaMotos.main_forms
{
    public partial class main_vehiculos : Form
    {
        private Form currentChildForm;
        public main_vehiculos()
        {
            InitializeComponent();
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

        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new nuevo_vehiculo());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new buscar_vehiculo());
        }
    }
}
