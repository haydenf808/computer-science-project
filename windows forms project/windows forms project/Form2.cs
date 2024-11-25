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
    public partial class Form2 : Form
    {
        private char[,] bigBoard = new char[3, 3];
        private char[,] smallBoard = new char[3, 3];
        private bool isPlayerXTurn = true;
        private char[,] bigBoardbuttons = new char[3, 3];
        private char[,] smallBoardbuttons = new char[3, 3];
        private bool isrobotenabled = true;

        public Form2()
        {
            InitializeComponent();
            InitializeGame();

        }
        private void InitializeGame()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    bigBoard[i, j] = '';
                    bigBoardbuttons[i, j] = new Buttons();
                    {

                    }
                    //Kylo sucks
                    int[,] smallBoard = new int[3, 2];
                    int[,] smallBoardbuttons = new int[3, 2] { { 1, 1 }, { 1, 2 }, { 1, 3 }, };
                                                             

                    Random r = new Random();
                    smallBoard[i, j] = r.Next();



                }


                private void button82_Click(object sender, EventArgs e)
                {

                    Form1 form1 = new Form1();
                    form1.Show();
                    this.Hide();

                }
            }
        }
    }
}
