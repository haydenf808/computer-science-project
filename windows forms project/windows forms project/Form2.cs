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
        Button[,] buttons = new Button[9, 9];
        string[,] smallGrid = new string[3, 3];
        string currentPlayer = "X";
        int currentSmallgrid = -1;
        TextBox PlayersTurnTextBox;

        public Form2()
        {
            InitializeComponent();
            Board();
            PlayersTextBox();
        }

        void PlayersTextBox()
        {
            PlayersTurnTextBox = new TextBox
            {
                Size = new Size(200, 30),
                Location = new Point((this.ClientSize.Width - 200) / 2, this.ClientSize.Height - 700),
                Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold),
                ReadOnly = true,
                Text = "Player X's turn"
            };
            Controls.Add(PlayersTurnTextBox); //textBox to show player turn
        }

        void Board()
        {
            int buttonSize = 50;
            int gridWidth = buttonSize * 9; //9 buttons per row
            int gridHeight = buttonSize * 9; //9 buttons per column

            //center the board
            int offsetX = (this.ClientSize.Width - gridWidth) / 2;
            int offsetY = (this.ClientSize.Height - gridHeight) / 2;

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    Button button = new Button
                    {
                        Size = new Size(buttonSize, buttonSize),
                        Location = new Point(offsetX + col * buttonSize, offsetY + row * buttonSize),
                        Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold),
                        Tag = new int[] { row, col },
                        BackColor = Color.White
                    };
                    button.Click += Button_Click;
                    buttons[row, col] = button;
                    Controls.Add(button);
                }
            } //create board and iterate through it to make buttons
        }

        void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null && string.IsNullOrEmpty(clickedButton.Text))
            {

                int[] position = (int[])clickedButton.Tag;
                int row = position[0];
                int col = position[1];

                //check if the move is in the current small grid
                if (currentSmallgrid != -1 && !IsInCurrentSmallgrid(row, col))
                {
                    MessageBox.Show("You have to play in the current smallgrid");
                    return;
                }

                //make the move
                clickedButton.Text = currentPlayer;
                clickedButton.Enabled = false;

                //check if the small grid has been won
                if (CheckSmallgridWin(row / 3, col / 3))
                {
                    smallGrid[row / 3, col / 3] = currentPlayer;
                    MarkSmallgrid(row / 3, col / 3, currentPlayer);
                }

                //check for overall win
                if (CheckOverallWin())
                {
                    MessageBox.Show($"Player {currentPlayer} wins the game!");
                    ResetBoard();
                    return;
                }

                //decide next current small grid
                currentSmallgrid = GetSmallgrid(row % 3, col % 3);

                //check if small grid is full 
                if (smallGrid[row % 3, col % 3] != null || IsSmallgridFull(currentSmallgrid))
                {
                    currentSmallgrid = -1; //Allow any move
                }

                //switch player
                currentPlayer = currentPlayer == "X" ? "O" : "X";
                Updateboard();

                //update the current players turn in the TextBox
                PlayersTurnTextBox.Text = $"Player {currentPlayer}'s turn";
            }
        }

        void Updateboard()
        {
            Text = $"Player {currentPlayer}'s turn";

            //mark the current small grid 
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (currentSmallgrid == -1 || IsInCurrentSmallgrid(row, col))
                    {
                        buttons[row, col].BackColor = Color.LightYellow;
                    }
                    else
                    {
                        buttons[row, col].BackColor = Color.White;
                    }
                }
            }
        }
        //checks if selected row and column is in current small grid so the player makes the correct move 
        bool IsInCurrentSmallgrid(int row, int col)
        {
            int smallgridRow = row / 3;
            int smallgridCol = col / 3;
            return GetSmallgrid(smallgridRow, smallgridCol) == currentSmallgrid;
        }
        //combine row and column into a single number 
        int GetSmallgrid(int smallgridRow, int smallgridCol)
        {
            return smallgridRow * 3 + smallgridCol;
        }

        bool CheckSmallgridWin(int smallgridRow, int smallgridCol)
        {
            //check rows, columns, and diagonals within the small grid 
            for (int i = 0; i < 3; i++)
            {
                if (buttons[smallgridRow * 3 + i, smallgridCol * 3].Text == currentPlayer && buttons[smallgridRow * 3 + i, smallgridCol * 3 + 1].Text == currentPlayer && buttons[smallgridRow * 3 + i, smallgridCol * 3 + 2].Text == currentPlayer)
                    return true;

                if (buttons[smallgridRow * 3, smallgridCol * 3 + i].Text == currentPlayer && buttons[smallgridRow * 3 + 1, smallgridCol * 3 + i].Text == currentPlayer && buttons[smallgridRow * 3 + 2, smallgridCol * 3 + i].Text == currentPlayer)
                    return true;
            }

            if (buttons[smallgridRow * 3, smallgridCol * 3].Text == currentPlayer && buttons[smallgridRow * 3 + 1, smallgridCol * 3 + 1].Text == currentPlayer && buttons[smallgridRow * 3 + 2, smallgridCol * 3 + 2].Text == currentPlayer)
                return true;

            if (buttons[smallgridRow * 3, smallgridCol * 3 + 2].Text == currentPlayer && buttons[smallgridRow * 3 + 1, smallgridCol * 3 + 1].Text == currentPlayer && buttons[smallgridRow * 3 + 2, smallgridCol * 3].Text == currentPlayer)
                return true;

            return false;
        }

        bool CheckOverallWin()
        {
            //check rows, columns, and diagonals on the main grid for small grid winners
            for (int i = 0; i < 3; i++)
            {
                if (smallGrid[i, 0] == currentPlayer && smallGrid[i, 1] == currentPlayer && smallGrid[i, 2] == currentPlayer)
                    return true;

                if (smallGrid[0, i] == currentPlayer && smallGrid[1, i] == currentPlayer && smallGrid[2, i] == currentPlayer)
                    return true;
            }

            if (smallGrid[0, 0] == currentPlayer && smallGrid[1, 1] == currentPlayer && smallGrid[2, 2] == currentPlayer)
                return true;

            if (smallGrid[0, 2] == currentPlayer && smallGrid[1, 1] == currentPlayer && smallGrid[2, 0] == currentPlayer)
                return true;

            return false;
        }
        //checks if small grid is full
        bool IsSmallgridFull(int smallgrid)
        {
            int smallgridRow = smallgrid / 3;
            int smallgridCol = smallgrid % 3;

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (string.IsNullOrEmpty(buttons[smallgridRow * 3 + row, smallgridCol * 3 + col].Text))
                        return false;
                }
            }
            return true;
        }
        //marks the background color of the buttons based on the winner, if x wins its light blue and o is light pink 
        void MarkSmallgrid(int smallgridRow, int smallgridCol, string winner)
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    buttons[smallgridRow * 3 + row, smallgridCol * 3 + col].BackColor =
                        winner == "X" ? Color.Blue : Color.Red;
                }
            }
        }

        //resets the board by clearing button text, enabling the buttons and resetting the colours of grid
        void ResetBoard()
        {
            foreach (var button in buttons)
            {
                button.Text = string.Empty;
                button.Enabled = true;
                button.BackColor = Color.White;
            }

            smallGrid = new string[3, 3];
            currentPlayer = "X";
            currentSmallgrid = -1;
            Updateboard();
        }
    }
}

