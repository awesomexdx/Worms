using Snakes.behaviours;
using Snakes.models;
using Snakes.Services;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Snakes.DataBase.Base;
using Snakes.DataBase.Repositories;

namespace View
{
    public partial class MainForm : Form
    {
        private readonly IFoodGenerator foodGenerator;
        private readonly INameGenerator nameGenerator;
        private readonly ISnakeActionsService snakeActionsService;
        private readonly IFileHandler fileHandlerService;
        private readonly IWorldBehaviourRepository worldBehaviourRepository;

        private World world;

        private GameSession gameSession;

        private int currentStep = 0;

        private bool fieldPrepared = false;

        private bool started = false;
        public MainForm(IFoodGenerator foodGenerator,
            INameGenerator nameGenerator,
            ISnakeActionsService snakeActionsService,
            IFileHandler fileHandlerService,
            IWorldBehaviourRepository worldBehaviourRepository)
        {
            InitializeComponent();
            this.foodGenerator = foodGenerator;
            this.nameGenerator = nameGenerator;
            this.snakeActionsService = snakeActionsService;
            this.fileHandlerService = fileHandlerService;
            this.worldBehaviourRepository = worldBehaviourRepository;
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

            world = new World(nameGenerator, foodGenerator, snakeActionsService, fileHandlerService);

            world.AddSnake(new Snake("John", new Cell(0, 0),
                new OptimumBehaviour()));
            gameSession = world.Start();

            goToStep(0);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
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

        private Task updateProgress(int val)
        {
            progressSimulations.Value = val;
            return Task.CompletedTask;
        }

        private async void bestOfButton_Click(object sender, EventArgs e)
        {
            GameSession bestGameSession = new GameSession();
            int maxSnakes = 0;
            int totalSnakes = 0;
            double bestAvg = 0;
            int bestParam = 0;
            int fails = 0;
            progressSimulations.MaxValue = (int)countOfGenerationsUpDown.Value;
            progressSimulations.Value = 0;
            progressSimulations.BackColor = Color.White;
            progressSimulations.ForeColor = Color.Black;
            progressSimulations.BorderColor = Color.Black;

            //for (int j = 2; j< 10; j++)
            //{
                //OptimumBehaviour.maxSnakes = j;
                for (int i = 0; i < countOfGenerationsUpDown.Value; i++)
                {
                    world = new World(nameGenerator, foodGenerator, snakeActionsService, fileHandlerService);

                    world.AddSnake(new Snake("John", new Cell(0, 0),
                        new OptimumBehaviour()));
                    gameSession = world.Start();
                    totalSnakes += gameSession.SnakeList[99].Count;
                    
                    if (maxSnakes < gameSession.SnakeList[99].Count)
                    {
                        maxSnakes = gameSession.SnakeList[99].Count;
                        bestGameSession = gameSession;
                    }

                    await updateProgress(i);
                    await Task.Delay(2);
                }
                if (bestAvg < totalSnakes/(double)countOfGenerationsUpDown.Value) {
                    bestAvg = totalSnakes / (double)countOfGenerationsUpDown.Value;
                    //bestParam = j;
                }
                totalSnakes = 0;
            //}

            progressSimulations.Value = (int)countOfGenerationsUpDown.Value;

            gameSession = bestGameSession;
            countOfSnakesLable.Text = $"Best count of snakes: {maxSnakes}," +
                $" Dead food count: {gameSession.DeadFoodCount}," +
                $" Average: {totalSnakes/countOfGenerationsUpDown.Value}" + 
                $" Best Param: {bestParam} with avg {bestAvg}";

            if (fieldPrepared)
            {
                goToStep(99);
            }
            else
            {
                prepareField();
                fieldPrepared = true;
                goToStep(99);
            }
        }

        private void simulateWorldBehaviour_Click(object sender, EventArgs e)
        {
            World worldForDb = new World(nameGenerator, foodGenerator, snakeActionsService, fileHandlerService);
            worldForDb.WorldBehaviour = worldBehaviourRepository;
            worldForDb.AddSnake(new Snake("John", new Cell(0, 0),
                new OptimumBehaviour()));
            gameSession = worldForDb.SimulateSessionByName(behaviourName.Text);
            if (fieldPrepared)
            {
                goToStep(0);
            }
            else
            {
                prepareField();
                fieldPrepared = true;
                goToStep(0);
            }
        }
    }
}
