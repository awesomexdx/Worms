using System.IO;

namespace Snakes.Utils
{
    internal class FileHandler
    {
        public static void CreateNewGameSessionFile()
        {
            using (FileStream fstream = new FileStream($"gameSession.txt", FileMode.OpenOrCreate))
            {
                fstream.SetLength(0);
            }
        }
        public static void WriteToFile(string data)
        {
            using (FileStream fstream = new FileStream($"gameSession.txt", FileMode.Append))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(data);
                fstream.Write(array, 0, array.Length);
            }
        }
    }
}
