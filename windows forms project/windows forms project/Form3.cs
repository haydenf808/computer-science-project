using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace windows_forms_project
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();

            this.ActiveControl = label1;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
