using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace recorder
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            txtInfo.Text = "";
            if (Properties.Settings.Default.p_device == 0)
            {
                txtInfo.Text += "Устройство по умолчанию" + Environment.NewLine;
            }
            else if (Properties.Settings.Default.p_device > 0)
            {
                txtInfo.Text += "Устройство номер: " + Properties.Settings.Default.p_device + Environment.NewLine;
            }
            if (Properties.Settings.Default.p_cut == 0)
            {
                txtInfo.Text += "Файл не режется" + Environment.NewLine;
            }
            else if (Properties.Settings.Default.p_cut > 0)
            {
                txtInfo.Text += "Новый файл каждые " + Properties.Settings.Default.p_cut + " секунд" + Environment.NewLine;
            }
            if (Properties.Settings.Default.p_dir.Length > 0)
            {
                txtInfo.Text += "Директория для записей: " + Properties.Settings.Default.p_dir;
            }
        }
    }
}
