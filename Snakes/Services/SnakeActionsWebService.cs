using Snakes.models;
using Snakes.moves;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Snakes.Services
{
    public class SnakeActionsWebService : ISnakeActionsService
    {
        public SnakeAction Answer(Snake snake, World world)
        {
            var result = PostRequest(snake.Name, world.GetStateInJSON());
            ActionType actionType = result.action.split ? ActionType.REPRODUCE : ActionType.MOVE;
            IMove move;
            switch(result.action.direction)
            {
                case "Up":
                    move = new MoveUp();
                    break;
                case "Down":
                    move = new MoveDown();
                    break;
                case "Right":
                    move = new MoveRight();
                    break;
                case "Left":
                    move = new MoveLeft();
                    break;
                default:
                    move = new MoveNoWhere();
                    break;
            }

            return new SnakeAction(move, actionType);
        }

        private JsonResponce PostRequest(string name, JsonWorld jsonWorld)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create($"http://localhost:5000/{name}/getAction");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            string data = JsonSerializer.Serialize<JsonWorld>(jsonWorld);
            // преобразуем данные в массив байтов
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);

            //записываем данные в поток запроса
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }

            var answer = new JsonResponce();
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                answer = JsonSerializer.Deserialize<JsonResponce>(streamReader.ReadToEnd());
            }

            Console.WriteLine("Запрос выполнен...");
            return answer;
        }
    }
}
