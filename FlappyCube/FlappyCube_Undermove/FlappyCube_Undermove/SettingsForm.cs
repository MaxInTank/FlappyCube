using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlappyCube_Undermove
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] settings = new string[3];

            settings[0] = trackBar1.Value.ToString();
            settings[2] = checkBox1.Checked.ToString();
            settings[1] = trackBar2.Value.ToString();


            File.WriteAllLines("settings",settings);
            Close();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }
    }
}
