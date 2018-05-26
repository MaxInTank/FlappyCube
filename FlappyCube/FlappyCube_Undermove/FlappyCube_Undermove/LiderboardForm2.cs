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
    public partial class LiderboardForm2 : Form
    {
        int _currentscore = 0;
        public LiderboardForm2(int currentscore)
        {
            InitializeComponent();
            _currentscore = currentscore;
            try
            {
 textBox1.Text=  File.ReadAllText("Liaders");
            }
            catch(FileNotFoundException)
            {
                File.Create("Liaders");
            }
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.AppendText(textBox2.Text+":"+_currentscore.ToString()+Environment.NewLine);
        }

        private void LiderboardForm2_FormClosing(object sender, FormClosingEventArgs e)
        {
            File.WriteAllText("Liaders",textBox1.Text);
        }
    }
}
