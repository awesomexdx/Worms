using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snakes.DataBase.Models;

namespace Snakes.DataBase.Repositories
{
    public interface IWorldBehaviourRepository
    {
        IEnumerable<WorldBehaviourModel> Get();
        WorldBehaviourModel Get(int id);
        bool Create(WorldBehaviourModel item, bool autoincrement = true);
        void Update(WorldBehaviourModel updatedUser);
        WorldBehaviourModel Delete(int id);
    }
}
