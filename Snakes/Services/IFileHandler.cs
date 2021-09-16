namespace Snakes.Services
{
    public interface IFileHandler
    {
        public void CreateNewGameSessionFile();

        public void WriteToFile(string data);
    }
}
