using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snakes.Services
{
    public interface IFileHandler
    {
        public void CreateNewGameSessionFile();

        public void WriteToFile(string data);
    }
}
