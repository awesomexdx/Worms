using Snakes.models;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Snakes.Services;
using Snakes.Utils;
using System.Threading;
using Snakes.behaviours;

namespace View
{
    public partial class MainForm : Form, IHostedService
    {
        private readonly IFoodGenerator foodGenerator;
        private readonly INameGenerator nameGenerator;
        private readonly ISnakeActionsService snakeActionsService;
        private readonly IFileHandler fileHandlerService;

        private World world;

        private GameSession gameSession;

        private int currentStep = 0;

        private bool fieldPrepared = false;

        private bool started = false;
        public MainForm(IFoodGenerator foodGenerator,
            INameGenerator nameGenerator,
            ISnakeActionsService snakeActionsService,
            IFileHandler fileHandlerService)
        {
            InitializeComponent();
            this.foodGenerator = foodGenerator;
            this.nameGenerator = nameGenerator;
            this.snakeActionsService = snakeActionsService;
            this.fileHandlerService = fileHandlerService;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void prepareField()
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

        private void goToStep(int stepNumber)
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

            foreach (Snake snake in gameSession.SnakeList[stepNumber])
            {
                try
                {
                    worldField[snake.Cell.X + 10, snake.Cell.Y + 10].Value = snake.Name + "(" + snake.HitPoints + ")";
                    worldField[snake.Cell.X + 10, snake.Cell.Y + 10].Style.BackColor = Color.Green;
                }
                catch { }
            }


            foreach (Food food in gameSession.FoodList[stepNumber])
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

            this.world = new World(nameGenerator, foodGenerator, snakeActionsService, fileHandlerService);

            this.world.AddSnake(new Snake("John", new Cell(0, 0, CellContent.Snake),
                new GoToFoodBehaviour(new Cell(0, 0, CellContent.Snake), world)));
            this.gameSession = world.Start();

            goToStep(0);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            var cancellationTokenSource = new CancellationTokenSource(1000);
            
            StopAsync(cancellationTokenSource.Token);
        }

        private void next_Click(object sender, EventArgs e)
        {
            if (currentStep < 99 && fieldPrepared)
            {
                goToStep(currentStep + 1);
            }
        }

        private void prev_Click(object sender, EventArgs e)
        {
            if (currentStep > 0 && fieldPrepared)
            {
                goToStep(currentStep - 1);

            }
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            if (fieldPrepared)
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
        }

        private async void continueGame()
        {
            while (started && currentStep < 99 && fieldPrepared)
            {
                await Task.Delay((int)timeoutUpDown.Value);
                goToStep(currentStep + 1);
            }
        }

        private void toFirstStepButton_Click(object sender, EventArgs e)
        {
            if (fieldPrepared)
            {
                goToStep(0);
            }
        }

        private void toLastStepButton_Click(object sender, EventArgs e)
        {
            if (fieldPrepared)
            {
                goToStep(99);
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Application.Run(this);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
