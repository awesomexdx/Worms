using Snakes.Services;
using System.IO;

namespace Snakes.Utils
{
    internal class FileHandler : IFileHandler
    {
        public void CreateNewGameSessionFile()
        {
            using (FileStream fstream = new FileStream($"gameSession.txt", FileMode.OpenOrCreate))
            {
                fstream.SetLength(0);
            }
        }
        public void WriteToFile(string data)
        {
            using (FileStream fstream = new FileStream($"gameSession.txt", FileMode.Append))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(data);
                fstream.Write(array, 0, array.Length);
            }
        }
    }
}
