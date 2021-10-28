using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Snakes.behaviours;
using Snakes.models;
using Snakes.moves;
using Snakes.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snakes.Controllers
{
    [ApiController]
    public class WormsController : ControllerBase
    {
        [Route("/{name}/getAction")]
        [HttpPost]
        public ActionResult GetAction(string name, [FromBody] JsonWorld jsonWorld)
        {
            var snake = jsonWorld.worms.Where(x => x.name == name).FirstOrDefault();
            var realSnake = new Snake(name, new Cell(snake.position.x, snake.position.y), new GoToFoodBehaviour());

            realSnake.Behaviour.CurrentCell = new Cell(snake.position.x, snake.position.y);
            realSnake.Behaviour.SnakeHP = snake.lifeStrength;

            List<Food> realFoods = jsonWorld.foods.Select(x => new Food(new Cell(x.position.x, x.position.y))).ToList();
            List<Snake> realSnakes = jsonWorld.worms.Select(x => new Snake(x.name, new Cell(x.position.x, x.position.y), new GoToFoodBehaviour())).ToList();
            SnakeAction snakeAction = realSnake.Behaviour.NextStep(new Snake(realSnake), new List<Food>(realFoods), new List<Snake>(realSnakes));

            string move;
            switch (snakeAction.Move)
            {
                case MoveUp:
                    move = "Up";
                    break;
                case MoveDown:
                    move = "Down";
                    break;
                case MoveRight:
                    move = "Right";
                    break;
                case MoveLeft:
                    move = "Left";
                    break;
                default:
                    move = "Up";
                    break;
            }

            bool split = snakeAction.ActionType == ActionType.REPRODUCE ? true : false; 

            return new JsonResult(new JsonResponce() { action = new JsonWormAction() {direction= move, split = split } });
        }

        [Route("/{name}/getAction")]
        [HttpGet]
        public IActionResult GetAction()
        {
            return new JsonResult(new { action = new JsonWormAction() { direction = "Up", split = false } });
        }
    }
}
