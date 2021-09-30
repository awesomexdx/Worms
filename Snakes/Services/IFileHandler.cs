using System.IO;

namespace Snakes.Services
{
    public interface IFileHandler
    {
        public TextWriter TextWriter { get; set; }

        public void CreateNewGameSessionFile();
        public void WriteToFile(string data);

        public void WriteToTextWriter(string data);
    }
}
