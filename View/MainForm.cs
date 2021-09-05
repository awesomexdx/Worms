using Snakes.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Snakes.behaviours;
using Snakes.Utils;

namespace View
{
    public partial class MainForm : Form
    {
        private GameSession gameSession;

        private int currentStep = 0;

        private bool fieldPrepared = false;

        private bool started = false;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        void prepareField()
        {
            worldField.BackgroundColor = Color.White;

            for (int i = 0; i < 20; i++)
            {
                worldField.Columns.Add("", "" + (i - 10));
            }

            for (int i = 0; i < 25; i++)
            {
                worldField.Rows.Add("", "");
                worldField.Rows[i].HeaderCell.Value = "" + (i - 10);
            }
        }
        void goToStep(int stepNumber)
        {
            currentStep = stepNumber;

            step.Text = "Step number " + stepNumber;

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    worldField[i, j].Value = "";
                    worldField[i, j].Style.BackColor = Color.White;
                }
            }

            foreach (var snake in gameSession.SnakeList[stepNumber])
            {
                try
                {
                    worldField[snake.Cell.X + 10, snake.Cell.Y + 10].Value = snake.Name + "(" + snake.HitPoints + ")";
                    worldField[snake.Cell.X + 10, snake.Cell.Y + 10].Style.BackColor = Color.Green;
                }
                catch { }
            }


            foreach (var food in gameSession.FoodList[stepNumber])
            {
                try
                {
                    worldField[food.Cell.X + 10, food.Cell.Y + 10].Value = "Food" + "(" + food.TimeToLive + ")";
                    worldField[food.Cell.X + 10, food.Cell.Y + 10].Style.BackColor = Color.Red;
                }
                catch { }
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (!fieldPrepared)
            {
                prepareField();
                fieldPrepared = true;
            }
            World.Reset();
            World.Instance().AddSnake(new Snake(NameGenerator.GenerateNext(), new Cell(0, 0, CellContent.Snake), new GoToFoodBehaviour(new Cell(0, 0, CellContent.Snake))));
            World.Instance().AddSnake(new Snake(NameGenerator.GenerateNext(), new Cell(3, 3, CellContent.Snake), new GoToFoodBehaviour(new Cell(3, 3, CellContent.Snake))));
            gameSession = World.Start();
            goToStep(0);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void next_Click(object sender, EventArgs e)
        {
            if (currentStep < 99)
            {
                goToStep(currentStep + 1);
            }
        }

        private void prev_Click(object sender, EventArgs e)
        {
            if (currentStep > 0)
            {
                goToStep(currentStep - 1);

            }
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            started = !started;

            if (started)
            {
                continueButton.Text = "Stop";
            }
            else
            {
                continueButton.Text = "Continue with timeout";
            }

            continueGame();
        }

        private async void continueGame()
        {
            while (started && currentStep < 99)
            {
                await Task.Delay((int)this.timeoutUpDown.Value);
                goToStep(currentStep + 1);
            }
        }
    }
}
