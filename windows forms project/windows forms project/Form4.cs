using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace windows_forms_project
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int flipResult = random.Next(2);
            if (flipResult == 0)
            {
                button1.Text = "You are Player X!";
            }
            else
            {
                button1.Text = "You are Player O!";
            }
        }
    }
}