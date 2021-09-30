using Snakes.Services;
using System.IO;

namespace Snakes.Utils
{
    public class FileHandler : IFileHandler
    {
        private readonly string fileName = "gameSession.txt";
        public TextWriter TextWriter { get; set; }
        public void CreateNewGameSessionFile()
        {
            using (FileStream fstream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                fstream.SetLength(0);
            }
        }
        public void WriteToFile(string data)
        {
            using (FileStream fstream = new FileStream(fileName, FileMode.Append))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(data + "\n");
                fstream.Write(array, 0, array.Length);
            }
        }

        public void WriteToTextWriter(string data)
        {
            if (TextWriter != null)
            {
                TextWriter.WriteLine(data);
            }
        }
    }
}
